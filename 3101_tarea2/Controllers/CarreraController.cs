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
            var carreraModel = _context.Carreras
                .Join(_context.Corredors, ca => ca.Id, co => co.IdCarrera,
                (ca, co) => new  { Carrera = ca, Corredor = co })
                .GroupBy(c => new { c.Carrera.Id, c.Carrera.Descripcion })
                .Select(c => new CarreraIndexViewModel { Id = c.Key.Id, Descripcion = c.Key.Descripcion, TotalCorredores = c.Count() })
                .ToListAsync();

            return View(await carreraModel);
        }

        // GET: Carrera/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var corredores = await _context.Corredors
                .Where(c => c.IdCarrera == id)
                .Select(c => new CorredorDetalle
                {
                    Id = c.Id,
                    Tiempo = c.Tiempo.Value,
                    Velocidad = (decimal)(2000 / c.Tiempo.Value.TotalSeconds)
                })
                .ToListAsync();

            if (corredores == null)
            {
                return NotFound();
            }

            var carreraDetalle = new CarreraDetailViewModel
            {
                Id = id.Value,
                CorredorDetalle = corredores,
                MayorVelocidad = 0,
                MenorVelocidad = 3600,
                PromedioVelocidad = 0
            };

            decimal sumaVelocidad = 0;

            foreach (var corredor in corredores)
            {
                sumaVelocidad += corredor.Velocidad;

                if (corredor.Velocidad > carreraDetalle.MayorVelocidad)
                {
                    carreraDetalle.MayorVelocidad = corredor.Velocidad;
                }

                if (corredor.Velocidad < carreraDetalle.MenorVelocidad)
                {
                    carreraDetalle.MenorVelocidad = corredor.Velocidad;
                }
            }

            carreraDetalle.PromedioVelocidad = sumaVelocidad / corredores.Count();

            return View(carreraDetalle);
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
        public async Task<IActionResult> Create(CarreraViewModel carreraViewModel)
        {
            var carrera = new Carrera
            {
                Descripcion = carreraViewModel.DescripcionCarrera
            };

            _context.Carreras.Add(carrera);
            _context.SaveChanges();

            var corredores = Array.Empty<Corredor>();

            foreach (var corredor in carreraViewModel.Corredor)
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

            return RedirectToAction(nameof(Index));
        }

        private bool CarreraExists(int id)
        {
            return _context.Carreras.Any(e => e.Id == id);
        }
    }
}
