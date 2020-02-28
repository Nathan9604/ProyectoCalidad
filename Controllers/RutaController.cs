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
    public class RutaController : Controller
    {
        private readonly CalidadContext _context;

        public RutaController(CalidadContext context)
        {
            _context = context;
        }

        // GET: Ruta
        public async Task<IActionResult> Index()
        {
            var calidadContext = _context.Ruta.Include(r => r.CodigoaeropuertoNavigation);
            return View(await calidadContext.ToListAsync());
        }

        // GET: Ruta/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta
                .Include(r => r.CodigoaeropuertoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        // GET: Ruta/Create
        public IActionResult Create()
        {
            ViewData["Codigoaeropuerto"] = new SelectList(_context.Aeropuerto, "Codigo", "Codigo");
            return View();
        }

        // POST: Ruta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Empresa,Hora,Estado,Lugar,Capacidadmaxima,Codigoaeropuerto")] Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ruta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Codigoaeropuerto"] = new SelectList(_context.Aeropuerto, "Codigo", "Codigo", ruta.Codigoaeropuerto);
            return View(ruta);
        }

        // GET: Ruta/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta == null)
            {
                return NotFound();
            }
            ViewData["Codigoaeropuerto"] = new SelectList(_context.Aeropuerto, "Codigo", "Codigo", ruta.Codigoaeropuerto);
            return View(ruta);
        }

        // POST: Ruta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Empresa,Hora,Estado,Lugar,Capacidadmaxima,Codigoaeropuerto")] Ruta ruta)
        {
            if (id != ruta.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RutaExists(ruta.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Codigoaeropuerto"] = new SelectList(_context.Aeropuerto, "Codigo", "Codigo", ruta.Codigoaeropuerto);
            return View(ruta);
        }

        // GET: Ruta/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ruta = await _context.Ruta
                .Include(r => r.CodigoaeropuertoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (ruta == null)
            {
                return NotFound();
            }

            return View(ruta);
        }

        // POST: Ruta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ruta = await _context.Ruta.FindAsync(id);
            _context.Ruta.Remove(ruta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RutaExists(string id)
        {
            return _context.Ruta.Any(e => e.Codigo == id);
        }
    }
}
