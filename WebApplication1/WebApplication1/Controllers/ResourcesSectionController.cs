using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesSectionController : Controller
    {
        private readonly IResourcesRepository _repository;

        public ResourcesSectionController(IResourcesRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var section = _repository.GetResourcesSection(lang);
            if (section == null || section.Language == null) return NotFound();
            return Ok(section);
        }
    }
}


