using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class CategoriesIndexViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public int CategoriesTotal => Categories.Count();
    }
}
