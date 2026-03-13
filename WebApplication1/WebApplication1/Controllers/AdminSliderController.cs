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
        public IActionResult Get(string? language = null)
        {
            var sliders = _sliderRepository.GetSliders(language);

            var result = sliders.Select(x => new SliderAdminDto
            {
                Id = x.Id,
                HeaderText = x.HeaderText,
                ParagraphText = x.ParagraphText,
                Img = x.Img,
                Language = x.Language,
                SliderButton = x.SliderButton
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var slider = _sliderRepository.GetSliderById(id);
            if (slider == null)
                return NotFound();

            return Ok(new SliderAdminDto
            {
                Id = slider.Id,
                HeaderText = slider.HeaderText,
                ParagraphText = slider.ParagraphText,
                Img = slider.Img,
                Language = slider.Language,
                SliderButton = slider.SliderButton
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSliderAdminDto model)
        {
            if (model == null || id != model.Id)
                return BadRequest();

            var slider = new Slider
            {
                Id = model.Id,
                HeaderText = model.HeaderText,
                ParagraphText = model.ParagraphText,
                Img = model.Img,
                Language = model.Language,
                SliderButton = model.SliderButton
            };

            var updated = _sliderRepository.UpdateSlider(slider);

            if (!updated)
                return NotFound();

            return Ok(new { message = "Slider updated successfully." });
        }
    }
}