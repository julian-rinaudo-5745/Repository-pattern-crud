using Practica01.datos.interfaces;
using Practica01.dominio;
using System.Data;
using System.Data.SqlClient;


namespace Practica01.datos.repositorios
{
    public class RepositorioArticulo : IArticulo
    {
        public List<Articulo> ObtenerTodo()
        {
            List<Articulo> listArticulos = new List<Articulo>();

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_OBTENER_ARTICULOS", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Articulo articulo = new Articulo()
                        {
                            Id = (int)reader["id"],
                            Nombre = (string)reader["nombre"],
                            PrecioUnitario = (decimal)reader["precio_unitario"]
                        };

                        listArticulos.Add(articulo);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al obtener todos los articulos. Error: {ex.Message}");
            }

            return listArticulos;

        }
        public bool Crear(Articulo articulo)
        {
            var resultado = false;

            try
            {
                using(var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_INSERTAR_ARTICULO", cnn);

                    cmd.CommandType= CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nombre", articulo.Nombre);
                    cmd.Parameters.AddWithValue("precio_unitario", articulo.PrecioUnitario);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al crear un articulo. Error: {ex.Message}");
            }

            return resultado;
        }

        public bool Editar(Articulo articulo)
        {
            bool resultado = false;

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_EDITAR_ARTICULO", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", articulo.Id);
                    cmd.Parameters.AddWithValue("nombre", articulo.Nombre);
                    cmd.Parameters.AddWithValue("precio_unitario", articulo.PrecioUnitario);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas == 1;

                } ;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error inesperado al editar un articulo. Error: {ex.Message}");
            }

            return resultado;
        }
        public bool Eliminar(int articuloId)
        {
            bool resultado = false;

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_ARTICULO", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", articuloId);

                   int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas == 1;  
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error inesperado al eliminar un articulo. Error: {ex.Message}");
            }

            return resultado;   
        }


    }
}
