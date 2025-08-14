using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscoverSectionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var dsList = BuildDiscoverSectionList();
            var filtred = dsList.Where(s=>s.Language == lang);
            return Ok(filtred);// JSON
        }

        private List<DiscoverSection> BuildDiscoverSectionList()
        {
            var dsList = new List<DiscoverSection>
                {
                    // English
                    new DiscoverSection
                    {
                        DiscoverHeader = "Discover what’s happening with <span>MeetDesk</span>",
                        Language = "en",
                        Cards = new List<DiscoverCard>
                        {
                            new DiscoverCard
                            {
                                Img = "/images/discover-1.png",
                                DiscoverCardHeader = "From Threads to Workflows: New Features that Boost Productivity",
                                DiscoverCardHeaderDescription = "Work faster with threads, multi-emoji reactions and emoji-triggered workflows built into MeetDesk.",
                                DiscoverCardButton = "Learn more",
                                //Language = "en"
                            },
                            new DiscoverCard
                            {
                                Img = "/images/discover-2.png",
                                DiscoverCardHeader = "2025: The Year the Frontier Firm Is Born",
                                DiscoverCardHeaderDescription = "Read the latest research and insights from the Work Trend Index Annual Report.",
                                DiscoverCardButton = "Read the report",
                                //Language = "en"
                            },
                            new DiscoverCard
                            {
                                Img = "/images/discover-3.png",
                                DiscoverCardHeader = "Prompt like a pro with MeetDesk Copilot",
                                DiscoverCardHeaderDescription = "Transform meetings with prompts: capture ideas, summarize decisions and move work forward.",
                                DiscoverCardButton = "Learn more"
                                //Language = "en"
                            }
                        }
                    },

                    // Georgian
                    new DiscoverSection
                    {
                        DiscoverHeader = "აღმოაჩინე რა ხდება <span>MeetDesk</span>-თან ერთად",
                        Language = "ka",
                        Cards = new List<DiscoverCard>
                        {
                            new DiscoverCard
                            {
                                Img = "/images/discover-1.png",
                                DiscoverCardHeader = "თემებიდან სამუშაო პროცესებამდე: ახალი ფუნქციები პროდუქტიულობის გასაზრდელად",
                                DiscoverCardHeaderDescription = "იმუშავე სწრაფად თემებით, მრავალ-ემოჯი რეაქციებით და ემოჯი-ტრიგერ სამუშაო პროცესებით MeetDesk-ში.",
                                DiscoverCardButton = "გაიგე მეტი",
                                //Language = "ka"
                            },
                            new DiscoverCard
                            {
                                Img = "/images/discover-2.png",
                                DiscoverCardHeader = "2025: წელი, როდესაც შეიქმნა ფრონტიერ-კომპანია",
                                DiscoverCardHeaderDescription = "წაიკითხე უახლესი კვლევები და შეხედულებები სამუშაო ტენდენციების წლიური ანგარიშიდან.",
                                DiscoverCardButton = "წაიკითხე ანგარიში",
                                //Language = "ka"
                            },
                            new DiscoverCard
                            {
                                Img = "/images/discover-3.png",
                                DiscoverCardHeader = "MeetDesk Copilot-თან ერთად იმუშავე როგორც პროფესიონალი",
                                DiscoverCardHeaderDescription = "გადაწერე შეხვედრები პრომპტებით: შეაგროვე იდეები, შეაჯამე გადაწყვეტილებები და წინ წაიწიე სამუშაოში.",
                                DiscoverCardButton = "გაიგე მეტი",
                                //Language = "ka"
                            }
                        }
                    }
                };
            return dsList;
        }
    }
}
