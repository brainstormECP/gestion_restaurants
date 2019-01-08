using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Models
{
    public class Restaurant
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
