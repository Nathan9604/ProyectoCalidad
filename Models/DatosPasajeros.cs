using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AeropuertoCalidad.Models{
    public partial class DatosPasajeros{
        public IEnumerable<VuelosPorDiaSemanal> VuelosSemanales { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }
    }
}
