using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SliderController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var slider = BuildSliderList();                 // ⬅️ დროებით აქვე copy-paste
            var filtered = slider.Where(s => s.Language == lang).ToList();
            return Ok(filtered);                            // JSON
        }

        // ⬇️ დროებით ესე დავტოვოთ. მერე გადავიტანთ სერვისში, თუ გაინდება.
        private List<Slider> BuildSliderList()
        {
            var slider = new List<Slider> {
            new Slider
            {
                HeaderText = "Organize your  meetings via<br /><span style='font-weight:700;font-size:42px;color:#224541;'><span style='color:#1f5c4e;'>Meet</span>Desk</span>",
                ParagraphText = "Start meetings, share moments and work together easily! Start meetings, share moments and work together easily! Start meetings, share moments and work together easily!",
                Img = "/images/videocalls.png",
                Language = "en",
                SliderButton = "Download now"
            },

            new Slider
            {
                HeaderText = "Find a friend if they are free. Via:<br />" +
                              "<span style='font-weight:700;font-size:42px;color:#224541;'>" +
                              "<span style='color:#1f5c4e;'>Meet</span>Desk</span>",
                ParagraphText = "Start meetings, share moments and work together easily!",
                Img = "/images/Fly.png",
                Language = "en",
                SliderButton = "Download now"
            },

            new Slider
            {
                HeaderText = "Organize your Daylly meetings via<br />" +
                              "<span style='font-weight:700;font-size:42px;color:#224541;'>" +
                              "<span style='color:#1f5c4e;'>Meet</span>Desk</span>",
                ParagraphText = "Start meetings, share moments and work together easily!",
                Img = "/images/Fly2.png",
                Language = "en",
                SliderButton = "Download now"
            },

            new Slider
            {
                HeaderText = "Organize your Daylly meetings via<br />" +
                              "<span style='font-weight:700;font-size:42px;color:#224541;'>" +
                              "<span style='color:#1f5c4e;'>Meet</span>Desk</span>",
                ParagraphText = "Start meetings, share moments and work together easily!",
                Img = "/images/videocall2.png",
                Language = "en",
                SliderButton = "Download now"
            },

            new Slider
            {
                HeaderText = "დაგეგმე შენი  შეხვედრები<br /><span style='font-weight:700;font-size:42px;color:#224541;'><span style='color:#1f5c4e;'>Meet</span>Desk</span>",
                ParagraphText = "დაიწყე შეხვედრები, გააზიარე მომენტები და იმუშავე მარტივად ერთად!",
                Img = "/images/videocalls.png",
                Language = "ka",
                SliderButton = "გადმოწერეთ"
            },

            new Slider
            {
                HeaderText = "იპოვე მეგობარი, თუ თავისუფალია. საშუალებით:<br />" +
                              "<span style='font-weight:700;font-size:42px;color:#224541;'>" +
                              "<span style='color:#1f5c4e;'>Meet</span>Desk</span>",
                ParagraphText = "დაიწყე შეხვედრები, გააზიარე მომენტები და იმუშავე მარტივად ერთად!",
                Img = "/images/FlyKa.png",
                Language = "ka",
                SliderButton = "გადმოწერეთ"
            },

            new Slider
            {
                HeaderText = "დაასაწყობე შენი ყოველდღიური შეხვედრები<br />" +
                              "<span style='font-weight:700;font-size:42px;color:#224541;'>" +
                              "<span style='color:#1f5c4e;'>Meet</span>Desk</span>",
                ParagraphText = "დაიწყე შეხვედრები, გააზიარე მომენტები და იმუშავე მარტივად ერთად!",
                Img = "/images/FlyKa2.png",
                Language = "ka",
                SliderButton = "გადმოწერეთ"
            },

            new Slider
            {
                HeaderText = "დაასაწყობე შენი ყოველდღიური შეხვედრები<br />" +
                              "<span style='font-weight:700;font-size:42px;color:#224541;'>" +
                              "<span style='color:#1f5c4e;'>Meet</span>Desk</span>",
                ParagraphText = "დაიწყე შეხვედრები, გააზიარე მომენტები და იმუშავე მარტივად ერთად!",
                Img = "/images/videocall2.png",
                Language = "ka",
                SliderButton = "გადმოწერეთ"
            }};
            return slider;
        }
    }
}
