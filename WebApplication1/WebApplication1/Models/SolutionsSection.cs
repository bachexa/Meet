namespace WebApplication1.Models
{
    public class SolutionsSection
    {
        public string? SolutionName { get; set; }
        public string? SolutionDescription { get; set; }

        public string? Language { get; set; }

        public List<SolutionCard>? Cards { get; set; }
    }
}
