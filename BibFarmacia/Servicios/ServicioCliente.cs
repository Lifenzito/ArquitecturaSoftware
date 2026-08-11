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
    public class ServicioCliente
    {
        private readonly IClienteRepository clienteRepository;

        public EventoPuntos EventoPuntos;

        public ServicioCliente(
            IClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository;

            EventoPuntos = new EventoPuntos();
        }

        public void AgregarCliente(
            Cliente cliente)
        {
            clienteRepository.AgregarCliente(cliente);
        }

        public List<Cliente> ObtenerClientes()
        {
            return clienteRepository.ObtenerClientes();
        }

        public void AcumularPuntos(
            Cliente cliente,
            int puntos)
        {
            clienteRepository.AcumularPuntos(
                cliente,
                puntos);

            EventoPuntos.Disparar(
                cliente.Nombre,
                puntos);
        }

        public string Cargar(
            string ruta)
        {
            return clienteRepository.Cargar(ruta);
        }
    }
}
