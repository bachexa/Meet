using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionsSectionController : ControllerBase
    {
        private readonly ISolutionRepository _repository;

        public SolutionsSectionController(ISolutionRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var section = _repository.GetSolutionSection(lang);

            if (section == null)
                return NotFound();

            return Ok(section);
        }
    }
}
