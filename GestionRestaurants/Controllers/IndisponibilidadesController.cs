using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionRestaurants.Data;
using GestionRestaurants.Models;

namespace GestionRestaurants.Controllers
{
    public class IndisponibilidadesController : BaseController
    {
        public IndisponibilidadesController(DbContext context) : base(context)
        {
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var indisponibilidades = _db.Set<IndisponibilidadRestaurant>()
                .Include(r => r.Restaurant);
            return View(await indisponibilidades.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indisponibilidad = await _db.Set<IndisponibilidadRestaurant>()
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indisponibilidad == null)
            {
                return NotFound();
            }

            return View(indisponibilidad);
        }

        // GET: Reservas/Create
        public IActionResult Nuevo()
        {
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nuevo([Bind("Id,FechaInicio,FechaFin,RestaurantId")] IndisponibilidadRestaurant indisponibilidad)
        {
            if (ModelState.IsValid)
            {
                _db.Add(indisponibilidad);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }            
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", indisponibilidad.RestaurantId);
            return View(indisponibilidad);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indisponibilidad = await _db.Set<IndisponibilidadRestaurant>().FindAsync(id);
            if (indisponibilidad == null)
            {
                return NotFound();
            }            
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", indisponibilidad.RestaurantId);
            return View(indisponibilidad);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,FechaInicio,FechaFin,RestaurantId")] IndisponibilidadRestaurant indisponibilidad)
        {
            if (id != indisponibilidad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(indisponibilidad);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IndisponibilidadExists(indisponibilidad.Id))
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
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", indisponibilidad.RestaurantId);
            return View(indisponibilidad);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var indisponibilidad = await _db.Set<IndisponibilidadRestaurant>()
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (indisponibilidad == null)
            {
                return NotFound();
            }

            return View(indisponibilidad);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var indisponibilidad = await _db.Set<IndisponibilidadRestaurant>().FindAsync(id);
            _db.Set<IndisponibilidadRestaurant>().Remove(indisponibilidad);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IndisponibilidadExists(int id)
        {
            return _db.Set<IndisponibilidadRestaurant>().Any(e => e.Id == id);
        }
    }
}
