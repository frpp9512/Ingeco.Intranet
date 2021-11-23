using Ingeco.Intranet.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class WebMediaViewModel
    {
        [Display(AutoGenerateField = false)]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Tipo de medio", Description = "Define el tipo de medio (video o imágen)")]
        public WebMediaType MediaType { get; set; }

        [Display(Name = "Descripción", Description = "La descripción del medio.")]
        public string Description { get; set; }

        [Display(Name = "Es portada", Description = "Define si el medio aparecerá como portada.")]
        public bool IsCover { get; set; }

        [Display(AutoGenerateField = false)]
        public Guid PostId { get; set; }

        [Display(AutoGenerateField = false)]
        public string Filename { get; set; }
    }
}