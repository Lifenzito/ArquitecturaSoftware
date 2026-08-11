using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    public interface IClienteRepository
    {
        void AgregarCliente(Cliente cliente);

        List<Cliente> ObtenerClientes();

        string Cargar(string ruta);

        void AcumularPuntos(Cliente cliente, int puntos);
    }
}
