using Practica01.datos.interfaces;
using Practica01.dominio;
using Practica01.servicios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Practica01.datos.repositorios
{
    public class RepositorioFactura : IFactura
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        private ServicioFormaPago servicioFormaPago;
        private ServicioDetalleFactura servicioDetalleFactura;

        public RepositorioFactura() 
        {
            servicioFormaPago = new ServicioFormaPago();
            servicioDetalleFactura = new ServicioDetalleFactura();
        }
        public RepositorioFactura(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
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
                            Detalles = servicioDetalleFactura.ObtenerPorNroFactura(nroFactura)
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
        public int Crear(Factura factura)
        {
            int nroFactura = 0;

            try
            {
                SqlCommand cmd = new SqlCommand("SP_CREAR_FACTURA", _connection, _transaction);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("id_forma_pago", factura.FormaPago.Id);
                cmd.Parameters.AddWithValue("cliente", factura.Cliente);

                SqlParameter param = new SqlParameter("@nro_factura", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();

                nroFactura = (int)param.Value;
                    
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al crear factura. Error: {ex.Message}");
            }

            return nroFactura;
        }

        public bool Editar(Factura factura)
        {
            bool resultado = false;

            try
            {

                    SqlCommand cmd = new SqlCommand("SP_EDITAR_FACTURA", _connection, _transaction);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("nro_factura", factura.NroFactura);
                    cmd.Parameters.AddWithValue("fecha", factura.Fecha);
                    cmd.Parameters.AddWithValue("id_forma_pago", factura.FormaPago.Id);
                    cmd.Parameters.AddWithValue("cliente", factura.Cliente);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    resultado = filasAfectadas == 1;


            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al editar factura. Error: {ex.Message}");
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

                //servicioDetalleFactura.Eliminar(null, nroFactura);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado al eliminar factura. Error: {ex.Message}");
            }


            return resultado;
        }

    }
}
