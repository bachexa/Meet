using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IPlansRepository
    {
        PlansSection GetPlansSection(string language);
    }

    public class PlansRepository : IPlansRepository
    {
        private readonly string _connectionString;

        public PlansRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public PlansSection GetPlansSection(string language)
        {
            var section = new PlansSection();
            section.AllPlans = new List<PlansAll>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Get Section
                string sectionQuery = "SELECT TOP 1 * FROM PlansSections WHERE Language = @Language";
                int sectionId = 0;

                using (var command = new SqlCommand(sectionQuery, connection))
                {
                    command.Parameters.AddWithValue("@Language", language);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sectionId = (int)reader["Id"];
                            section.Language = reader["Language"].ToString();
                            section.PlansTitle = reader["PlansTitle"].ToString();
                            section.PlansDescription = reader["PlansDescription"].ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                // Get PlansAll (Categories)
                string plansAllQuery = "SELECT * FROM PlansAll WHERE PlansSectionId = @SectionId";
                
                // We'll fetch into a list first because we need nested queries
                var allPlansList = new List<PlansAll>();
                var allPlansIds = new List<int>();

                using (var command = new SqlCommand(plansAllQuery, connection))
                {
                    command.Parameters.AddWithValue("@SectionId", sectionId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var planAll = new PlansAll
                            {
                                Img = reader["Img"].ToString(),
                                MenuItem = reader["MenuItem"].ToString(),
                                Plans = new List<PlansCard>()
                            };
                            allPlansList.Add(planAll);
                            allPlansIds.Add((int)reader["Id"]);
                        }
                    }
                }
                
                // Now iterate and fill children using the IDs
                for (int i = 0; i < allPlansList.Count; i++)
                {
                    var planAll = allPlansList[i];
                    int id = allPlansIds[i];
                    section.AllPlans.Add(planAll); // Add to section

                    string cardsQuery = "SELECT * FROM PlansCards WHERE PlansAllId = @PlansAllId";
                    using (var command = new SqlCommand(cardsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PlansAllId", id);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                planAll.Plans.Add(new PlansCard
                                {
                                    Icon = reader["Icon"].ToString(),
                                    Title = reader["Title"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    More = reader["More"].ToString()
                                });
                            }
                        }
                    }
                }
            }

            return section;
        }
    }
}
