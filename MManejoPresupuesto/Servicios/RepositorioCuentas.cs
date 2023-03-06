using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public interface IRepositorioCuentas 
    {
        Task<IEnumerable<Cuenta>> Buscar(int usuarioId);
        Task Crear(Cuenta cuenta);
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
            var query = @"insert into Cuentas (Nombre, TipoCuentaId, Balance, Descripcion) values (@Nombre, @TipoCuentaId, @Balance, @Descripcion); " +
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
    }
}
