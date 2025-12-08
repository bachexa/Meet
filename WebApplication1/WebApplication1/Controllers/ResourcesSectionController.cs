using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesSectionController : Controller
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var plansSections = BuildPlansSections();
            var section = plansSections.FirstOrDefault(s => s.Language == lang);
            if (section == null) return NotFound();
            return Ok(section);
        }

        private List<ResourcesSectionModel> BuildPlansSections()
        {
            return new List<ResourcesSectionModel>
            {
                new ResourcesSectionModel
                {
                    CardName = "Get Started",
                    CardDescription = "Take the next step with MeetDesk",
                    Language = "en",
                    BackgroundImg = "../images/13d29be3-9843-4631-b987-9ea323793171.jpg",
                    Cards = new List<ResourcesCardModel>
                    {
                        new ResourcesCardModel
                        {
                            CardName = "For business",
                            CardDescription = "Grow your customer base with communications software designed for up to 300 employees.",
                            CardButton = "Contact sales",
                            CardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                            <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                            <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                        </svg>"
                        },
                        new ResourcesCardModel
                        {
                            CardName = "For personal use",
                            CardDescription = "Plan events, share photos, and connect with your friends, family, and community.",
                            CardButton = "Get started free",
                            CardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                            <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                            <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                        </svg>"
                        },
                        new ResourcesCardModel
                        {
                            CardName = "For enterprise",
                            CardDescription = "Achieve more with Teams accounts for more than 300 people.",
                            CardButton = "Get started",
                            CardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                            <circle cx='12' cy='12' r='10' opacity='0.12'></circle>
                                            <path d='M7 13h10v2H7zM7 9h10v2H7z'></path>
                                        </svg>"
                        },
                        new ResourcesCardModel
                        {
                            CardName = "For education",
                            CardDescription = "Make learning collaborative—for both students and educators.",
                            CardButton = "Learn more",
                            CardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                            <path d='M2 7l10-5 10 5-10 5z'></path>
                                            <path d='M4 12v5l8 4 8-4v-5' opacity='0.12'></path>
                                        </svg>"
                        }
                    }
                },
                new ResourcesSectionModel
                {
                    CardName = "დაიწყე",
                    CardDescription = "გადადგით შემდეგი ნაბიჯი MeetDesk-თან ერთად",
                    Language = "ka",
                    Cards = new List<ResourcesCardModel>
                    {
                        new ResourcesCardModel
                        {
                            CardName = "ბიზნესისთვის",
                            CardDescription = "გაზარდეთ თქვენი მომხმარებელთა ბაზა 300-მდე თანამშრომლისთვის შექმნილი საკომუნიკაციო პროგრამული უზრუნველყოფით",
                            CardButton = "გაყიდვებთან დაკავშირება",
                            CardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                            <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                            <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                        </svg>"
                        },
                        new ResourcesCardModel
                        {
                            CardName = "პირადი გამოყენებისთვის",
                            CardDescription = "დაგეგმეთ ღონისძიებები, გააზიარეთ ფოტოები და დაუკავშირდით თქვენს მეგობრებს, ოჯახს და საზოგადოებას",
                            CardButton = "დაიწყეთ უფასოდ",
                            CardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                            <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                            <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                        </svg>"
                        },
                        new ResourcesCardModel
                        {
                            CardName = "საწარმოსთვის",
                            CardDescription = "მიაღწიეთ მეტს Teams ანგარიშებით 300-ზე მეტი ადამიანის ანგარიშებით",
                            CardButton = "დაიწყეთ",
                            CardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                            <circle cx='12' cy='12' r='10' opacity='0.12'></circle>
                                            <path d='M7 13h10v2H7zM7 9h10v2H7z'></path>
                                        </svg>"
                        },
                        new ResourcesCardModel
                        {
                            CardName = "განათლებისთვის",
                            CardDescription = "სწავლის თანამშრომლობითი გახადეთ — როგორც სტუდენტებისთვის, ასევე პედაგოგებისთვის.",
                            CardButton = "შეიტყვეთ მეტი",
                            CardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                            <path d='M2 7l10-5 10 5-10 5z'></path>
                                            <path d='M4 12v5l8 4 8-4v-5' opacity='0.12'></path>
                                        </svg>"
                        }
                    }
                }
            };
        }
    }


}
