using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionRestaurants.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionRestaurants.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantsController : BaseController
    {
        public RestaurantsController(DbContext context) : base(context)
        {
        }

        [HttpGet("[action]")]
        public IEnumerable<Restaurant> Get()
        {
            var restaurants = _db.Set<Restaurant>();
            return restaurants;
        }

        [HttpGet("[action]/{id}")]
        public Restaurant Get(int id)
        {
            var restaurant = _db.Set<Restaurant>().SingleOrDefault(r => r.Id == id);            
            return restaurant;
        }

        [HttpPost("[action]")]
        public bool Post([FromBody]Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Add(restaurant);
                _db.SaveChanges();
                return true;
            }            
            return false;
        }

        [HttpPut("[action]")]
        public bool Put([FromBody]Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _db.Update(restaurant);
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
                var restaurant = _db.Set<Restaurant>().SingleOrDefault(r => r.Id == id);
                _db.Remove(restaurant);
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
