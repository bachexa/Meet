using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansSectionController : ControllerBase
    {
        private readonly IPlansRepository _repository;

        public PlansSectionController(IPlansRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var plans = _repository.GetPlansSection(lang);

            if (plans == null)
                return NotFound();
            return Ok(plans); 
        }
    }
}
