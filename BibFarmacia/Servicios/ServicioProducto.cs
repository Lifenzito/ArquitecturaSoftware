using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Eventos;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    public class ServicioProducto
    {
        private readonly IRepositoryProducto repositoryProducto;

        public EventoStockMinimo EventoStock;
        public EventoVencimiento EventoVencimiento;

        public ServicioProducto(
            IRepositoryProducto repositoryProducto)
        {
            this.repositoryProducto = repositoryProducto;

            EventoStock = new EventoStockMinimo();
            EventoVencimiento = new EventoVencimiento();
        }

        public string AgregarProducto(
            Producto producto)
        {
            return repositoryProducto
                .AgregarProducto(producto);
        }

        public List<Producto> ObtenerProductos()
        {
            return repositoryProducto
                .ObtenerProductos();
        }

        public string CargarDesdeArchivo(
            string ruta)
        {
            return repositoryProducto
                .CargarDesdeArchivo(ruta);
        }

        public void VerificarStock()
        {
            foreach (var producto in
                ObtenerProductos()
                .OfType<IProductoConStock>())
            {
                if (producto.Stock <=
                    producto.StockMinimo)
                {
                    EventoStock.Disparar(
                        (Producto)producto);
                }
            }
        }

        public void VerificarVencimiento()
        {
            foreach (var producto in
                ObtenerProductos()
                .OfType<IVencimiento>())
            {
                int dias =
                    (producto.FechaVencimiento -
                    DateTime.Now).Days;

                if (dias <= 30)
                {
                    EventoVencimiento
                        .Disparar(
                            (Producto)producto);
                }
            }
        }
    }
}
