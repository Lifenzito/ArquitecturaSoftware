using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> clientes;

        public ClienteRepository()
        {
            clientes = new List<Cliente>();
        }

        public void AgregarCliente(
            Cliente cliente)
        {
            clientes.Add(cliente);
        }

        public List<Cliente> ObtenerClientes()
        {
            return clientes;
        }

        public void AcumularPuntos(
            Cliente cliente,
            int puntos)
        {
            cliente.Puntos += puntos;
        }

        public string Cargar(
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

                    Cliente cliente =
                        new Cliente(
                            datos[0],
                            datos[1],
                            datos[2],
                            datos[3]);

                    clientes.Add(cliente);
                }

                return "Clientes cargados";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
