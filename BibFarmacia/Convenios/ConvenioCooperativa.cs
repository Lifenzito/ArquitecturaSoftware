using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Enum;

namespace BibFarmacia.Convenios
{
    public class ConvenioCooperativa : Convenio
    {
        public ConvenioCooperativa(string nombreEntidad)
            : base(nombreEntidad,
                  TipoConvenio.Cooperativa)
        {
        }
    }
}
