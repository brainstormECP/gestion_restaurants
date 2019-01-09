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
    public class HorariosController : BaseController
    {
        public HorariosController(DbContext context) : base(context)
        {
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var reservas = _db.Set<HorarioDeReserva>()
                .Include(r => r.Restaurant);
            return View(await reservas.ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _db.Set<Reserva>()
                .Include(r => r.Hora)
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
        public async Task<IActionResult> Nuevo([Bind("Id,Hora,RestaurantId")] HorarioDeReserva horario)
        {
            if (ModelState.IsValid)
            {
                _db.Add(horario);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }            
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", horario.RestaurantId);
            return View(horario);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _db.Set<HorarioDeReserva>().FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }            
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", horario.RestaurantId);
            return View(horario);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Hora,RestaurantId")] HorarioDeReserva horario)
        {
            if (id != horario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(horario);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorarioExists(horario.Id))
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
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", horario.RestaurantId);
            return View(horario);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horario = await _db.Set<HorarioDeReserva>()
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (horario == null)
            {
                return NotFound();
            }

            return View(horario);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var horario = await _db.Set<HorarioDeReserva>().FindAsync(id);
            _db.Set<HorarioDeReserva>().Remove(horario);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorarioExists(int id)
        {
            return _db.Set<HorarioDeReserva>().Any(e => e.Id == id);
        }
    }
}
