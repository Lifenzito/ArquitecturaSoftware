using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Repositorios
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly List<Usuario> usuarios;

        public RepositoryUsuario()
        {
            usuarios = new List<Usuario>();
        }

        public string AgregarUsuario(
            Usuario usuario)
        {
            try
            {
                usuarios.Add(usuario);

                return "Usuario agregado";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return usuarios;
        }

        public string Cargar(
            string ruta)
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    return "Archivo no encontrado";
                }

                string[] lineas =
                    File.ReadAllLines(ruta);

                foreach (string linea in lineas)
                {
                    string[] datos =
                        linea.Split(';');

                    Usuario usuario =
                        new Usuario(
                            datos[0],
                            datos[1],
                            datos[2],
                            datos[3],
                            datos[4],
                            datos[5]);

                    usuarios.Add(usuario);
                }

                return "Usuarios cargados";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
