using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Models
{
    public class IndisponibilidadRestaurant
    {
        public int Id { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public string Observaciones { get; set; }
    }
}
