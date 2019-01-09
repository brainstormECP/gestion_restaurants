using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionRestaurants.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionRestaurants.Controllers
{
    public class RestaurantsController : BaseController
    {
        public RestaurantsController(DbContext context) : base(context)
        {
        }

        public IActionResult Index()
        {
            var restaurants = _db.Set<Restaurant>();
            return View(restaurants);
        }

        public IActionResult Nuevo()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nuevo(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Add(restaurant);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }            
            return View(restaurant);
        }

        public IActionResult Editar(int id)
        {
            return View(_db.Set<Restaurant>().SingleOrDefault(r => r.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Update(restaurant);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        public IActionResult Eliminar(int id)
        {
            return View(_db.Set<Restaurant>().SingleOrDefault(r => r.Id == id));
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmed(int id)
        {
            try
            {
                var restaurant = _db.Set<Restaurant>().SingleOrDefault(r => r.Id == id);
                _db.Remove(restaurant);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Eliminar),new { id });
            }            
        }
    }
}
