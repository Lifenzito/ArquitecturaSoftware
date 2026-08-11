using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    public class ServicioDescuento
    {
        private readonly List<IEntidadConvenio> convenios;

        public ServicioDescuento(
            List<IEntidadConvenio> convenios)
        {
            this.convenios = convenios;
        }

        public decimal CalcularDescuento(
            decimal precio,
            Cliente cliente)
        {
            IEntidadConvenio? convenio =
                convenios.FirstOrDefault(c =>
                    c.AplicaA(cliente));

            if (convenio == null)
            {
                return 0;
            }

            return convenio.CalcularBeneficio(precio);
        }
    }
}
