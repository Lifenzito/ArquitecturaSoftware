using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Enum;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    public class RepositoryProducto : IRepositoryProducto
    {
        private readonly List<Producto> productos;

        public RepositoryProducto()
        {
            productos = new List<Producto>();
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

                    Laboratorio laboratorio =
                        new Laboratorio(
                            datos[5],
                            "Medellin",
                            "4444444");

                    MedicamentoCapsula medicamento =
                        new MedicamentoCapsula(
                            datos[0],
                            decimal.Parse(datos[1]),
                            int.Parse(datos[2]),
                            int.Parse(datos[3]),
                            DateTime.Parse(datos[4]),
                            laboratorio,
                            TipoRelleno.Gel);

                    productos.Add(medicamento);
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
