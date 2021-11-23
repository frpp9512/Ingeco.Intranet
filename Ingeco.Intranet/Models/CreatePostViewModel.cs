using Ingeco.Intranet.Controllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingeco.Intranet.Models
{
    public class CreatePostViewModel : PostViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public string SerializedImages { get; set; }
        public string SerializedVideos { get; set; }
        public string SerializedTags { get; set; }

        public IEnumerable<MediaRecord> GetImageRecords() 
            => JsonConvert.DeserializeObject<IEnumerable<MediaRecord>>(SerializedImages.Replace("\\\"", "\""));

        public IEnumerable<MediaRecord> GetVideoRecords()
            => JsonConvert.DeserializeObject<IEnumerable<MediaRecord>>(SerializedVideos.Replace("\\\"", "\""));

        public string[] GetTags()
            => JsonConvert.DeserializeObject<string[]>(SerializedTags.Replace("\\\"", "\""));
    }
}