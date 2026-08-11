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
    public class ServicioMovimiento
    {
        private readonly IMovimientoRepository movimientoRepository;

        public EventoMovimiento EventoMovimiento;

        public ServicioMovimiento(
            IMovimientoRepository movimientoRepository)
        {
            this.movimientoRepository = movimientoRepository;

            EventoMovimiento =
                new EventoMovimiento();
        }

        public void RegistrarMovimiento(
            Movimiento movimiento)
        {
            movimientoRepository
                .RegistrarMovimiento(movimiento);

            EventoMovimiento.Disparar(
                movimiento.Tipo);
        }

        public List<Movimiento>
            ObtenerMovimientos()
        {
            return movimientoRepository
                .ObtenerMovimientos();
        }
    }
}
