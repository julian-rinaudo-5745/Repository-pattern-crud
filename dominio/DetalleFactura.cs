using Practica01.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos
{
    public class DetalleFactura
    {
        public Articulo Articulo { get; set; }
        public int cantidad { get; set; }
    }
}
