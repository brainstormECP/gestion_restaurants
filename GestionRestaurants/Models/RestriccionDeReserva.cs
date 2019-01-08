using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Models
{
    public class RestriccionDeReserva
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public int DisponibilidadDeMesa { get; set; }
    }
}
