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
                select new Pasajeros {
                    Nombre = g.Key.Nombre,
                    Fecha = g.Key.Fecha,
                    Llegada = g.Key.Estado,
                    Cantidad = g.Sum(s => s.Capacidadreal)
                };           
            return View(PasajerosPorAeropuertoPorDía);
        }
    }
}

