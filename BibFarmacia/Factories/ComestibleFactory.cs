using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    public class ComestibleFactory : IProductoFactory
    {
        public string Tipo => "comestible";

        public Comestible Crear(
            string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Proveedor proveedor)
        {
            return new Comestible(
                nombre,
                precio,
                stock,
                stockMinimo,
                fechaVencimiento,
                proveedor);
        }

        public Producto Crear(string[] datos)
        {
            Marca marca =
                new Marca(
                    datos[5],
                    "Medellin",
                    "4444444");

            return Crear(
                datos[0],
                decimal.Parse(datos[1]),
                int.Parse(datos[2]),
                int.Parse(datos[3]),
                DateTime.Parse(datos[4]),
                marca);
        }
    }
}
