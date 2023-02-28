using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{
    public interface IRepositorioCuentas 
    {
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
    }
}
