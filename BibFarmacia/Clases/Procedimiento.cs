using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Clases
{
    public abstract class Procedimiento : Producto
    {
        public int DuracionMinutos { get; set; }

        protected Procedimiento(string nombre,
            decimal precio,
            Proveedor proveedor,
            int duracionMinutos)
            : base(nombre, precio, proveedor)
        {
            DuracionMinutos = duracionMinutos;
        }
    }
}
