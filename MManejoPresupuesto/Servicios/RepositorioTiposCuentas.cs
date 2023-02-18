using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipocuenta);

        Task Borrar(int id);

        Task Crear(TipoCuenta tipocuenta);

        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);

        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
    }

    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        /* DataBase DeskTop */
       
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public RepositorioTiposCuentas()
        {
        }

        public async Task Crear(TipoCuenta tipocuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>($@"insert into TiposCuentas (Nombre, UsuarioId, Orden) 
                                                    values (@Nombre, @UsuarioId, 0);
                                                    select SCOPE_IDENTITY();", tipocuenta);
            tipocuenta.Id = id;
        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId) 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TipoCuenta>(@"select Id, Nombre, Orden From TiposCuentas
                                                                Where usuarioId = @UsuarioId;", new { usuarioId });

        }

        public async Task Actualizar(TipoCuenta tipocuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync($@"update tiposCuentas set Nombre = @Nombre where Id = @Id;", tipocuenta);
        }

        public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"select Id, Nombre,Orden 
                                        from TiposCuentas where Id = @id AND UsuarioId = @usuarioId;", new { id, usuarioId });

        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"delete from TiposCuentas where Id = @id;", new { id });

        }
    }
}
