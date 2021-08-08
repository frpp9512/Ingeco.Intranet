using SmartB1t.Security.WebSecurity.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class AccountManagamentViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int UsersPerPage { get; set; }

        public int UsersCount { get; set; }

        public bool FirstPage => CurrentPage == 1;

        public bool LastPage => CurrentPage == PagesCount;
    }
}