using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface ISliderRepository
    {
        List<Slider> GetSliders(string language);
    }

    public class SliderRepository : ISliderRepository
    {
        private readonly string _connectionString;

        public SliderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Slider> GetSliders(string language)
        {
            var sliders = new List<Slider>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Sliders WHERE Language = @Language";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Language", language);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sliders.Add(new Slider
                            {
                                HeaderText = reader["HeaderText"].ToString(),
                                ParagraphText = reader["ParagraphText"].ToString(),
                                Img = reader["Img"].ToString(),
                                Language = reader["Language"].ToString(),
                                SliderButton = reader["SliderButton"].ToString()
                            });
                        }
                    }
                }
            }

            return sliders;
        }
    }
}
