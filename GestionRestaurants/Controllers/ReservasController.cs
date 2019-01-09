using GestionRestaurants.Models;
using GestionRestaurants.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Controllers
{
    public class ReservasController : BaseController
    {
        public ReservasController(DbContext context) : base(context)
        {
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var fechaFin = DateTime.Now.AddDays(5);
            var grupo = _db.Set<Reserva>()
                .Include(r => r.Hora)
                .Include(r => r.Hora.Restaurant)
                .Include(r => r.Restaurant)
                .Where(r => r.Fecha >= DateTime.Now.Date && r.Fecha <= fechaFin.Date)
                .ToList()
                .GroupBy(r => r.Fecha.Date);
            var reservas = grupo.SelectMany(r => r.GroupBy(e => e.Restaurant).Select(e =>
                new ReservaViewModel
                {
                    Fecha = r.Key,
                    Restaurant = e.Key.Nombre,
                    RestaurantId = e.Key.Id,
                    Detalles = e.GroupBy(d => d.Hora.Hora).Select(d => new DetalleDeReservaViewModel
                    {
                        Hora = d.Key.ToString(),
                        Reservas = d.Select(t => new ReservaInfoViewModel
                        {
                            Habitacion = t.Habitacion,
                            Pax = t.CantidadDePersonas,
                            ReservaId = t.Id
                        }).ToList()
                    }).ToList()
                }));
            return View(reservas.ToList());
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
            ViewData["HoraId"] = new SelectList(_db.Set<HorarioDeReserva>(), "Id", "Hora");
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Nuevo([Bind("Id,Fecha,HoraId,CantidadDePersonas,RestaurantId,Habitacion")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _db.Add(reserva);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HoraId"] = new SelectList(_db.Set<HorarioDeReserva>(), "Id", "Hora", reserva.HoraId);
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", reserva.RestaurantId);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reserva = await _db.Set<Reserva>().FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            ViewData["HoraId"] = new SelectList(_db.Set<HorarioDeReserva>(), "Id", "Id", reserva.HoraId);
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", reserva.RestaurantId);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, [Bind("Id,Fecha,HoraId,CantidadDePersonas,RestaurantId,Habitacion")] Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(reserva);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaExists(reserva.Id))
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
            ViewData["HoraId"] = new SelectList(_db.Set<HorarioDeReserva>(), "Id", "Id", reserva.HoraId);
            ViewData["RestaurantId"] = new SelectList(_db.Set<Restaurant>(), "Id", "Nombre", reserva.RestaurantId);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Eliminar(int? id)
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

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmed(int id)
        {
            var reserva = await _db.Set<Reserva>().FindAsync(id);
            _db.Set<Reserva>().Remove(reserva);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaExists(int id)
        {
            return _db.Set<Reserva>().Any(e => e.Id == id);
        }
    }
}
