using Ingeco.Intranet.Models;
using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Data.Models
{
    public static class Extensions
    {
        public static User GetModel(this CreateUserViewModel viewModel)
            => new()
            {
                Fullname = viewModel.Fullname,
                Department = viewModel.Department,
                Position = viewModel.Position,
                Email = viewModel.Email,
                Active = true
            };

        public static EditUserViewModel GetEditViewModel(this User user)
            => new() 
            {
                Id = user.Id.ToString(),
                Fullname = user.Fullname,
                Email = user.Email,
                Department = user.Department,
                Position = user.Position,
                RolesSelected = user.Roles.Select(ur => ur.Role.Id.ToString()).ToArray(),
                ProfilePictureId = user.Id.ToString()
            };


        public static CategoryViewModel GetViewModel(this Category category)
            => new() 
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

        public static Category GetModel(this CategoryViewModel viewModel)
            => new()
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description
            };
    }
}