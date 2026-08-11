using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibFarmacia.Interfaces
{
    public interface IServicioAutenticacion
    {
        bool Login(string user, string password);
    }
}
