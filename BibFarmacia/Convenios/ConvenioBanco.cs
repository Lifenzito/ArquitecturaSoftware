using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Enum;

namespace BibFarmacia.Convenios
{
    public class ConvenioBanco : Convenio
    {
        public ConvenioBanco(string nombreEntidad)
            : base(nombreEntidad,
                  TipoConvenio.Banco)
        {
        }
    }
}
