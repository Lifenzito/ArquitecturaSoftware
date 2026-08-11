using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    public class ServicioProducto
    {
        private readonly IRepositoryProducto repositoryProducto;
        private readonly List<IVerificador> verificadores;
        private readonly List<IEvento> eventos;

        public ServicioProducto(
            IRepositoryProducto repositoryProducto,
            List<IVerificador> verificadores,
            List<IEvento> eventos)
        {
            this.repositoryProducto = repositoryProducto;
            this.verificadores = verificadores;
            this.eventos = eventos;
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

        public List<IEvento> ObtenerEventos()
        {
            return eventos;
        }

        public void Verificar()
        {
            foreach (var verificador in verificadores)
            {
                foreach (var producto in
                    ObtenerProductos())
                {
                    verificador.Verificar(producto);
                }
            }
        }
    }
}
