using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ingeco.Intranet.Models
{
    public class CreateUserViewModel
    {
        //[DataType(DataType.Upload)]
        //public IFormFile ProfilePicture { get; set; }
        public string ProfilePictureId { get; set; }

        public string ProfilePictureFileName { get; set; }

        [Required(ErrorMessage = "Debe de escribir en nombre completo del usuario")]
        [Display(Name = "Nombre completo", Description = "El nombre completo del usuario.", Prompt = "Nombre completo")]
        public string Fullname { get; set; }
        
        [Display(Name = "Cargo", Description = "El cargo que ocupa el usuario en la empresa.", Prompt = "Cargo que desempeña")]
        public string Position { get; set; }

        [Display(Name = "Departamento", Description = "Deparamento en el que trabajo en usuario.", Prompt = "Departamento o área")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Debe de escribir la dirección de correo electrónico para iniciar sesión.")]
        [Display(Name = "Correo electrónico", Prompt = "Correo electrónico (Ej. alguien@dominio.cu)", Description = "La dirección de correo electrónico con que se registró el usuario.")]
        [EmailAddress(ErrorMessage = "Debe de escribir la dirección de correo electrónico correctamente.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe de escribir correctamente la contraseña para registrar el usuario.")]
        [Display(Name = "Contraseña", Prompt = "Contraseña", Description = "La contraseña a usar para el inicio de sesión.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "La contraseña debe de tener al menos 8 caractéres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe de confirmar correctamente la contraseña para registrar el usuario.")]
        [Display(Name = "Confirmar contraseña", Prompt = "Confirmar contraseña", Description = "Confirme la contraseña a usar para el inicio de sesión.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Debe de confirmar correctamente la contraseña.")]
        public string ConfirmPassword { get; set; }

        public string[] RolesSelected { get; set; }

        public IEnumerable<RoleViewModel> RoleList { get; set; }
    }
}