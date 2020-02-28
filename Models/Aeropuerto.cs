using System;
using System.Collections.Generic;

namespace AeropuertoCalidad.Models
{
    public partial class Aeropuerto
    {
        public Aeropuerto()
        {
            Ruta = new HashSet<Ruta>();
        }

        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public bool Habilitado { get; set; }

        public virtual ICollection<Ruta> Ruta { get; set; }
    }
}
