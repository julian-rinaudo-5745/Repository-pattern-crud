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
    public class ServicioDetalleFactura
    {
        private IDetalleFactura repositorioDetalleFactura;
        private readonly UnitOfWork _unitOfWork;

        public ServicioDetalleFactura() 
        {
            repositorioDetalleFactura = new RepositorioDetalleFactura();
            _unitOfWork = new UnitOfWork(Properties.Resources.cnnString);
        }

        public List<DetalleFactura> ObtenerTodo()
        {
            List<DetalleFactura> detalles = new List<DetalleFactura>();

            try
            {
                detalles = repositorioDetalleFactura.ObtenerTodo();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return detalles;
        }
        public bool Crear(int nroFactura, DetalleFactura detalle)
        {
            bool resultado = false;

            if(nroFactura == 0)
            {
                Console.WriteLine($"Nro de factura no válido. Nro factura: {nroFactura}");
                return false;
            }

            if (detalle != null)
            {
                try
                {
                    resultado = _unitOfWork.RepositorioDetalleFactura.Crear(nroFactura, detalle);
                    Console.WriteLine("Detalle creado con éxito");
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
    }
}
