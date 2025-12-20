
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainMenuSectionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var dsList = BuildMainMenuList();
            var menuItems = dsList.FirstOrDefault(s => s.Language == lang);

            if (menuItems == null)
                return NotFound();
            return Ok(menuItems);
        }

        private List<MainmenuItemsSection> BuildMainMenuList()
        {
            var mainMenuItem = new List<MainmenuItemsSection>
            { 
                new MainmenuItemsSection
                {
                    Language = "en",
                    Download = "Download MetDesk",
                    SignIn = "Sign in",
                    LangENG = "ENG",
                    LangGeo = "GEO",
                    FooterFirstText = "&copy; 2025 - MeetDesk. All rights reserved.",
                    AuthCart = new AuthCart
                    {
                        UsernamePlaceholder = "Username or Email",
                        PasswordPlaceholder = "Password",
                        ForgotPassword = "Forgot password?",
                        SignIn = "Sign in",
                        GoogleSignIn = "Sign in with Google",
                        MsSignIn = "Sign in with Microsoft",
                        RegisterPrompt = "Don't have an account?",
                        RegisterLink = "Register"
                    },

                    MneuItemName = new List<MneuItems>
                    {
                        new MneuItems
                        {
                            MenuItems = "Products"
                        },
                        new MneuItems
                        {
                            MenuItems = "Features"
                        },
                        new MneuItems
                        {
                            MenuItems = "Solutions"
                        },
                        new MneuItems
                        {
                            MenuItems = "Resources"
                        },
                        new MneuItems
                        {
                            MenuItems = "Support"
                        },
                    }
                },

                new MainmenuItemsSection
                {
                    Language = "ka",
                    Download = "გადმოწერე MeetDesk",
                    SignIn = "შესვლა",
                    LangENG = "ინგ",
                    LangGeo = "ქარ",
                    FooterFirstText = "&copy; 2025 - MeetDesk. ყველა უფლება დაცულია.",
                    AuthCart = new AuthCart
                    {
                        UsernamePlaceholder = "მომხმარებელი ან ელფოსტა",
                        PasswordPlaceholder = "პაროლი",
                        ForgotPassword = "დაგავიწყდა პაროლი?",
                        SignIn = "შესვლა",
                        GoogleSignIn = "შესვლა Google - ით",
                        MsSignIn = "შესვლა Microsoft - ით",
                        RegisterPrompt = "არ გაქვს ანგარიში?",
                        RegisterLink = "რეგისტრაცია"
                    },

                    MneuItemName = new List<MneuItems>
                    {
                        new MneuItems
                        {
                            MenuItems = "პროდუქტები"
                        },
                        new MneuItems
                        {
                            MenuItems = "ფუნქციები"
                        },
                        new MneuItems
                        {
                            MenuItems = "გადაწყვეტები"
                        },
                        new MneuItems
                        {
                            MenuItems = "რესურსები"
                        },
                        new MneuItems
                        {
                            MenuItems = "მხარდაჭერა"
                        }
                    }
                }
            };
            return mainMenuItem;
        }
    }
}
