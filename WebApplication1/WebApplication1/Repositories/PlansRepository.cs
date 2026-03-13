using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IPlansRepository
    {
        PlansSection? GetPlansSection(string language);
    }

    public class PlansRepository : IPlansRepository
    {
        private readonly string _connectionString;

        public PlansRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection connection string is not configured.");
        }

        public PlansSection? GetPlansSection(string language)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            const string sectionQuery = @"
                SELECT TOP 1 Id, Language, PlansTitle, PlansDescription
                FROM PlansSections
                WHERE Language = @Language;";

            int sectionId;
            var section = new PlansSection
            {
                AllPlans = new List<PlansAll>()
            };

            using (var command = new SqlCommand(sectionQuery, connection))
            {
                command.Parameters.AddWithValue("@Language", language);
                using var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    return null;
                }

                sectionId = reader.GetInt32(reader.GetOrdinal("Id"));
                section.Language = reader["Language"]?.ToString();
                section.PlansTitle = reader["PlansTitle"]?.ToString();
                section.PlansDescription = reader["PlansDescription"]?.ToString();
            }

            const string plansWithCardsQuery = @"
                SELECT
                    pa.Id AS PlanId,
                    pa.Img,
                    pa.MenuItem,
                    pc.Icon,
                    pc.Title,
                    pc.Description,
                    pc.More
                FROM PlansAll pa
                LEFT JOIN PlansCards pc ON pa.Id = pc.PlansAllId
                WHERE pa.PlansSectionId = @SectionId
                ORDER BY pa.Id;";

            var plansMap = new Dictionary<int, PlansAll>();

            using (var command = new SqlCommand(plansWithCardsQuery, connection))
            {
                command.Parameters.AddWithValue("@SectionId", sectionId);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var planId = reader.GetInt32(reader.GetOrdinal("PlanId"));

                    if (!plansMap.TryGetValue(planId, out var planAll))
                    {
                        planAll = new PlansAll
                        {
                            Img = reader["Img"]?.ToString(),
                            MenuItem = reader["MenuItem"]?.ToString(),
                            Plans = new List<PlansCard>()
                        };

                        plansMap[planId] = planAll;
                        (section.AllPlans ??= new List<PlansAll>()).Add(planAll);
                    }

                    var iconValue = reader["Icon"];
                    if (iconValue is DBNull)
                    {
                        continue;
                    }

                    (planAll.Plans ??= new List<PlansCard>()).Add(new PlansCard
                    {
                        Icon = iconValue.ToString(),
                        Title = reader["Title"]?.ToString(),
                        Description = reader["Description"]?.ToString(),
                        More = reader["More"]?.ToString()
                    });
                }
            }

            return section;
        }
    }
}