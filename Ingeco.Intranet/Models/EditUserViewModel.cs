using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ingeco.Intranet.Models
{
    public class EditUserViewModel
    {
        public string ProfilePictureId { get; set; }

        public string ProfilePictureFileName { get; set; }

        public string Id { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage = "Debe de escribir en nombre completo del usuario")]
        [Display(Name = "Nombre completo", Description = "El nombre completo del usuario.", Prompt = "Nombre completo")]
        public string Fullname { get; set; }

        [Display(Name = "Cargo", Description = "El cargo que ocupa el usuario en la empresa.", Prompt = "Cargo que desempeña")]
        public string Position { get; set; }

        [Display(Name = "Departamento", Description = "Deparamento en el que trabajo en usuario.", Prompt = "Departamento o área")]
        public string Department { get; set; }
        
        public string[] RolesSelected { get; set; }

        public IEnumerable<RoleViewModel> RoleList { get; set; }
    }
}