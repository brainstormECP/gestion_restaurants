using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.ViewModels
{
    public class ReservaViewModel
    {
        public DateTime Fecha { get; set; }

        public string Restaurant { get; set; }

        public int RestaurantId { get; set; }

        public List<DetalleDeReservaViewModel> Detalles { get; set; }

        public ReservaViewModel()
        {
            Detalles = new List<DetalleDeReservaViewModel>();
        }
    }    
}
