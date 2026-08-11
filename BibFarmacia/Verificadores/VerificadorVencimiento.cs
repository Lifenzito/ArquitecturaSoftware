using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Eventos;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Verificadores
{
    public class VerificadorVencimiento : IVerificador
    {
        private readonly EventoVencimiento eventoVencimiento;

        public VerificadorVencimiento(
            EventoVencimiento eventoVencimiento)
        {
            this.eventoVencimiento = eventoVencimiento;
        }

        public void Verificar(
            Producto producto)
        {
            if (producto is not IVencimiento
                productoConVencimiento)
            {
                return;
            }

            int dias =
                (productoConVencimiento.FechaVencimiento -
                DateTime.Now).Days;

            if (dias <= 30)
            {
                eventoVencimiento.Disparar(producto);
            }
        }
    }
}
