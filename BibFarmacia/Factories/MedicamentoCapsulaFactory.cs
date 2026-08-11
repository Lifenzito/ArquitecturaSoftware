using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Enum;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Factories
{
    public class MedicamentoCapsulaFactory : IProductoFactory
    {
        public string Tipo => "medicamento_capsula";

        public MedicamentoCapsula Crear(
            string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Proveedor proveedor,
            TipoRelleno tipoRelleno)
        {
            return new MedicamentoCapsula(
                nombre,
                precio,
                stock,
                stockMinimo,
                fechaVencimiento,
                (Laboratorio)proveedor,
                tipoRelleno);
        }

        public Producto Crear(string[] datos)
        {
            Laboratorio laboratorio =
                new Laboratorio(
                    datos[5],
                    "Medellin",
                    "4444444");

            return Crear(
                datos[0],
                decimal.Parse(datos[1]),
                int.Parse(datos[2]),
                int.Parse(datos[3]),
                DateTime.Parse(datos[4]),
                laboratorio,
                TipoRelleno.Gel);
        }
    }
}
