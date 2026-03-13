using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface ISolutionRepository
    {
        SolutionsSection? GetSolutionSection(string language);
    }

    public class SolutionRepository : ISolutionRepository
    {
        private readonly string _connectionString;

        public SolutionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection connection string is not configured.");
        }

        public SolutionsSection? GetSolutionSection(string language)
        {
            var section = new SolutionsSection();
            section.Cards = new List<SolutionCard>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Get Section
                string sectionQuery = "SELECT TOP 1 * FROM SolutionSections WHERE Language = @Language";
                using (var command = new SqlCommand(sectionQuery, connection))
                {
                    command.Parameters.AddWithValue("@Language", language);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            section.SolutionName = reader["SolutionName"].ToString();
                            section.SolutionDescription = reader["SolutionDescription"].ToString();
                            section.Language = reader["Language"].ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                // Get Cards with JOIN
                string cardQuery = @"
                    SELECT sc.* 
                    FROM SolutionCards sc
                    INNER JOIN SolutionSections ss ON sc.SolutionSectionId = ss.Id
                    WHERE ss.Language = @Language";

                using (var command = new SqlCommand(cardQuery, connection))
                {
                    command.Parameters.AddWithValue("@Language", language);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            section.Cards.Add(new SolutionCard
                            {
                                SolutionCardName = reader["SolutionCardName"].ToString(),
                                SolutionCardDescription = reader["SolutionCardDescription"].ToString(),
                                SolutionCardButton = reader["SolutionCardButton"].ToString(),
                                SolutionCardSliderImg = reader["SolutionCardSliderImg"].ToString(),
                                Language = reader["Language"].ToString()
                            });
                        }
                    }
                }
            }

            return section;
        }
    }
}
