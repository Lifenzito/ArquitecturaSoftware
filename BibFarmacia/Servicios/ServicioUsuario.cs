using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    public class ServicioUsuario
    {
        private readonly IRepositoryUsuario repositoryUsuario;

        public ServicioUsuario(
            IRepositoryUsuario repositoryUsuario)
        {
            this.repositoryUsuario = repositoryUsuario;
        }

        public string AgregarUsuario(
            Usuario usuario)
        {
            return repositoryUsuario
                .AgregarUsuario(usuario);
        }

        public string Cargar(
            string ruta)
        {
            return repositoryUsuario.Cargar(ruta);
        }
    }
}
