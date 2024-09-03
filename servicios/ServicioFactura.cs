using Practica01.datos;
using Practica01.datos.interfaces;
using Practica01.datos.repositorios;
using Practica01.datos.UnitOfWork;
using Practica01.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.servicios
{
    public class ServicioFactura
    {
        private IFactura repositorioFactura;
        private readonly UnitOfWork _unitOfWork;


        public ServicioFactura()
        {
            repositorioFactura = new RepositorioFactura();
            _unitOfWork = new UnitOfWork(Properties.Resources.cnnString); 
        }

        public List<Factura> ObtenerTodo()
        {
            List<Factura>  facturas = new List<Factura>();

            try
            {
                facturas = repositorioFactura.ObtenerTodo();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return facturas;
        }
        public bool Crear(Factura factura)
        {
            bool resultado = false;

            if (factura != null)
            {
                if (factura.FormaPago.Id == 0)
                {
                    Console.Error.WriteLine("Debe ingresar una forma de pago válida");
                    return false;
                }

                if (String.IsNullOrEmpty(factura.Cliente))
                {
                    Console.Error.WriteLine("El cliente es requerido");
                    return false;
                }

                
                try
                {
                    int nroFactura = _unitOfWork.RepositorioFactura.Crear(factura);

                    if (nroFactura > 0)
                    {
                        foreach (DetalleFactura detalle in factura.Detalles)
                        {
                            _unitOfWork.RepositorioDetalleFactura.Crear(nroFactura, detalle);
                        }
                    }

                    _unitOfWork.GuardarCambios();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
                finally
                {
                    _unitOfWork.Dispose();  
                }
                
            }

            return resultado;
        }

        public bool Editar(Factura factura)
        {
            bool resultado = false;


            if (factura != null)
            {

                try
                {
                    resultado = _unitOfWork.RepositorioFactura.Editar(factura);

                    if (resultado) 
                    {
                        if (factura.Detalles.Count > 0)
                        {
                            foreach (DetalleFactura detalle in factura.Detalles)
                            {
                                _unitOfWork.RepositorioDetalleFactura.Editar(detalle, factura.NroFactura);
                            }

                        }

                    }

                    _unitOfWork.GuardarCambios();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
                finally
                {
                    _unitOfWork.Dispose();
                }

            }

            return resultado;
        }
        public bool Eliminar(int nroFactura)
        {
            bool resultado = false;

            if (nroFactura != 0)
            {
                try
                {
                    resultado = _unitOfWork.RepositorioFactura.Eliminar(nroFactura);
                    if (resultado)
                    {

                        Console.WriteLine("Factura Editada con éxito");
                    }


                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
                finally
                {
                    _unitOfWork.Dispose();
                }
            }


            return resultado;
         }
    }
}
