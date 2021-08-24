using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Debe de especificar un nombre para la categoría.")]
        [Display(Name = "Nombre de categoría", Prompt = "Nombre de categoría", Description = "El nombre de que define la categoría.")]
        public string Name { get; set; }

        [Display(Name = "Descripción", Prompt = "Descripción de la categoria", Description = "La descripción del contenido que englobará la categoría.")]
        public string Description { get; set; }

        public int PostsCount { get; set; }
    }
}
