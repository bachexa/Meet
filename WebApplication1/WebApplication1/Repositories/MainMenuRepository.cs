using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class MainMenuRepository : IMainMenuRepository
    {
        private readonly string _connectionString;

        public MainMenuRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public MainmenuItemsSection GetMainMenuSection(string language)
        {
            var section = new MainmenuItemsSection();
            section.MneuItemName = new List<MneuItems>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string sectionQuery = "SELECT TOP 1 * FROM MainMenuSections WHERE Language = @Language";
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
                            section.Download = reader["Download"].ToString();
                            section.SignIn = reader["SignIn"].ToString();
                            section.LangGeo = reader["LangGeo"].ToString();
                            section.LangENG = reader["LangENG"].ToString();
                            section.FooterFirstText = reader["FooterFirstText"].ToString();
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                string authQuery = "SELECT TOP 1 * FROM AuthCarts WHERE MainMenuSectionId = @SectionId";
                using (var command = new SqlCommand(authQuery, connection))
                {
                    command.Parameters.AddWithValue("@SectionId", sectionId);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            section.AuthCart = new AuthCart
                            {
                                UsernamePlaceholder = reader["UsernamePlaceholder"].ToString(),
                                PasswordPlaceholder = reader["PasswordPlaceholder"].ToString(),
                                ForgotPassword = reader["ForgotPassword"].ToString(),
                                SignIn = reader["SignIn"].ToString(),
                                GoogleSignIn = reader["GoogleSignIn"].ToString(),
                                MsSignIn = reader["MsSignIn"].ToString(),
                                RegisterPrompt = reader["RegisterPrompt"].ToString(),
                                RegisterLink = reader["RegisterLink"].ToString()
                            };
                        }
                    }
                }

                string menuQuery = "SELECT * FROM MenuItems WHERE MainMenuSectionId = @SectionId";
                using (var command = new SqlCommand(menuQuery, connection))
                {
                    command.Parameters.AddWithValue("@SectionId", sectionId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            section.MneuItemName.Add(new MneuItems
                            {
                                MenuItems = reader["MenuItemText"].ToString()
                            });
                        }
                    }
                }
            }

            return section;
        }
    }
}
