using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class RegisterTextRepository : IRegisterTextRepository
    {
        private readonly string _connectionString;

        public RegisterTextRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public RegisterTextModel GetRegisterText(string language)
        {
            var model = new RegisterTextModel();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT TOP 1 * FROM RegisterTexts WHERE Language = @Language";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Language", language);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            model.Title = reader["Title"].ToString();
                            model.FirstName = reader["FirstName"].ToString();
                            model.LastName = reader["LastName"].ToString();
                            model.Birthday = reader["Birthday"].ToString();
                            model.Gender = reader["Gender"].ToString();
                            model.Female = reader["Female"].ToString();
                            model.Male = reader["Male"].ToString();
                            model.Custom = reader["Custom"].ToString();
                            model.PhoneMail = reader["PhoneMail"].ToString();
                            model.Password = reader["Password"].ToString();
                            model.ConfirmPassword = reader["ConfirmPassword"].ToString();
                            model.Submit = reader["Submit"].ToString();
                            model.Back = reader["Back"].ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

            return model;
        }
    }
}
