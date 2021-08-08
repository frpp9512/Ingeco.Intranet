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
    }
}