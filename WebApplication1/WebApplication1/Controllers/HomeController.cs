using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var slider = new List<Slider>();
            slider.Add(new Slider
            {
                HeaderText = "Organize your Daylly meetings via<br /><span style='font-weight:700;font-size:42px;color:#1a1a1a;'><span style='color:#0078d4;'>Meet</span>Desk</span>",
                ParagraphText = "Start meetings, share moments and work together easily!",
                Img = "/images/teams-hero.png",
                Language = "en"
            });

            slider.Add(new Slider
            {
                HeaderText = "Find a friend if they are free. Via:<br />" +
                 "<span style='font-weight:700;font-size:42px;color:#1a1a1a;'>" +
                 "<span style='color:#0078d4;'>Meet</span>Desk</span>",
                ParagraphText = "Start meetings, share moments and work together easily!",
                Img = "/images/teams-hero2.png",
                Language = "en"
            });

            slider.Add(new Slider
            {
                HeaderText = "Organize your Daylly meetings via<br />" +
                 "<span style='font-weight:700;font-size:42px;color:#1a1a1a;'>" +
                 "<span style='color:#0078d4;'>Meet</span>Desk</span>",
                ParagraphText = "Start meetings, share moments and work together easily!",
                Img = "/images/teams-hero3.png",
                Language = "en"
            });

            slider.Add(new Slider
            {
                HeaderText = "Organize your Daylly meetings via<br />" +
                 "<span style='font-weight:700;font-size:42px;color:#1a1a1a;'>" +
                 "<span style='color:#0078d4;'>Meet</span>Desk</span>",
                ParagraphText = "Start meetings, share moments and work together easily!",
                Img = "/images/teams-hero4.png",
                Language = "en"
            });

            return View(slider);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
