using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Interfaces;

namespace BibFarmacia.Servicios
{
    public class ServicioAutenticacion : IServicioAutenticacion
    {
        private readonly IRepositoryUsuario repositoryUsuario;

        public ServicioAutenticacion(
            IRepositoryUsuario repositoryUsuario)
        {
            this.repositoryUsuario = repositoryUsuario;
        }

        public bool Login(
            string user,
            string password)
        {
            return repositoryUsuario
                .ObtenerUsuarios()
                .Any(u =>
                    u.UserName == user &&
                    u.Password == password);
        }
    }
}
