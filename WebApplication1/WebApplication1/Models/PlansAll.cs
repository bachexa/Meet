namespace WebApplication1.Models
{
    public class PlansAll
    {
        public string? Language { get; set; }
        public string? PlansTitle { get; set; }
        public string? PlansDescription { get; set; }
        public string? Img { get; set; }
        public string? MenuItem { get; set; }
        public List<PlansCard>? Plans { get; set; }
    }
}
