using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.Models
{
    public class Usuario:IdentityUser
    {
        public bool Activo { get; set; }

        public string Nombre { get; set; }
    }
}
