namespace WebApplication1.Models
{
    public class DiscoverSection
    {
        public string? DiscoverHeader { get; set; }  // only once
        public string? Language { get; set; }
        public List<DiscoverCard> Cards { get; set; } = new();
    }
}
