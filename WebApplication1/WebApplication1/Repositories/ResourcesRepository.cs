using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class ResourcesRepository : IResourcesRepository
    {
        private readonly string _connectionString;

        public ResourcesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public ResourcesSectionModel GetResourcesSection(string language)
        {
            var section = new ResourcesSectionModel();
            section.Cards = new List<ResourcesCardModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sectionQuery = "SELECT TOP 1 * FROM ResourcesSections WHERE Language = @Language";
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
                            section.CardName = reader["CardName"].ToString();
                            section.CardDescription = reader["CardDescription"].ToString();
                            section.BackgroundImg = reader["BackgroundImg"].ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                string cardQuery = "SELECT * FROM ResourcesCards WHERE ResourcesSectionId = @SectionId";
                using (var command = new SqlCommand(cardQuery, connection))
                {
                    command.Parameters.AddWithValue("@SectionId", sectionId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            section.Cards.Add(new ResourcesCardModel
                            {
                                CardName = reader["CardName"].ToString(),
                                CardDescription = reader["CardDescription"].ToString(),
                                CardButton = reader["CardButton"].ToString(),
                                CardSvg = reader["CardSvg"].ToString()
                            });
                        }
                    }
                }
            }

            return section;
        }
    }
}
