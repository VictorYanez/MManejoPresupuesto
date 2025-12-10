using Dapper;
using Microsoft.Data.SqlClient;
using MManejoPresupuesto.Models;

namespace MManejoPresupuesto.Servicios
{

    public interface IRepositorioCategorias
    {
        Task Crear(Categoria categoria);
        Task<IEnumerable<Categoria>> Obtener(int usuarioId);
        Task<Categoria> ObtenerPorId(int id, int usuarioId);
        Task Actualizar(Categoria categoria);
        Task Borrar(int id);

    }
    public class RepositorioCategorias : IRepositorioCategorias
    {
        private readonly string connectionString;
        public RepositorioCategorias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Categoria categoria)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"
                                        INSERT INTO Categorias (Nombre, TipoOperacionId, UsuarioId)
                                        Values (@Nombre, @TipoOperacionId, @UsuarioId);

                                        SELECT SCOPE_IDENTITY();";
            var id = await connection.QuerySingleAsync<int>(query,categoria);
            categoria.Id = id;
        }

        public async Task<IEnumerable<Categoria>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"SELECT *  FROM Categorias
                                        WHERE UsuarioId = @UsuarioId 
                                        ORDER BY Nombre;";
            return await connection.QueryAsync<Categoria>(query, new { usuarioId });
        }

        public async Task<Categoria> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            var query = @"SELECT *  FROM Categorias
                                        WHERE Id = @Id AND UsuarioId = @UsuarioId;";
            return await connection.QuerySingleOrDefaultAsync<Categoria>(query, new { id, usuarioId });
        }

        public async Task Actualizar(Categoria categoria)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"UPDATE Categorias
                                        SET Nombre = @Nombre, TipoOperacionId = @TipoOperacionId
                                        WHERE Id = @Id;";
            await connection.ExecuteAsync(query, categoria);
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"DELETE FROM Categorias
                                        WHERE Id = @Id;";
            await connection.ExecuteAsync(query, new { id });
        }

        public async Task<bool> Existe(string nombre, int tipoOperacionId, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            var query = @"SELECT COUNT(1)
                                        FROM Categorias
                                        WHERE Nombre = @Nombre AND TipoOperacionId = @TipoOperacionId AND UsuarioId = @UsuarioId;";
            var cantidad = await connection.QuerySingleAsync<int>(query, new { nombre, tipoOperacionId, usuarioId });
            return cantidad > 0;
        }


    }


}
