namespace WebApplication1.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string? HeaderText { get; set; }

        public string? ParagraphText { get; set; }

        public string? Img { get; set; }

        public byte[]? ImgData { get; set; }
        public string? ImgContentType { get; set; }

        public string? Language { get; set; }

        public string? SliderButton { get; set; }
    }
}
