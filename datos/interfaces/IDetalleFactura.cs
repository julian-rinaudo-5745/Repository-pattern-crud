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
        List<DetalleFactura> ObtenerPorNroFactura(int nroFactura);
        public bool Crear(int nroFactura, DetalleFactura detalleFactura);
        public bool Editar(DetalleFactura detalleFactura, int nroFactura);
        public bool Eliminar(int nroFactura);
    }
}
