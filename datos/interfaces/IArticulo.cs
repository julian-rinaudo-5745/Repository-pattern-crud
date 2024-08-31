
using Practica01.dominio;

namespace Practica01.datos.interfaces
{
    public interface IArticulo
    {
        public List<Articulo> ObtenerTodo();
        public bool Crear(Articulo articulo);
        public bool Editar(Articulo articulo);
        public bool Eliminar(int articuloId);
    }
}
