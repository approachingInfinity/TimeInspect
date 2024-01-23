
namespace WebPageAge.Models
{
    public class WebpageInfo
    {
        public string? datePublished { get; set; }
        public string? dateModified { get; set; }
        public  string? url { get; set; }

        public int pubYears { get; set; }
        public int pubMonths { get; set; }
        public int pubDays { get; set; }

        public int modYears { get; set; }
        public int modMonths { get; set; }
        public int modDays { get; set; }
        public bool hasError { get; set; }
    }
}
