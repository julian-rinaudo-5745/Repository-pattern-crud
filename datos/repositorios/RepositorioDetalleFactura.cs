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
        
        public RepositorioDetalleFactura() 
        {
            servicioArticulo = new ServicioArticulo();
        }
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
                            Id = (int)reader["id"],
                            Articulo = servicioArticulo.ObtenerPorId(articuloId),
                            Cantidad = (int)reader["cantidad"],
                            PrecioVenta = (decimal)reader["precio_venta"]
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
        public List<DetalleFactura> ObtenerPorNroFactura(int nroFactura)
        {
            List<DetalleFactura> listDetalles = new List<DetalleFactura>();

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString))
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_OBTENER_DETALLES_POR_NRO_FACTURA", cnn);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nro_factura", nroFactura);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int articuloId = (int)reader["id_articulo"];

                        DetalleFactura detalle = new DetalleFactura()
                        {
                            Id = (int)reader["id"],
                            Articulo = servicioArticulo.ObtenerPorId(articuloId),
                            Cantidad = (int)reader["cantidad"],
                            PrecioVenta = (decimal)reader["precio_venta"]
                        };

                        listDetalles.Add(detalle);
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al obtener todos los detalles por nro de factura. Error: {ex.Message}");
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
        public bool Editar(DetalleFactura detalleFactura, int nroFactura)
        {
            bool resultado = false;

            try
            {


                    SqlCommand cmd = new SqlCommand("SP_EDITAR_DETALLE", _connection, _transaction);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", detalleFactura.Id);
                    cmd.Parameters.AddWithValue("id_articulo", detalleFactura.Articulo.Id);
                    cmd.Parameters.AddWithValue("cantidad", detalleFactura.Cantidad);
                    cmd.Parameters.AddWithValue("precio_venta", detalleFactura.PrecioVenta);
                    cmd.Parameters.AddWithValue("nro_factura", nroFactura);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas == 1;
            }


            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al editar un detalle. Error: {ex.Message}");
            }

            return resultado;
        }

        public bool Eliminar(int nroFactura)
        {
            bool resultado = false;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINAR_DETALLE", _connection, _transaction);

                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("nro_factura", nroFactura);
                                
                cmd.ExecuteNonQuery();

                resultado = true;
            }


            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al eliminar un detalle. Error: {ex.Message}");
            }

            return resultado;
        }

    }
}
