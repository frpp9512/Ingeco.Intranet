namespace Ingeco.Intranet.Models
{
    public record MediaRecord
    {
        public int id { get; set; }
        public string filename { get; set; }
        public string tmpId { get; set; }
        public string description { get; set; }
        public bool isCover { get; set; }
    }
}