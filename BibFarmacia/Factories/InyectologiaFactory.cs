using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    public class InyectologiaFactory : IProductoFactory
    {
        public string Tipo => "inyectologia";

        public Inyectologia Crear(
            string nombre,
            decimal precio,
            Proveedor proveedor,
            int duracionMinutos)
        {
            return new Inyectologia(
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
