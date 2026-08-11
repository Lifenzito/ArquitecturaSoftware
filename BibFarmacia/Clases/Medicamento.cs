using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Interfaces;

namespace BibFarmacia.Clases
{
    public class Medicamento : Producto,
        IProductoConStock,
        IVencimiento
    {
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public Medicamento(string nombre,
            decimal precio,
            int stock,
            int stockMinimo,
            DateTime fechaVencimiento,
            Laboratorio laboratorio)
            : base(nombre, precio, laboratorio)
        {
            Stock = stock;
            StockMinimo = stockMinimo;
            FechaVencimiento = fechaVencimiento;
        }

        public override void MostrarInformacion()
        {
            base.MostrarInformacion();

            Console.WriteLine($"Stock: {Stock}");
        }
    }
}
