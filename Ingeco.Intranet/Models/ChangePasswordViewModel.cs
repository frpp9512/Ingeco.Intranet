using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ingeco.Intranet.Models
{
    public class ChangePasswordViewModel
    {
        public string Fullname { get; set; }

        [Display(Name = "Correo electrónico", Prompt = "Correo electrónico (Ej. alguien@dominio.cu)", Description = "La dirección de correo electrónico que servirá además para iniciar sesión.")]
        [EmailAddress(ErrorMessage = "Debe de escribir la dirección de correo electrónico correctamente.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe de escribir correctamente la contraseña.")]
        [Display(Name = "Contraseña", Prompt = "Contraseña", Description = "La contraseña usada para el inicio de sesión.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe de confirmar correctamente la contraseña.")]
        [Display(Name = "Confirmar contraseña", Prompt = "Confirmar contraseña", Description = "Confirme la contraseña a usar para el inicio de sesión.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Debe de confirmar correctamente la contraseña.")]
        public string ConfirmPassword { get; set; }

        public string Id { get; set; }

        public bool AllowChangeEmail { get; set; } = false;
    }
}