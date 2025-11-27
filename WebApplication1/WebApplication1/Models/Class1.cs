
namespace WebApplication1.Models
{
    public class ProductSectionModel
    {
        public string? ProductSectionTitle { get; set; }
        public string? ProductSectionDescription { get; set; }
        public string? Language { get; set; }
        public List<ProductCardModel>? ProductCards { get; set; }
    }

    public class ProductCardModel
    {
        public string? ProductCardTitle { get; set; }
        public string? ProductCardDescription { get; set; }
        public string? ProductCardButton { get; set; }
        public string? ProductCardSvg { get; set; }
        public string? ProductCardPanel { get; set; } // home, business, enterprise, education
    }
}