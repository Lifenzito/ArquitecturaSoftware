using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    public class MovimientoRepository : IMovimientoRepository
    {
        private readonly List<Movimiento> movimientos;

        public MovimientoRepository()
        {
            movimientos = new List<Movimiento>();
        }

        public void RegistrarMovimiento(
            Movimiento movimiento)
        {
            movimientos.Add(movimiento);
        }

        public List<Movimiento> ObtenerMovimientos()
        {
            return movimientos;
        }
    }
}
