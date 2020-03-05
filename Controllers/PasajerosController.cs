using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AeropuertoCalidad.Models;

namespace AeropuertoCalidad.Controllers
{
    public class PasajerosController : Controller
    {
        private readonly CalidadContext _context;

        public PasajerosController(CalidadContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Consulta(DateTime fechaInicial, DateTime fechaFinal){
            var PasajerosPorAeropuertoPorDía = 
                from v in _context.Vuelo 
                join r in _context.Ruta on v.Codigoruta equals r.Codigo 
                join a in _context.Aeropuerto on r.Codigoaeropuerto equals a.Codigo 
                where v.Fecha >= fechaInicial && v.Fecha <= fechaFinal
                group v by new {a.Nombre, v.Fecha, r.Estado} into g
                select new VuelosDelDia {
                    Aeropuerto = g.Key.Nombre,
                    Fecha = g.Key.Fecha,
                    Llegada = g.Key.Estado,
                    Pasajeros = g.Sum(s => s.Capacidadreal)
                };           

                DatosPasajeros dp = new DatosPasajeros();
                dp.FechaInicial = fechaInicial;
                dp.FechaFinal = fechaFinal;
                dp.VuelosSemanales = procesarConsulta(PasajerosPorAeropuertoPorDía);
            return View(dp);
        }

        public IEnumerable<VuelosPorDiaSemanal> procesarConsulta(IEnumerable<VuelosDelDia> vuelosDiaros){
            var diaSemana = new Dictionary<int,string> {{0,"Lunes"}, {1,"Martes"}, {2,"Miércoles"}, {3,"Jueves"}, {4,"Viernes"}, {5,"Sábado"}, {6,"Domingo"}};
            var aeropuertos = new Dictionary<string,int>();
            var vuelosSemanales = new List<VuelosPorDiaSemanal>();
            
            //Inicializa conteo general por día
            if(vuelosDiaros.Count() > 0){
                aeropuertos.Add("Todos los aeropuertos",0);
                for(int i = 0; i < diaSemana.Count(); i++){
                    vuelosSemanales.Add(new VuelosPorDiaSemanal("Todos los aeropuertos",diaSemana[i]));
                }
                vuelosSemanales.Add(new VuelosPorDiaSemanal("Todos los aeropuertos","Todos los días"));
            }
            
            foreach(var vuelosDelDia in vuelosDiaros){
                //Inicializa pasajeros por día de la semana por aeropuerto
                if(!aeropuertos.ContainsKey(vuelosDelDia.Aeropuerto)){
                    aeropuertos.Add(vuelosDelDia.Aeropuerto,aeropuertos.Count());
                    for(int i = 0; i < diaSemana.Count(); i++){
                        vuelosSemanales.Add(new VuelosPorDiaSemanal(vuelosDelDia.Aeropuerto,diaSemana[i]));
                    }
                    vuelosSemanales.Add(new VuelosPorDiaSemanal(vuelosDelDia.Aeropuerto,"Todos los días"));
                }
                //Rellena datos diarios
                if(vuelosDelDia.Llegada){
                    vuelosSemanales[aeropuertos[vuelosDelDia.Aeropuerto]*(diaSemana.Count()+1)+((int)vuelosDelDia.Fecha.DayOfWeek+6)%7].addLlegada(vuelosDelDia.Pasajeros);// dias/aeropuerto
                }else{
                    vuelosSemanales[aeropuertos[vuelosDelDia.Aeropuerto]*(diaSemana.Count()+1)+((int)vuelosDelDia.Fecha.DayOfWeek+6)%7].addSalida(vuelosDelDia.Pasajeros);// dias/aeropuerto
                }
            }

            //Suma los datos de la semana y por día como datos generales
            for(int i = 0; i < aeropuertos.Count(); i++){
                for(int j = 0; j < diaSemana.Count()+1; j++){
                    int llegada = vuelosSemanales[i*(diaSemana.Count()+1)+j].Llegada;
                    int salida = vuelosSemanales[i*(diaSemana.Count()+1)+j].Salida;
                    int CantidadLlegadas = vuelosSemanales[i*(diaSemana.Count()+1)+j].CantidadLlegadas;
                    int CantidadSalidas = vuelosSemanales[i*(diaSemana.Count()+1)+j].CantidadSalidas;
                    vuelosSemanales[j].addLlegada(llegada,CantidadLlegadas);
                    vuelosSemanales[j].addSalida(salida,CantidadSalidas);
                    vuelosSemanales[i*(diaSemana.Count()+1)+j].setPromedios();
                    if(j != 7){
                        vuelosSemanales[i*(diaSemana.Count()+1)+7].addLlegada(llegada,CantidadLlegadas);
                        vuelosSemanales[i*(diaSemana.Count()+1)+7].addSalida(salida,CantidadSalidas);
                    }
                }
            }

            for(int i = 0; i < diaSemana.Count()+1;i++){
                vuelosSemanales[i].setPromedios();
            }

            for(int i = 0; i < aeropuertos.Count(); i++){
                for(int j = 0; j < diaSemana.Count()+1; j++){
                    vuelosSemanales[i*(diaSemana.Count()+1)+j].debug();
                }
            }
            Console.WriteLine(vuelosSemanales.Count());
            return vuelosSemanales;
        }
    }
}

