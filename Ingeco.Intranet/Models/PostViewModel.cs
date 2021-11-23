using Ingeco.Intranet.Data.Models;
using Microsoft.AspNetCore.Http;
using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class PostViewModel
    {
        [Display(AutoGenerateField = false)]
        public Guid Id { get; set; }

        public Guid CategorySelected { get; set; }

        public CategoryViewModel Category { get; set; }

        [Required(ErrorMessage = "Debe de especificar el título de la publicación")]
        [Display(Name = "Título", Prompt = "Título de la publicación", Description = "El título de la publicación")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Debe de especificar una descripción para la publicación")]
        [Display(Name = "Descripción", Prompt = "Descripción de la publicación", Description = "Una muy breve reseña del contenido de la publicación o subtítulo")]
        public string Description { get; set; }

        [Display(AutoGenerateField = false)]
        public DateTimeOffset Created { get; set; }

        [Display(Name = "Cuerpo de la publicación", Prompt = "Contenido de la publicación", Description = "Todo el contenido de la publicación.")]
        public string Body { get; set; }

        public IEnumerable<WebMediaViewModel> Media { get; set; }

        public WebMediaViewModel Cover => Media?.Any(m => m.IsCover) is true ? Media.First(m => m.IsCover) : Media?.FirstOrDefault();

        public string TagsLine { get; set; }

        public User PostedBy { get; set; }

        public bool Public { get; set; }

        public string[] GetTagArray() => TagsLine?.Split(";");

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public int TotalCommentsCount { get; set; }
    }
}