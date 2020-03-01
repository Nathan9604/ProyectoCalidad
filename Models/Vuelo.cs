using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeropuertoCalidad.Models
{
    public partial class Vuelo
    {
        public string Codigoruta { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public int Capacidadreal { get; set; }

        public virtual Ruta CodigorutaNavigation { get; set; }
    }
}
