using System;
using System.Collections.Generic;

namespace AeropuertoCalidad.Models
{
    public partial class Vuelo
    {
        public string Codigoruta { get; set; }
        public DateTime Fecha { get; set; }
        public int Capacidadreal { get; set; }

        public virtual Ruta CodigorutaNavigation { get; set; }
    }
}
