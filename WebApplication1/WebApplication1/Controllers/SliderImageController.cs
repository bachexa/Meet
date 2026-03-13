using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class SliderImageController : Controller
    {
        private readonly ISliderRepository _sliderRepository;

        public SliderImageController(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        [HttpGet("/slider/image/{id:int}")]
        public IActionResult GetImage(int id)
        {
            var slider = _sliderRepository.GetSliderById(id);
            if (slider == null)
                return NotFound();

            if (slider.ImgData != null && slider.ImgData.Length > 0)
            {
                var contentType = string.IsNullOrWhiteSpace(slider.ImgContentType)
                    ? "application/octet-stream"
                    : slider.ImgContentType;

                return File(slider.ImgData, contentType);
            }

            if (!string.IsNullOrWhiteSpace(slider.Img))
            {
                return Redirect(slider.Img);
            }

            return NotFound();
        }
    }
}