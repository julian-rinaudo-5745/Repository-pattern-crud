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
                        resultado = _unitOfWork.RepositorioFactura.Crear(factura);

                        Console.WriteLine("Factura creada con éxito");

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

            int nroFactura = factura.NroFactura;

            if (factura != null)
            {
                if (nroFactura == 0)
                {
                    Console.Error.WriteLine($"Número de factura no válido. Nro factura:{nroFactura}");
                    return false;
                }

                try
                {
                    resultado = _unitOfWork.RepositorioFactura.Crear(factura);
                    Console.WriteLine("Factura Editada con éxito");
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
                    Console.WriteLine("Factura eliminada con éxito");
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
