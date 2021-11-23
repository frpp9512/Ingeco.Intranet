using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class HomePageViewModel
    {
        public ForecastSummaryViewModel ForecastSummary { get; set; }
        public IEnumerable<PostViewModel> LatestPosts { get; set; }
    }
}