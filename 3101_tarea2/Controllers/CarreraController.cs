using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Velocidad.Entities;
using Velocidad.Models;

namespace Velocidad.Controllers
{
    public class CarreraController : Controller
    {
        private readonly VelocidadContext _context;

        public CarreraController(VelocidadContext context)
        {
            _context = context;
        }

        // GET: Carrera
        public async Task<IActionResult> Index()
        {
            var velocidadContext = _context.Corredors.Include(c => c.IdCarreraNavigation);
            return View(await velocidadContext.ToListAsync());
        }

        // GET: Carrera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corredor = await _context.Corredors
                .Include(c => c.IdCarreraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (corredor == null)
            {
                return NotFound();
            }

            return View(corredor);
        }

        // GET: Carrera/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carrera/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarreraModel carreraModel)
        {
            var carrera = new Carrera
            {
                Descripcion = carreraModel.DescripcionCarrera
            };

            _context.Carreras.Add(carrera);
            _context.SaveChanges();

            var corredores = Array.Empty<Corredor>();

            foreach (var corredor in carreraModel.Corredor)
            {
                if (corredor.Minutos != null && corredor.Segundos != null)
                {
                    var _corredor = new Corredor
                    {
                        IdCarrera = carrera.Id,
                        Tiempo = new TimeSpan(0, (int)corredor.Minutos, (int)corredor.Segundos)
                    };
                    _context.Corredors.Add(_corredor);
                }  
            }

            await _context.SaveChangesAsync();

            return View();
        }

        // GET: Carrera/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corredor = await _context.Corredors.FindAsync(id);
            if (corredor == null)
            {
                return NotFound();
            }
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "Id", corredor.IdCarrera);
            return View(corredor);
        }

        // POST: Carrera/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCarrera,Tiempo")] Corredor corredor)
        {
            if (id != corredor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(corredor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorredorExists(corredor.Id))
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
            ViewData["IdCarrera"] = new SelectList(_context.Carreras, "Id", "Id", corredor.IdCarrera);
            return View(corredor);
        }

        // GET: Carrera/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corredor = await _context.Corredors
                .Include(c => c.IdCarreraNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (corredor == null)
            {
                return NotFound();
            }

            return View(corredor);
        }

        // POST: Carrera/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var corredor = await _context.Corredors.FindAsync(id);
            _context.Corredors.Remove(corredor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorredorExists(int id)
        {
            return _context.Corredors.Any(e => e.Id == id);
        }
    }
}
