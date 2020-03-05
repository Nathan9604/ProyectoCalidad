using System;
using System.ComponentModel.DataAnnotations;

namespace AeropuertoCalidad.Models{
    public partial class VuelosPorDiaSemanal{
        public VuelosPorDiaSemanal(string aeropuerto, string dia){
            this.Aeropuerto = aeropuerto;
            this.Dia = dia;
            Llegada = 0;
            CantidadLlegadas = 0;
            Salida = 0;
            CantidadSalidas = 0;
        }

        public void addLlegada(int pasajeros){
            Llegada+=pasajeros;
            CantidadLlegadas++;
        }
        public void addSalida(int pasajeros){
            Salida+=pasajeros;
            CantidadSalidas++;
        }

        public void addLlegada(int pasajeros, int dias){
            Llegada+=pasajeros;
            CantidadLlegadas+=dias;
        }
        public void addSalida(int pasajeros, int dias){
            Salida+=pasajeros;
            CantidadSalidas+=dias;
        }

        public void setPromedios(){
            if(CantidadLlegadas!=0)PromedioLlegada = (int)Llegada/CantidadLlegadas;
            if(CantidadSalidas!=0)PromedioSalida = (int)Salida/CantidadSalidas;
        }
        public void debug(){
            Console.WriteLine(Aeropuerto + " " + Dia + " (" + Llegada + ":" + CantidadLlegadas +":"+PromedioLlegada+ ") (" + Salida + ":" +CantidadSalidas+":"+PromedioSalida+")");
        }
        public string Aeropuerto { get; set; }
        public string Dia { get; set; }
        public int Llegada { get; set; }
        public int CantidadLlegadas { get; set; }
        public int PromedioLlegada { get; set; }
        public int Salida { get; set; }
        public int CantidadSalidas { get; set; }
        public int PromedioSalida { get; set; }
    }
}