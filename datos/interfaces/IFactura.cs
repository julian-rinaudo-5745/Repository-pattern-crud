using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practica01.dominio;

namespace Practica01.datos.interfaces
{
    public  interface IFactura
    {
        public List<Factura> ObtenerTodo();
        public int Crear(Factura factura);
        public bool Editar(Factura factura);
        public bool Eliminar(int nroFactura);

    }
}
