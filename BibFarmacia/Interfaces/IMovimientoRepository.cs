using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    public interface IMovimientoRepository
    {
        void RegistrarMovimiento(Movimiento movimiento);

        List<Movimiento> ObtenerMovimientos();
    }
}
