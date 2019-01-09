using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.ViewModels
{
    public class DetalleDeReservaViewModel
    {
        public string Hora { get; set; }

        public List<ReservaInfoViewModel> Reservas { get; set; }

        public DetalleDeReservaViewModel()
        {
            Reservas = new List<ReservaInfoViewModel>();
        }
    }
}
