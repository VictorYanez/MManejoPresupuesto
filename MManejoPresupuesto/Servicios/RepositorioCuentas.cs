using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public interface IRepositorioCuentas 
    {
        Task Actualizar(CuentaCreacionViewModel cuenta);
        Task Borrar(int id);
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta cuenta);
        Task<Cuenta> ObtenerPorId(int id, int usuarioId);
    }

    public class RepositorioCuentas : IRepositorioCuentas
    {
        private readonly string conectionString;

        public RepositorioCuentas(IConfiguration configuration) 
        {
            conectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Cuenta cuenta)
        {
            using var connection = new SqlConnection(conectionString);
            var query = @"insert into Cuentas (Nombre, TipoCuentaId, Balance, Descripcion) 
                        values (@Nombre, @TipoCuentaId, @Balance, @Descripcion); " +
                                "Select SCOPE_IDENTITY();";

            var id = await connection.QuerySingleAsync<int>(query, cuenta);

            cuenta.Id = id;
        }

        public async Task<IEnumerable<Cuenta>> Buscar(int usuarioId)
        {
            using var connection = new SqlConnection(conectionString);
            var query = @"Select ct.id, ct.Nombre, balance, tc.Nombre AS TipoCuenta 
                            from Cuentas ct
                            INNER JOIN TiposCuentas tc ON ct.TipoCuentaId = tc.Id
                            where tc.UsuarioId = @UsuarioId Order by tc.Orden;";

            return await connection.QueryAsync<Cuenta>(query, new { usuarioId });
        }

        public async Task<Cuenta> ObtenerPorId(int id, int usuarioId) 
        {
            using var connection = new SqlConnection(conectionString);
            var query = @"Select ct.id, ct.Nombre, balance, Descripcion, tc.Id 
                            from Cuentas ct
                            INNER JOIN TiposCuentas tc ON ct.TipoCuentaId = tc.Id
                            where tc.UsuarioId = @UsuarioId AND ct.Id = @Id;";

            return await connection.QueryFirstOrDefaultAsync<Cuenta>(query, new {id, usuarioId});
        }

        public async Task Actualizar(CuentaCreacionViewModel cuenta) 
        {
            using var connection = new SqlConnection(conectionString);
            var query = @"update Cuentas
                            set Nombre = @Nombre, Descripcion = @Descripcion, Balance = @Balance, TipoCuentaId = @TipoCuentaId
                            where Id = @Id;";
            await connection.ExecuteAsync(query, cuenta);
        }

        public async Task Borrar(int id) 
        {
            using var connection = new SqlConnection(conectionString);
            var query = @"delete from Cuentas where id = @Id;";
            await connection.ExecuteAsync(query, new { id });
        }
    }
}
