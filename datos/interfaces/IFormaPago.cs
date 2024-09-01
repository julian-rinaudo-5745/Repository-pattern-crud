using Practica01.dominio;
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
        public FormaPago ObtenerPorId(int id);
        public bool Crear(FormaPago formaPago);
        public bool Editar(FormaPago formaPago);
        public bool Eliminar(int formaPagoId);
    }
}
