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

         // GET: Vuelo/Details/5
        public IActionResult Consulta(DateTime fechaInicial, DateTime fechaFinal){
           var Pasajeros = from v in _context.Vuelo join r in _context.Ruta 
                            on v.Codigoruta equals r.Codigo join a in _context.Aeropuerto 
                            on r.Codigoaeropuerto equals a.Codigo
                            where v.Fecha >= fechaInicial && v.Fecha <= fechaFinal
                            orderby v.Fecha
                            select new {Fecha = v.Fecha, Capacidad = v.Capacidadreal, Nombre = a.Nombre, Estado = r.Estado};
            foreach(var p in Pasajeros){
                Console.Write(p.Nombre + "(");
                Console.Write(p.Fecha + "): ");
                Console.Write(p.Capacidad);
                Console.WriteLine();
                int partieronGeneral = 0;
                int llegaronGeneral = 0;

                if(p.Estado){
                    llegaronGeneral += p.Capacidad;
                }else{
                    partieronGeneral += p.Capacidad;
                }
            }
            Console.WriteLine("General");
            Console.WriteLine("Llegaron: " + llegaronGeneral);
            Console.WriteLine("Partieron: " + partieronGeneral);
            
            var PasajerosPorDía = from v in _context.Vuelo join r in _context.Ruta 
                                on v.Codigoruta equals r.Codigo join a in _context.Aeropuerto 
                                on r.Codigoaeropuerto equals a.Codigo into j
                                where v.Fecha >= fechaInicial && v.Fecha <= fechaFinal
                                group j by j.Fecha into g 
                                select new {Fecha = v.Fecha, Capacidad = v.Capacidadreal, Nombre = a.Nombre, Estado = r.Estado};

            
            
            return View(Pasajeros);
        }

        public int = 

    }
}

