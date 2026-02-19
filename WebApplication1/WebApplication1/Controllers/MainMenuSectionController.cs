
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainMenuSectionController : ControllerBase
    {
        private readonly IMainMenuRepository _repository;

        public MainMenuSectionController(IMainMenuRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var menuItems = _repository.GetMainMenuSection(lang);

            if (menuItems == null || menuItems.Language == null) // Check if valid data returned
                return NotFound();
            return Ok(menuItems);
        }
    }
}
