using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01.datos.interfaces
{
    public interface IFormaPago
    {
        public List<FormaPago> ObtenerTodo();
        //Para crear o editar
        public bool Guardar(FormaPago formaPago);
        public bool Eliminar(int formaPagoId);
    }
}
