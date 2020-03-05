using System;
using System.ComponentModel.DataAnnotations;

namespace AeropuertoCalidad.Models{
    public partial class VuelosDelDia{
        public string Aeropuerto { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public bool Llegada { get; set; }
        public int Pasajeros { get; set; }
    }
}

