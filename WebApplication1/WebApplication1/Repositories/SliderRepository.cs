using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface ISliderRepository
    {
        List<Slider> GetSliders(string? language = null);
        Slider? GetSliderById(int id);
        bool UpdateSlider(Slider model);
    }

    public class SliderRepository : ISliderRepository
    {
        private readonly string _connectionString;

        public SliderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("DefaultConnection connection string is not configured.");
        }

        public List<Slider> GetSliders(string? language = null)
        {
            var sliders = new List<Slider>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = string.IsNullOrWhiteSpace(language)
                    ? "SELECT * FROM Sliders ORDER BY Id"
                    : "SELECT * FROM Sliders WHERE Language = @Language ORDER BY Id";

                using (var command = new SqlCommand(query, connection))
                {
                    if (!string.IsNullOrWhiteSpace(language))
                        command.Parameters.AddWithValue("@Language", language);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sliders.Add(new Slider
                            {
                                Id = Convert.ToInt32(reader["Id"]),
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

        public Slider? GetSliderById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Sliders WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Slider
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                HeaderText = reader["HeaderText"].ToString(),
                                ParagraphText = reader["ParagraphText"].ToString(),
                                Img = reader["Img"].ToString(),
                                Language = reader["Language"].ToString(),
                                SliderButton = reader["SliderButton"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        public bool UpdateSlider(Slider model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"
            UPDATE Sliders
            SET 
                HeaderText = @HeaderText,
                ParagraphText = @ParagraphText,
                Img = @Img,
                Language = @Language,
                SliderButton = @SliderButton
            WHERE Id = @Id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", model.Id);
                    command.Parameters.AddWithValue("@HeaderText", (object?)model.HeaderText ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ParagraphText", (object?)model.ParagraphText ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Img", (object?)model.Img ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Language", (object?)model.Language ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SliderButton", (object?)model.SliderButton ?? DBNull.Value);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }



}
