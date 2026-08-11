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
    public class MedicamentoLiquidoFactory : IProductoFactory
    {
        public string Tipo => "medicamento_liquido";

        public MedicamentoLiquido Crear(
            string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Proveedor proveedor,
            MaterialEnvase materialEnvase,
            int mililitros)
        {
            return new MedicamentoLiquido(
                nombre,
                precio,
                stock,
                stockMinimo,
                fechaVencimiento,
                (Laboratorio)proveedor,
                materialEnvase,
                mililitros);
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
                System.Enum.Parse<MaterialEnvase>(datos[6]),
                int.Parse(datos[7]));
        }
    }
}
