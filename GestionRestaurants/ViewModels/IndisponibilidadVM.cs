namespace GestionRestaurants.ViewModels
{
    public class IndisponibilidadVM
    {

        public int Id { get; set; }

        public string FechaInicio { get; set; }

        public string FechaFin { get; set; }

        public int RestaurantId { get; set; }

        public string Observaciones { get; set; }
    }
}
