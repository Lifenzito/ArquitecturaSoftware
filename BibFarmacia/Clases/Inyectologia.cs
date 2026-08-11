using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Clases
{
    public class Inyectologia : Procedimiento
    {
        public Inyectologia(string nombre,
            decimal precio,
            Proveedor proveedor,
            int duracionMinutos)
            : base(nombre, precio, proveedor,
                  duracionMinutos)
        {
        }
    }
}
