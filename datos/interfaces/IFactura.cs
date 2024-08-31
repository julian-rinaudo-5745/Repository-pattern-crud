using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos.interfaces
{
    public  interface IFactura
    {
        public List<Factura> ObtenerTodo();
        //Para crear o editar
        public bool Guardar(Factura factura);
        public bool Eliminar(int nroFactura);

    }
}
