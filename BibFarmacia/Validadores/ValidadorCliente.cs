using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Validadores
{
    public class ValidadorCliente : IValidador<Cliente>
    {
        public string Validar(
            Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(
                cliente.Nombre))
            {
                return "Nombre inválido";
            }

            if (cliente.Cedula.Length < 3)
            {
                return "Cédula inválida";
            }

            return "Cliente válido";
        }
    }
}
