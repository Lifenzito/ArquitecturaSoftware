using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Eventos;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Verificadores
{
    public class VerificadorStock : IVerificador
    {
        private readonly EventoStockMinimo eventoStock;

        public VerificadorStock(
            EventoStockMinimo eventoStock)
        {
            this.eventoStock = eventoStock;
        }

        public void Verificar(
            Producto producto)
        {
            if (producto is not IProductoConStock
                productoConStock)
            {
                return;
            }

            if (productoConStock.Stock <=
                productoConStock.StockMinimo)
            {
                eventoStock.Disparar(producto);
            }
        }
    }
}
