using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos.interfaces
{
    public interface IDetalleFactura
    {
        public List<DetalleFactura> ObtenerTodo();
        //Para crear o editar
        public bool Guardar(DetalleFactura detalleFactura);
        public bool Eliminar(int detalleId);
    }
}
