using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    public class RepositoryProducto : IRepositoryProducto
    {
        // Numero de columnas del productos.txt heredado, que no trae
        // discriminador de tipo.
        private const int ColumnasSinTipo = 6;

        private const string TipoPorDefecto = "medicamento_capsula";

        private readonly List<Producto> productos;
        private readonly List<IProductoFactory> fabricas;

        public RepositoryProducto(
            List<IProductoFactory> fabricas)
        {
            productos = new List<Producto>();

            this.fabricas = fabricas;
        }

        public List<Producto> ObtenerProductos()
        {
            return productos;
        }

        public string AgregarProducto(
            Producto producto)
        {
            try
            {
                productos.Add(producto);

                return "Producto agregado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string CargarDesdeArchivo(
            string ruta)
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    return "Archivo no encontrado";
                }

                string[] lineas =
                    File.ReadAllLines(ruta);

                foreach (string linea in lineas)
                {
                    string[] datos =
                        linea.Split(';');

                    string tipo =
                        datos.Length == ColumnasSinTipo
                            ? TipoPorDefecto
                            : datos[datos.Length - 1];

                    IProductoFactory fabrica =
                        fabricas.First(f =>
                            f.Tipo == tipo);

                    productos.Add(
                        fabrica.Crear(datos));
                }

                return "Productos cargados";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
