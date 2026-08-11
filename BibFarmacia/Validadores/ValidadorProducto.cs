using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Validadores
{
    public class ValidadorProducto : IValidador<Producto>
    {
        public string Validar(
            Producto producto)
        {
            if (producto.Precio <= 0)
            {
                return "Precio inválido";
            }

            if (producto is IProductoConStock
                productoConStock &&
                productoConStock.Stock < 0)
            {
                return "Stock inválido";
            }

            return "Producto válido";
        }
    }
}
