using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface ISliderRepository
    {
        List<Slider> GetSliders(string language);
        bool UpdateSlider(Slider model);
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
                        SliderButton = @SliderButton
                    WHERE Language = @Language";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HeaderText", (object?)model.HeaderText ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ParagraphText", (object?)model.ParagraphText ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Img", (object?)model.Img ?? DBNull.Value);
                    command.Parameters.AddWithValue("@SliderButton", (object?)model.SliderButton ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Language", (object?)model.Language ?? DBNull.Value);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }



}
