
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
        public Articulo ObtenerPorId(int id)
        {
            Articulo articulo = new Articulo();

            if (id <= 0)
            {
                Console.Error.WriteLine($"Id no válido. Id: {id}");
                return articulo;
            }

            try
            {
                articulo = repositorioArticulo.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return articulo;
        }
        public bool Crear(Articulo articulo)
        {
            bool resultado = false;

            if (articulo != null)
            {
                if (String.IsNullOrEmpty(articulo.Nombre))
                {
                    Console.Error.WriteLine("Debe ingresar un nombre");
                    return false;
                }

                if (articulo.PrecioUnitario < 1)
                {
                    Console.Error.WriteLine("Debe ingresar un precio mayor a 0");
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
                if (articulo.Id == 0)
                {
                    Console.Error.WriteLine($"Id no válido. Id: {articulo.Id}");
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
                Console.Error.WriteLine($"Id no válido. Id: {articuloId}");
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
