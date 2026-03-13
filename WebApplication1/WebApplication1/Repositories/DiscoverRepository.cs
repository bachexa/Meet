using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IDiscoverRepository
    {
        DiscoverSection? GetDiscoverSection(string language);
    }

    public class DiscoverRepository : IDiscoverRepository
    {
        private readonly string _connectionString;

        public DiscoverRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection connection string is not configured.");
        }

        public DiscoverSection? GetDiscoverSection(string language)
        {
            var section = new DiscoverSection();
            section.Cards = new List<DiscoverCard>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Get Section
                string sectionQuery = "SELECT TOP 1 * FROM DiscoverSections WHERE Language = @Language";
                int sectionId = 0;

                using (var command = new SqlCommand(sectionQuery, connection))
                {
                    command.Parameters.AddWithValue("@Language", language);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sectionId = (int)reader["Id"];
                            section.DiscoverHeader = reader["DiscoverHeader"].ToString();
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
                    SELECT dc.* 
                    FROM DiscoverCards dc
                    INNER JOIN DiscoverSections ds ON dc.DiscoverSectionId = ds.Id
                    WHERE ds.Language = @Language";

                using (var command = new SqlCommand(cardQuery, connection))
                {
                    command.Parameters.AddWithValue("@Language", language);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            section.Cards.Add(new DiscoverCard
                            {
                                Img = reader["Img"].ToString(),
                                DiscoverCardHeader = reader["DiscoverCardHeader"].ToString(),
                                DiscoverCardHeaderDescription = reader["DiscoverCardHeaderDescription"].ToString(),
                                DiscoverCardButton = reader["DiscoverCardButton"].ToString()
                            });
                        }
                    }
                }
            }

            return section;
        }
    }
}
