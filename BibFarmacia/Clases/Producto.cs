using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BibFarmacia.Clases
{
    public abstract class Producto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public Proveedor Proveedor { get; set; }

        protected Producto(string nombre,
            decimal precio,
            Proveedor proveedor)
        {
            Nombre = nombre;
            Precio = precio;
            Proveedor = proveedor;
        }

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Producto: {Nombre}");
            Console.WriteLine($"Precio: {Precio}");
        }
    }
}
