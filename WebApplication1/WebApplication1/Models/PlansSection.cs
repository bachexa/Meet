namespace WebApplication1.Models
{
    public class PlansSection
    {
         public string? Language { get; set; }
         public string? PlansTitle { get; set; }
         public string? PlansDescription { get; set; }

        public List<PlansAll>? AllPlans { get; set; }
    }
}
