using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class PostsPageViewModel
    {
        public int PageNumber { get; set; }
        public int PageTotal { get; set; }
        public int PostsPerPage { get; set; }
        public IEnumerable<PostViewModel> PostsList { get; set; }
    }
}