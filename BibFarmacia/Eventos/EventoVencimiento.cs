using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Eventos
{
    public class EventoVencimiento : IEvento
    {
        public delegate void DelegadoVencimiento(
            string mensaje);

        public event DelegadoVencimiento?
            Vencimiento;

        public void Disparar(
            Producto producto)
        {
            Vencimiento?.Invoke(
                $"ALERTA: {producto.Nombre} próximo a vencer");
        }

        void IEvento.Disparar(object entidad)
        {
            Disparar((Producto)entidad);
        }
    }
}
