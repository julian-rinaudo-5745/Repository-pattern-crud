using Practica01.datos.interfaces;
using Practica01.dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos.repositorios
{
    public class RepositorioFormaPago : IFormaPago
    {
        public List<FormaPago> ObtenerTodo()
        {
            List<FormaPago> listFormasPago = new List<FormaPago>();

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_OBTENER_FORMAS_PAGO", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        FormaPago formaPago = new FormaPago
                        {
                            Id = (int)reader["id"],
                            Nombre = (string)reader["nombre"],
                        };

                        listFormasPago.Add(formaPago);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al obtener todas las formas de pago. Error: {ex.Message}");
            }

            return listFormasPago;
        }
        public FormaPago ObtenerPorId(int id)
        {
            FormaPago formaPago = new FormaPago();

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_OBTENER_FORMA_PAGO", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        formaPago.Id = (int)reader["id"];
                        formaPago.Nombre = (string)reader["nombre"];
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al obtener una forma de pago. Error: {ex.Message}");
            }

            return formaPago;
        }
        public bool Crear(FormaPago formaPago)
        {
            var resultado = false;

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_INSERTAR_FORMA_PAGO", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nombre", formaPago.Nombre);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas == 1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al crear una forma de pago. Error: {ex.Message}");
            }

            return resultado;
        }
        public bool Editar(FormaPago formaPago)
        {
            bool resultado = false;

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_EDITAR_FORMA_PAGO", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", formaPago.Id);
                    cmd.Parameters.AddWithValue("nombre", formaPago.Nombre);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas == 1;

                };
            }
            catch (Exception ex)
            {

                throw new Exception($"Error inesperado al editar una forma de pago. Error: {ex.Message}");
            }

            return resultado;
        }
        public bool Eliminar(int formaPagoId)
        {
            bool resultado = false;

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_ELIMINAR_FORMA_PAGO", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("id", formaPagoId);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas == 1;
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error inesperado al eliminar una forma de pago. Error: {ex.Message}");
            }

            return resultado;
        }


    }
}
