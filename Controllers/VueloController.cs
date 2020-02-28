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
    public class VueloController : Controller
    {
        private readonly CalidadContext _context;

        public VueloController(CalidadContext context)
        {
            _context = context;
        }

        // GET: Vuelo
        public async Task<IActionResult> Index()
        {
            var calidadContext = _context.Vuelo.Include(v => v.CodigorutaNavigation);
            return View(await calidadContext.ToListAsync());
        }

        // GET: Vuelo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelo
                .Include(v => v.CodigorutaNavigation)
                .FirstOrDefaultAsync(m => m.Codigoruta == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // GET: Vuelo/Create
        public IActionResult Create()
        {
            ViewData["Codigoruta"] = new SelectList(_context.Ruta, "Codigo", "Codigo");
            return View();
        }

        // POST: Vuelo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigoruta,Fecha,Capacidadreal")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vuelo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Codigoruta"] = new SelectList(_context.Ruta, "Codigo", "Codigo", vuelo.Codigoruta);
            return View(vuelo);
        }

        // GET: Vuelo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelo.FindAsync(id);
            if (vuelo == null)
            {
                return NotFound();
            }
            ViewData["Codigoruta"] = new SelectList(_context.Ruta, "Codigo", "Codigo", vuelo.Codigoruta);
            return View(vuelo);
        }

        // POST: Vuelo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigoruta,Fecha,Capacidadreal")] Vuelo vuelo)
        {
            if (id != vuelo.Codigoruta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vuelo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VueloExists(vuelo.Codigoruta))
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
            ViewData["Codigoruta"] = new SelectList(_context.Ruta, "Codigo", "Codigo", vuelo.Codigoruta);
            return View(vuelo);
        }

        // GET: Vuelo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vuelo = await _context.Vuelo
                .Include(v => v.CodigorutaNavigation)
                .FirstOrDefaultAsync(m => m.Codigoruta == id);
            if (vuelo == null)
            {
                return NotFound();
            }

            return View(vuelo);
        }

        // POST: Vuelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vuelo = await _context.Vuelo.FindAsync(id);
            _context.Vuelo.Remove(vuelo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VueloExists(string id)
        {
            return _context.Vuelo.Any(e => e.Codigoruta == id);
        }
    }
}
