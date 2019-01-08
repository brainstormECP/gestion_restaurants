using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Models
{
    public class HorarioDeReserva
    {
        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public TimeSpan Hora { get; set; }
    }
}
