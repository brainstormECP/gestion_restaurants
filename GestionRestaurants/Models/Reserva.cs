using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int HoraId { get; set; }

        public virtual HorarioDeReserva Hora { get; set; }

        public int CantidadDePersonas { get; set; }

        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public string Habitacion { get; set; }
    }
}
