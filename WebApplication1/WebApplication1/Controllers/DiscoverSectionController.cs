using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscoverSectionController : ControllerBase
    {
        private readonly IDiscoverRepository _repository;

        public DiscoverSectionController(IDiscoverRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var section = _repository.GetDiscoverSection(lang);

            if (section == null)
                return NotFound();

            return Ok(section);
        }
    }
}
