using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        void Crear(TipoCuenta tipocuenta);
    }

    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        /* DataBase DeskTop */
       
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void Crear(TipoCuenta tipocuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id = connection.QuerySingle<int>($@"insert into TiposCuentas (Nombre, UsuarioId, Orden) 
                                                    values (@Nombre, @UsuarioId, 0);
                                                    select SCOPE_IDENTITY();", tipocuenta);
            tipocuenta.Id = id;
        }

    }
}
