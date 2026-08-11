using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Enum;

namespace BibFarmacia.Convenios
{
    public class ConvenioUniversidad : Convenio
    {
        public ConvenioUniversidad(string nombreEntidad)
            : base(nombreEntidad,
                  TipoConvenio.Universidad)
        {
        }
    }
}
