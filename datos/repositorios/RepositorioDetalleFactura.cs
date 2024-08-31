using Practica01.datos.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos.repositorios
{
    internal class RepositorioDetalleFactura : IDetalleFactura
    {
        public List<DetalleFactura> ObtenerTodo()
        {
            throw new NotImplementedException();
        }
        public bool Guardar(DetalleFactura detalleFactura)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(int detalleId)
        {
            throw new NotImplementedException();
        }

    }
}
