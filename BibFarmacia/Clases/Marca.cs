using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Clases
{
    public class Marca : Proveedor
    {
        public Marca(string nombre,
            string direccion,
            string telefono)
            : base(nombre, direccion, telefono)
        {
        }
    }
}
