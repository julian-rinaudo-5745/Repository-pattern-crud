
using Practica01.dominio;
using Practica01.datos.interfaces;
using Practica01.datos.repositorios;



namespace Practica01.servicios

{
    public class ServicioArticulo
    {
        private IArticulo repositorioArticulo;

        public ServicioArticulo()
        {
             repositorioArticulo = new RepositorioArticulo();
        }
        public List<Articulo> ObtenerTodo()
        {
            List<Articulo> articulos = new List<Articulo>();

            try
            {
                articulos = repositorioArticulo.ObtenerTodo();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return articulos;
        }
        public bool Crear(Articulo articulo)
        {
            bool resultado = false;

            if (articulo != null)
            {
                if (String.IsNullOrEmpty(articulo.Nombre) || articulo.PrecioUnitario < 1)
                {
                    Console.Error.WriteLine("Debe ingresar un nombre o un precio mayor a 0");
                    return false;
                } 
                
                try
                {
                    resultado = repositorioArticulo.Crear(articulo);
                    Console.WriteLine("Articulo creado con éxito");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
                
            }
   
            return resultado;
        }
        public bool Editar(Articulo articulo)
        {
            bool resultado = false;

            if (articulo != null)
            {
                if (String.IsNullOrEmpty(articulo.Nombre) || articulo.PrecioUnitario <= 0 || articulo.Id == 0)
                {
                    Console.Error.WriteLine(
                            $"No todos los datos son válidos. Id:{articulo.Id}, Nombre: {articulo.Nombre}, Precio: {articulo.PrecioUnitario}"
                        );
                    return false;
                }
               
                try
                {
                    resultado = repositorioArticulo.Editar(articulo);
                    Console.WriteLine("Articulo editado con éxito");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                }
                
            }

            return resultado;
        }
        public bool Eliminar(int articuloId)
        {
            bool resultado = false;

            if (articuloId == 0)
            {
                Console.Error.WriteLine("Debe ingresar un id válido");
                return false;
            }


            try
            {
                resultado = repositorioArticulo.Eliminar(articuloId);
                Console.WriteLine("Articulo eliminado con éxito");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return resultado;
        }

    }
}
