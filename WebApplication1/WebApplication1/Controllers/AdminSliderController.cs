using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Admin;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/admin/slider")]
    public class AdminSliderController : ControllerBase
    {
        private readonly ISliderRepository _sliderRepository;

        public AdminSliderController(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        [HttpGet]
        public IActionResult Get(string language = "en")
        {
            var sliders = _sliderRepository.GetSliders(language);

            var result = sliders.Select(x => new SliderAdminDto
            {
                HeaderText = x.HeaderText,
                ParagraphText = x.ParagraphText,
                Img = x.Img,
                Language = x.Language,
                SliderButton = x.SliderButton
            }).ToList();

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UpdateSliderAdminDto model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Language))
                return BadRequest("Language is required.");

            var slider = new Slider
            {
                HeaderText = model.HeaderText,
                ParagraphText = model.ParagraphText,
                Img = model.Img,
                Language = model.Language,
                SliderButton = model.SliderButton
            };

            var updated = _sliderRepository.UpdateSlider(slider);

            if (!updated)
                return NotFound("Slider not found.");

            return Ok(new { message = "Slider updated successfully." });
        }
    }
}