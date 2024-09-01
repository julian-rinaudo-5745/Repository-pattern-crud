using Practica01.datos;
using Practica01.datos.interfaces;
using Practica01.datos.repositorios;
using Practica01.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.servicios
{
    public class ServicioFormaPago
    {
        private IFormaPago repositorioFormaPago;

        public ServicioFormaPago()
        {
            repositorioFormaPago = new RepositorioFormaPago();
        }

        public List<FormaPago> ObtenerTodo()
        {
            List<FormaPago> articulos = new List<FormaPago>();

            try
            {
                articulos = repositorioFormaPago.ObtenerTodo();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return articulos;
        }

        public bool Crear(FormaPago formaPago)
        {
            bool resultado = false;

            if (formaPago != null)
            {
                if (String.IsNullOrEmpty(formaPago.Nombre))
                {
                    Console.Error.WriteLine("Debe ingresar un nombre");
                    return false;
                }

                try
                {
                    resultado = repositorioFormaPago.Crear(formaPago);
                    Console.WriteLine("Forma de pago creada con éxito");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }

            }

            return resultado;
        }

        public bool Editar(FormaPago formaPago)
        {
            bool resultado = false;

            if (formaPago != null)
            {
                if (String.IsNullOrEmpty(formaPago.Nombre) || formaPago.Id == 0)
                {
                    Console.Error.WriteLine(
                            $"No todos los datos son válidos. Id: {formaPago.Id}, Nombre: {formaPago.Nombre}"
                        );
                    return false;
                }

                try
                {
                    resultado = repositorioFormaPago.Editar(formaPago);
                    Console.WriteLine("Forma de pago editada con éxito");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }

            }

            return resultado;
        }

        public bool Eliminar(int formaPagoId)
        {
            bool resultado = false;

            if (formaPagoId == 0)
            {
                Console.Error.WriteLine("Debe ingresar un id válido");
                return false;
            }


            try
            {
                resultado = repositorioFormaPago.Eliminar(formaPagoId);
                Console.WriteLine("Forma de pago eliminada con éxito");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return resultado;
        }
    }
}
