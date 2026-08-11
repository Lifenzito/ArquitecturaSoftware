using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;

namespace BibFarmacia.Interfaces
{
    public interface IRepositoryUsuario
    {
        string AgregarUsuario(Usuario usuario);

        string Cargar(string ruta);

        List<Usuario> ObtenerUsuarios();
    }
}
