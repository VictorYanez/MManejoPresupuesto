using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public interface IRepositorioTransacciones
    {
        Task Crear(Transaccion transaccion);
        Task<IEnumerable<Transaccion>> ObtenerPorUsuarioId(ParametroObtenerTransaccionesPorUsuario modelo);
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
            var id = await connection.QuerySingleAsync<int>("Transacciones_Insertar",  
                new { transaccion.UsuarioId, transaccion.FechaTransaccion, transaccion.Monto, 
                        transaccion.CategoriaId, transaccion.CuentaId, transaccion.Nota
                },
                commandType : System.Data.CommandType.StoredProcedure);

            transaccion.Id = id;    
        }

        public async Task<IEnumerable<Transaccion>> ObtenerPorUsuarioId(
    ParametroObtenerTransaccionesPorUsuario modelo)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"SELECT t.Id, t.Monto, t.FechaTransaccion, c.Nombre as Categoria,
                        cu.Nombre as Cuenta, c.TipoOperacionId, Nota
                        FROM Transacciones t
                        INNER JOIN Categorias c
                        ON c.Id = t.CategoriaId
                        INNER JOIN Cuentas cu
                        ON cu.Id = t.CuentaId
                        WHERE t.UsuarioId = @UsuarioId
                        AND FechaTransaccion BETWEEN @FechaInicio AND @FechaFin
                        ORDER BY t.FechaTransaccion DESC;";
            return await connection.QueryAsync<Transaccion>(query, modelo);

        }
    }
}
