using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.ViewModels
{
    public class UsuarioViewModel
    {
        public string Id { get; set; }

        public string Usuario { get; set; }

        public string Nombre { get; set; }

        public List<string> Roles { get; set; }

        public bool Activo { get; set; }

        public UsuarioViewModel()
        {
            Roles = new List<string>();
        }
    }
}
