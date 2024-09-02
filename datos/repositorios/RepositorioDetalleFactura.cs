using Practica01.datos.interfaces;
using Practica01.dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.servicios;

namespace Practica01.datos.repositorios
{
    public class RepositorioDetalleFactura : IDetalleFactura
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private ServicioArticulo servicioArticulo;
        
        public RepositorioDetalleFactura() { }
        public RepositorioDetalleFactura(SqlConnection connection, SqlTransaction transaction) 
        {
            _connection = connection;
            _transaction = transaction;
            servicioArticulo = new ServicioArticulo();
        }
        public List<DetalleFactura> ObtenerTodo()
        {
            List<DetalleFactura> listDetalles = new List<DetalleFactura>();

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_OBTENER_DETALLES", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int articuloId = (int)reader["id_articulo"];

                        DetalleFactura detalle = new DetalleFactura()
                        {
                            Articulo = servicioArticulo.ObtenerPorId(articuloId),
                            Cantidad = (int)reader["cantidad"]
                        };

                        listDetalles.Add(detalle);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al obtener todos los detalles. Error: {ex.Message}");
            }

            return listDetalles;
        }
        public bool Crear(int nroFactura, DetalleFactura detalleFactura)
        {
            bool resultado = false;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_CREAR_DETALLE", _connection, _transaction);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nro_factura", nroFactura);
                cmd.Parameters.AddWithValue("id_articulo", detalleFactura.Articulo.Id);
                cmd.Parameters.AddWithValue("cantidad", detalleFactura.Cantidad);
                cmd.Parameters.AddWithValue("precio_venta", detalleFactura.PrecioVenta);

                int filasAfectadas = cmd.ExecuteNonQuery();

                resultado = filasAfectadas == 1;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al crear un detalle. Error: {ex.Message}");
            }

            return resultado;
        }
        public bool Editar(DetalleFactura detalleFactura)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int detalleId)
        {
            throw new NotImplementedException();
        }

    }
}
