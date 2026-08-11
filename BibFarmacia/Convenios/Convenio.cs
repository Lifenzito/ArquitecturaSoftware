using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BibFarmacia.Clases;
using BibFarmacia.Enum;
using BibFarmacia.Interfaces;

namespace BibFarmacia.Convenios
{
    public class Convenio : IEntidadConvenio
    {
        // El porcentaje es un parametro comercial: vive en el archivo de
        // configuracion, no en el codigo.
        public const string RutaConfiguracion = "convenios.txt";

        public string NombreEntidad { get; set; }
        public TipoConvenio TipoConvenio { get; set; }
        public TipoBeneficio TipoBeneficio { get; set; }

        public Convenio(string nombreEntidad,
            TipoConvenio tipoConvenio)
        {
            NombreEntidad = nombreEntidad;
            TipoConvenio = tipoConvenio;
            TipoBeneficio = TipoBeneficio.Descuento;
        }

        public decimal ObtenerPorcentaje()
        {
            try
            {
                if (!File.Exists(RutaConfiguracion))
                {
                    return 0;
                }

                string[] lineas =
                    File.ReadAllLines(RutaConfiguracion);

                foreach (string linea in lineas)
                {
                    string[] datos =
                        linea.Split(';');

                    if (datos[0] ==
                        TipoConvenio.ToString())
                    {
                        return decimal.Parse(datos[1]);
                    }
                }

                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public virtual decimal CalcularBeneficio(
            decimal precio)
        {
            return precio *
                ObtenerPorcentaje() / 100m;
        }

        public virtual bool AplicaA(
            Cliente cliente)
        {
            return cliente.Convenio != null &&
                cliente.Convenio.TipoConvenio ==
                TipoConvenio;
        }
    }
}
