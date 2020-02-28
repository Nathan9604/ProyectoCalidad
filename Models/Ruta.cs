using System;
using System.Collections.Generic;

namespace AeropuertoCalidad.Models
{
    public partial class Ruta
    {
        public Ruta()
        {
            Vuelo = new HashSet<Vuelo>();
        }

        public string Codigo { get; set; }
        public string Empresa { get; set; }
        public TimeSpan Hora { get; set; }
        public EstadoType Estado{ get; set; }
        public string Lugar { get; set; }
        public int Capacidadmaxima { get; set; }
        public string Codigoaeropuerto { get; set; }

        public virtual Aeropuerto CodigoaeropuertoNavigation { get; set; }
        public virtual ICollection<Vuelo> Vuelo { get; set; }
    }
}
