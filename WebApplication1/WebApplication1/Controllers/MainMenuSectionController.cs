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
                        }
                    }
                },

                new MainmenuItemsSection
                {
                    Language = "ka",
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
