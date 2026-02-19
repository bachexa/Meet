
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterTextController : ControllerBase
    {
        private readonly IRegisterTextRepository _repository;

        public RegisterTextController(IRegisterTextRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var textModel = _repository.GetRegisterText(lang);
            
            // If lang not found or model empty (check a required field)
            if (textModel == null || string.IsNullOrEmpty(textModel.Title))
            {
                 // Fallback to 'en' if not found, similar to original logic
                 textModel = _repository.GetRegisterText("en");
            }
            
            if (textModel == null) return NotFound();

            return Ok(textModel);
        }
    }
}
