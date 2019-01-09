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
    public class RestriccionesController : BaseController
    {
        public RestriccionesController(DbContext context) : base(context)
        {
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var restricciones = _db.Set<RestriccionDeReserva>()
                .Include(r => r.Restaurant);
            return View(await restricciones.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _db.Set<RestriccionDeReserva>()                
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reserva == null)
            {
                return NotFound();
            }

            return View(reserva);
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
        public async Task<IActionResult> Nuevo([Bind("Id,DisponibilidadDeMesa,RestaurantId")] RestriccionDeReserva restriccion)
        {
            if (ModelState.IsValid)
            {
                _db.Add(restriccion);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }            
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", restriccion.RestaurantId);
            return View(restriccion);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restriccion = await _db.Set<RestriccionDeReserva>().FindAsync(id);
            if (restriccion == null)
            {
                return NotFound();
            }            
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", restriccion.RestaurantId);
            return View(restriccion);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,DisponibilidadDeMesa,RestaurantId")] RestriccionDeReserva restriccion)
        {
            if (id != restriccion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(restriccion);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestriccionExists(restriccion.Id))
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
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", restriccion.RestaurantId);
            return View(restriccion);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restriccion = await _db.Set<RestriccionDeReserva>()
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restriccion == null)
            {
                return NotFound();
            }

            return View(restriccion);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var restriccion = await _db.Set<RestriccionDeReserva>().FindAsync(id);
            _db.Set<RestriccionDeReserva>().Remove(restriccion);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestriccionExists(int id)
        {
            return _db.Set<RestriccionDeReserva>().Any(e => e.Id == id);
        }
    }
}
