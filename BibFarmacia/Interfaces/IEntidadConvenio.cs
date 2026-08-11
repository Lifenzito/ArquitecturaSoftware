using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Enum;

namespace BibFarmacia.Interfaces
{
    public interface IEntidadConvenio
    {
        TipoBeneficio TipoBeneficio { get; set; }

        decimal CalcularBeneficio(decimal precio);

        bool AplicaA(Cliente cliente);
    }
}
