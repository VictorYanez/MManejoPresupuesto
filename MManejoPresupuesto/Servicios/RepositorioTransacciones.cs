using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public interface IRepositorioTransacciones 
    {
    
    }
    public class RepositorioTransacciones : IRepositorioTransacciones
    {
        private readonly string connectionString;

        public RepositorioTransacciones(IConfiguration configuration) 
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Transaccion transaccion) 
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("Transacciones_Insertar");  
        }
    }
}
