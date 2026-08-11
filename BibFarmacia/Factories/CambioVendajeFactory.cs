using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    public class CambioVendajeFactory : IProductoFactory
    {
        public string Tipo => "cambio_vendaje";

        public CambioVendaje Crear(
            string nombre,
            decimal precio,
            Proveedor proveedor,
            int duracionMinutos)
        {
            return new CambioVendaje(
                nombre,
                precio,
                proveedor,
                duracionMinutos);
        }

        public Producto Crear(string[] datos)
        {
            Marca marca =
                new Marca(
                    datos[3],
                    "Medellin",
                    "4444444");

            return Crear(
                datos[0],
                decimal.Parse(datos[1]),
                marca,
                int.Parse(datos[2]));
        }
    }
}
