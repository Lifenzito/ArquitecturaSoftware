using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Eventos
{
    public class EventoStockMinimo : IEvento
    {
        public delegate void DelegadoStock(
            string mensaje);

        public event DelegadoStock? StockMinimo;

        public void Disparar(
            Producto producto)
        {
            StockMinimo?.Invoke(
                $"ALERTA: stock mínimo de {producto.Nombre}");
        }

        void IEvento.Disparar(object entidad)
        {
            Disparar((Producto)entidad);
        }
    }
}
