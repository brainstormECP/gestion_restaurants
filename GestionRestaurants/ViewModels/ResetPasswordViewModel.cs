using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionRestaurants.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres y como maximo {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma nueva Contraseña")]
        [Compare("NewPassword", ErrorMessage = "No coinciden la contraseña y la confirmacion.")]
        public string ConfirmPassword { get; set; }
    }
}
