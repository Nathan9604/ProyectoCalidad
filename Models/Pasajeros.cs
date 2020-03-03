using System;
// using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeropuertoCalidad.Models{
    public partial class Pasajeros{
        public string Nombre { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public bool Llegada { get; set; }
        public int Cantidad { get; set; }
    }
}

