using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Interfaces;

namespace BibFarmacia.Eventos
{
    public class EventoPuntos : IEvento
    {
        public delegate void DelegadoPuntos(
            string mensaje);

        public event DelegadoPuntos?
            PuntosAcumulados;

        public void Disparar(
            string cliente,
            int puntos)
        {
            PuntosAcumulados?.Invoke(
                $"Cliente {cliente} acumuló {puntos} puntos");
        }

        void IEvento.Disparar(object entidad)
        {
            (string cliente, int puntos) =
                ((string, int))entidad;

            Disparar(cliente, puntos);
        }
    }
}
