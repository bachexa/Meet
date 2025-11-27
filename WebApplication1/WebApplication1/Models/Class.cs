namespace WebApplication1.Models
{
    public class PlansSectionModel
    {
        public string? CardName { get; set; }
        public string? CardDescription { get; set; }
        public string? Language { get; set; }
        public List<PlanCardModel>? Cards { get; set; }
    }

    public class PlanCardModel
    {
        public string? CardName { get; set; }
        public string? CardDescription { get; set; }
        public string? CardButton { get; set; }
        public string? CardSvg { get; set; }
    }
}