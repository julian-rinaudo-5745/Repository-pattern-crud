using Practica01.datos.interfaces;
using Practica01.dominio;
using Practica01.servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos.repositorios
{
    public class RepositorioFactura : IFactura
    {
        private ServicioFormaPago servicioFormaPago;
        private ServicioDetalleFactura servicioDetalleFactura;
        public RepositorioFactura()
        {
            servicioFormaPago = new ServicioFormaPago();
            servicioDetalleFactura = new ServicioDetalleFactura();
        }
        public List<Factura> ObtenerTodo()
        {
            List<Factura> facturas = new List<Factura>();

            try
            {
                using (var cnn = new SqlConnection(Properties.Resources.cnnString)) 
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_OBTENER_FACTURAS", cnn);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int idFormaPago = (int)reader["id_forma_pago"];
                        int nroFactura = (int)reader["nro_factura"];

                        Factura factura = new Factura()
                        {

                            NroFactura = nroFactura,
                            Fecha = (DateTime)reader["fecha"],
                            FormaPago = servicioFormaPago.ObtenerPorId(idFormaPago),
                            Cliente = (string)reader["cliente"],
                            Detalles = servicioDetalleFactura.ObtenerPorIdONroFactura(null, nroFactura)
                        };

                        facturas.Add(factura);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al obtener todas las facturas. Error: {ex.Message}");
            }

            return facturas;
        }
        public bool Crear(Factura factura)
        {
            bool resultado = false;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_CREAR_FACTURA");

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_forma_pago", factura.FormaPago.Id);
                cmd.Parameters.AddWithValue("cliente", factura.Cliente);

                SqlParameter param = new SqlParameter("@nro_factura", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                int nroFactura = (int)param.Value;

                foreach (DetalleFactura detalle in factura.Detalles)
                {
                    servicioDetalleFactura.Crear(nroFactura, detalle);
                }

                resultado = true;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al crear factura. Error: {ex.Message}");
            }
            return resultado;
        }

        public bool Editar(Factura factura)
        {
            bool resultado = false;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_EDITAR_FACTURA");

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("nro_factura", factura.Fecha);
                cmd.Parameters.AddWithValue("fecha", factura.Fecha);
                cmd.Parameters.AddWithValue("id_forma_pago", factura.FormaPago.Id);
                cmd.Parameters.AddWithValue("cliente", factura.Cliente);

                cmd.ExecuteNonQuery();

                if (factura.Detalles.Count > 0)
                {
                    foreach (DetalleFactura detalle in factura.Detalles)
                    {
                        servicioDetalleFactura.Editar(detalle);
                    }

                }

                resultado = true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al crear factura. Error: {ex.Message}");
            }
            return resultado;
        }
        public bool Eliminar(int nroFactura)
        {
            bool resultado = false;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_ELIMINAR_FACTURA");

                cmd.Parameters.AddWithValue("nro_factura", nroFactura);

                cmd.ExecuteNonQuery();

                servicioDetalleFactura.Eliminar(null, nroFactura);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al eliminar factura. Error: {ex.Message}");
            }


            return resultado;
        }

    }
}
