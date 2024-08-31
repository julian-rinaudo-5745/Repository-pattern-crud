using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos.interfaces
{
    public interface IArticulo
    {
        public List<Articulo> ObtenerTodo();
        public bool Guardar(Articulo articulo);  //Para crear o editar
        public bool Eliminar(int articuloId);
    }
}
