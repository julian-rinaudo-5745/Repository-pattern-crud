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
        private ServicioArticulo servicioArticulo;
        public RepositorioDetalleFactura() 
        {
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
            return true;
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
