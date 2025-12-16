namespace WebApplication1.Models
{
    public class ProductSectionModel
    {
        public string? ProductSectionTitle { get; set; }
        public string? ProductSectionDescription { get; set; }
        public string? Language { get; set; }
        public string? HeroImage { get; set; }   
        public Dictionary<string, string>? PanelHeroImages { get; set; } = new(); 
        public Dictionary<string, string>? TabLabels { get; set; } = new(); 
        public List<ProductCardModel>? ProductCards { get; set; } = new();
    }
}