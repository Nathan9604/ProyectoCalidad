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
            var PasajerosPorAeropuertoPorDía = from v in _context.Vuelo join r in _context.Ruta on v.Codigoruta equals r.Codigo join a in _context.Aeropuerto on r.Codigoaeropuerto equals a.Codigo 
                                               where v.Fecha >= fechaInicial && v.Fecha <= fechaFinal
                                               group v by new {a.Nombre, v.Fecha, r.Estado} into g
                                               select new {
                                                Nombre = g.Key.Nombre,
                                                Fecha = g.Key.Fecha,
                                                Estado = g.Key.Estado?"Llegada":"Salida",
                                                Capacidad = g.Sum(s => s.Capacidadreal)
                                               };           
            int llegaronGeneral = 0;
            int partieronGeneral = 0;
            var llegaronGeneralPorAeropueto = new Dictionary<string,int>();
            var partieronGeneralPorAeropuerto = new Dictionary<string,int>();
            
        	Console.WriteLine("Pasajeros por aeropuerto por día:");
            
            foreach(var p in PasajerosPorAeropuertoPorDía){
                Console.Write(p.Nombre + "(");
                Console.Write(p.Fecha + ")[");
                Console.Write(p.Estado + "]: ");
                Console.Write(p.Capacidad);
                Console.WriteLine();

                if(!llegaronGeneralPorAeropueto.ContainsKey(p.Nombre)){
                    llegaronGeneralPorAeropueto.Add(p.Nombre,0);
                    partieronGeneralPorAeropuerto.Add(p.Nombre,0);
                }

                if(p.Estado == "Llegada"){
                    llegaronGeneral += p.Capacidad;
                    llegaronGeneralPorAeropueto[p.Nombre] += p.Capacidad;
                }else{
                    partieronGeneral += p.Capacidad;
                    partieronGeneralPorAeropuerto[p.Nombre] += p.Capacidad;
                }
            }


            Console.WriteLine("General");
            Console.WriteLine("Llegaron: " + llegaronGeneral);
            Console.WriteLine("Partieron: " + partieronGeneral);

            foreach(var aeropuerto in llegaronGeneralPorAeropueto){
                Console.WriteLine(aeropuerto.Key);
                Console.WriteLine("Llegaron: " + aeropuerto.Value);
                Console.WriteLine("Partieron: " + partieronGeneralPorAeropuerto[aeropuerto.Key]);
            }
        
            return View(PasajerosPorAeropuertoPorDía);
        }
    }
}

