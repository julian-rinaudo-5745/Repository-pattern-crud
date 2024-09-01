using Practica01.dominio;
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
        public bool Crear(int nroFactura, DetalleFactura detalleFactura);
        public bool Editar(DetalleFactura detalleFactura);
        public bool Eliminar(int detalleId);
    }
}
