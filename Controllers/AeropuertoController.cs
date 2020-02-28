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
    public class AeropuertoController : Controller
    {
        private readonly CalidadContext _context;

        public AeropuertoController(CalidadContext context)
        {
            _context = context;
        }

        // GET: Aeropuerto
        public async Task<IActionResult> Index()
        {
            return View(await _context.Aeropuerto.ToListAsync());
        }

        // GET: Aeropuerto/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aeropuerto = await _context.Aeropuerto
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (aeropuerto == null)
            {
                return NotFound();
            }

            return View(aeropuerto);
        }

        // GET: Aeropuerto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aeropuerto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nombre,Direccion,Habilitado")] Aeropuerto aeropuerto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aeropuerto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aeropuerto);
        }

        // GET: Aeropuerto/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aeropuerto = await _context.Aeropuerto.FindAsync(id);
            if (aeropuerto == null)
            {
                return NotFound();
            }
            return View(aeropuerto);
        }

        // POST: Aeropuerto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Nombre,Direccion,Habilitado")] Aeropuerto aeropuerto)
        {
            if (id != aeropuerto.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aeropuerto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AeropuertoExists(aeropuerto.Codigo))
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
            return View(aeropuerto);
        }

        // GET: Aeropuerto/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aeropuerto = await _context.Aeropuerto
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (aeropuerto == null)
            {
                return NotFound();
            }

            return View(aeropuerto);
        }

        // POST: Aeropuerto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aeropuerto = await _context.Aeropuerto.FindAsync(id);
            _context.Aeropuerto.Remove(aeropuerto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AeropuertoExists(string id)
        {
            return _context.Aeropuerto.Any(e => e.Codigo == id);
        }
    }
}
