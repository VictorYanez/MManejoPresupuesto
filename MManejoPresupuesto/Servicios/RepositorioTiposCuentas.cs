using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Crear(TipoCuenta tipocuenta);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
    }

    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        /* DataBase DeskTop */
       
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
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

    }
}
