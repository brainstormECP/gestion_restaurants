using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionRestaurants.Models;
using GestionRestaurants.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionRestaurants.Controllers
{
    [Route("api/[controller]")]
    public class IndisponiblesController : BaseController
    {
        public IndisponiblesController(DbContext context) : base(context)
        {
        }

        [HttpGet("[action]")]
        public IEnumerable<IndisponibilidadRestaurant> Get()
        {
            var indisponibles = _db.Set<IndisponibilidadRestaurant>().Include(i => i.Restaurant);
            return indisponibles;
        }

        [HttpGet("[action]/{id}")]
        public IndisponibilidadVM Get(int id)
        {
            var indisponible = _db.Set<IndisponibilidadRestaurant>()
                .Include(i => i.Restaurant)
                .SingleOrDefault(r => r.Id == id);            
            return new IndisponibilidadVM {
                Id = indisponible.Id,
                FechaInicio = indisponible.FechaInicio.ToShortDateString(),
                FechaFin = indisponible.FechaFin.ToShortDateString(),
                RestaurantId = indisponible.RestaurantId,
                Observaciones = indisponible.Observaciones,
            };
        }

        [HttpPost("[action]")]
        public bool Post([FromBody]IndisponibilidadRestaurant indisponible)
        {
            if (ModelState.IsValid)
            {
                _db.Add(indisponible);
                _db.SaveChanges();
                return true;
            }            
            return false;
        }

        [HttpPut("[action]")]
        public bool Put([FromBody]IndisponibilidadRestaurant indisponible)
        {
            if (ModelState.IsValid)
            {
                _db.Update(indisponible);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpDelete("[action]/{id}")]
        public bool Delete(int id)
        {
            try
            {
                var indisponible = _db.Set<IndisponibilidadRestaurant>().SingleOrDefault(r => r.Id == id);
                _db.Remove(indisponible);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }
    }
}
