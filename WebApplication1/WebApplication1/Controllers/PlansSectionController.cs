using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansSectionController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var dsList = BuildPlansList();
            var plans = dsList.FirstOrDefault(s => s.Language == lang);

            if (plans == null)
                return NotFound();

            return Ok(plans); // single object
        }


        private List<PlansSection> BuildPlansList()
        {
            var plansSections = new List<PlansSection>
            {
                new PlansSection
                {
                    Language = "en",
                    PlansTitle = "Products and services",
                    PlansDescription = "Find the right MeetDesk plan and add-ons for your needs",
                    AllPlans = new List<PlansAll>()
                    {
                       new PlansAll
                       {
                           Img = "/images/home.png",
                           MenuItem = "Home",
                           Plans = new List<PlansCard>()
                           {
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M4 4h16v16H4z\" opacity=\"0.12\"></path>\r\n <path d=\"M7 7h6v10H7zM15 7h2v10h-2z\"></path>\r\n </svg>",
                                   Title = "Meet Desk (free)",
                                   Description = "Send messages, schedule calls for up to 60 minutes, and create communities for every interest.",
                                   More = "Learn more"
                               },
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n<circle cx=\"12\" cy=\"12\" r=\"10\" opacity=\"0.12\"></circle>\r\n <path d=\"M7 13h10v2H7zM7 9h10v2H7z\"></path>\r\n</svg>",
                                   Title = "Meet Desk (free)",
                                   Description = "Send messages, schedule calls for up to 60 minutes, and create communities for every interest.",
                                   More = "Learn more"
                               },
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <rect x=\"3\" y=\"4\" width=\"18\" height=\"14\" rx=\"3\" opacity=\"0.12\"></rect>\r\n <path d=\"M7 9h10v2H7zM7 12h6v2H7z\"></path>\r\n </svg>",
                                   Title = "Setup Desk (free)",
                                   Description = "Send messages, schedule calls for up to 60 minutes, and create communities for every interest.",
                                   More = "Learn more"
                               },
                               new PlansCard
                               {
                                   Icon = " <svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M4 4h16v16H4z\" opacity=\"0.12\"></path>\r\n <path d=\"M7 7h6v10H7zM15 7h2v10h-2z\"></path>\r\n </svg>",
                                   Title = "Meet Desk Desk Family",
                                   Description = "Get MeetDesk accounts for up to six people, plus Setup Desk apps and advanced security.",
                                   More = "Learn more"
                               }
                           }
                       },

                       new PlansAll
                       {
                           Img = "/images/business.png",
                           MenuItem = "Business",
                           Plans = new List<PlansCard>()
                           {
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <circle cx=\"12\" cy=\"12\" r=\"10\" opacity=\"0.12\"></circle>\r\n <path d=\"M7 13h10v2H7zM7 9h10v2H7z\"></path>\r\n</svg>",
                                   Title = "MeedDesk Essentials",
                                   Description = "Affordable meetings & chat for small teams with no Microsoft 365 subscription needed.",
                                   More = "See Details"
                               },
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n<circle cx=\"12\" cy=\"12\" r=\"10\" opacity=\"0.12\"></circle>\r\n <path d=\"M7 13h10v2H7zM7 9h10v2H7z\"></path>\r\n</svg>",
                                   Title = "MeetDesk with Microsoft 365 Business",
                                   Description = "All the collaboration features plus apps like Word, Excel, and secure cloud storage.",
                                   More = "See Details"
                               }
                           }
                       },

                       new PlansAll
                       {
                           Img = "/images/enterprise.png",
                           MenuItem = "Network",
                           Plans = new List<PlansCard>()
                           {
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M4 4h16v16H4z\" opacity=\"0.12\"></path>\r\n <path d=\"M7 7h6v10H7zM15 7h2v10h-2z\"></path>\r\n</svg>",
                                   Title = "MettDesk E3",
                                   Description = "Advanced meetings, security, compliance, and device management for large orgs.",
                                   More = "Explore"
                               },
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M12 2l9 5v10l-9 5-9-5V7z\" opacity=\"0.12\"></path>\r\n <path d=\"M7 9h10v2H7zM7 12h10v2H7z\"></path>\r\n </svg>",
                                   Title = "MettDesk E5",
                                   Description = "Premium voice, analytics, and top-tier security add-ons for mission-critical needs.",
                                   More = "Explore"
                               }
                           }
                       },

                       new PlansAll
                       {
                           Img = "/images/education.png",
                           MenuItem = "Education",
                           Plans = new List<PlansCard>()
                           {
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M2 7l10-5 10 5-10 5z\"></path>\r\n<path d=\"M4 12v5l8 4 8-4v-5\" opacity=\"0.12\"></path>\r\n</svg>",
                                   Title = "MeetDesk for Education",
                                   Description = "Build classes, assignments, and secure collaboration for schools.",
                                   More = "Learn More"
                               }
                           }
                       }

                    }
                },


                new PlansSection
                {
                    Language = "ka",
                    PlansTitle = "პროდუქტები და მომსახურება",
                    PlansDescription = "იპოვეთ თქვენი საჭიროებებისთვის შესაფერისი MeetDesk-ის გეგმა და დამატებები",
                    AllPlans = new List<PlansAll>()
                    {
                       new PlansAll
                       {
                           Img = "/images/home.png",
                           MenuItem = "მთავარი",
                           Plans = new List<PlansCard>()
                           {
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M4 4h16v16H4z\" opacity=\"0.12\"></path>\r\n <path d=\"M7 7h6v10H7zM15 7h2v10h-2z\"></path>\r\n </svg>",
                                   Title = "შეხვედრების მაგიდა (უფასოა)",
                                   Description = "გაგზავნეთ შეტყობინებები, დაგეგმეთ ზარები 60 წუთამდე და შექმენით საზოგადოებები ყველა ინტერესისთვის.",
                                   More = "შეიტყვეთ მეტი"
                               },
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n<circle cx=\"12\" cy=\"12\" r=\"10\" opacity=\"0.12\"></circle>\r\n <path d=\"M7 13h10v2H7zM7 9h10v2H7z\"></path>\r\n</svg>",
                                   Title = "Meet Desk (უფასოა)",
                                   Description = "გაგზავნეთ შეტყობინებები, დაგეგმეთ ზარები 60 წუთამდე და შექმენით საზოგადოებები ყველა ინტერესისთვის.",
                                   More = "შეიტყვეთ მეტი"
                               },
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <rect x=\"3\" y=\"4\" width=\"18\" height=\"14\" rx=\"3\" opacity=\"0.12\"></rect>\r\n <path d=\"M7 9h10v2H7zM7 12h6v2H7z\"></path>\r\n </svg>",
                                   Title = "Setup Desk (უფასოა)",
                                   Description = "გაგზავნეთ შეტყობინებები, დაგეგმეთ ზარები 60 წუთამდე და შექმენით საზოგადოებები ყველა ინტერესისთვის.",
                                   More = "შეიტყვეთ მეტი"
                               },
                               new PlansCard
                               {
                                   Icon = " <svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M4 4h16v16H4z\" opacity=\"0.12\"></path>\r\n <path d=\"M7 7h6v10H7zM15 7h2v10h-2z\"></path>\r\n </svg>",
                                   Title = "Meet Desk  ოჯახი",
                                   Description = "გაგზავნეთ შეტყობინებები, დაგეგმეთ ზარები 60 წუთამდე და შექმენით საზოგადოებები ყველა ინტერესისთვის.",
                                   More = "შეიტყვეთ მეტი"
                               }
                           }
                       },

                       new PlansAll
                       {
                           Img = "/images/business.png",
                           MenuItem = "ბიზნესი",
                           Plans = new List<PlansCard>()
                           {
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <circle cx=\"12\" cy=\"12\" r=\"10\" opacity=\"0.12\"></circle>\r\n <path d=\"M7 13h10v2H7zM7 9h10v2H7z\"></path>\r\n</svg>",
                                   Title = "MeedDesk აუცილებელი ნივთები",
                                   Description = "ხელმისაწვდომი შეხვედრები და ჩატი მცირე გუნდებისთვის, Microsoft 365-ის გამოწერის გარეშე..",
                                   More = "დეტალების ნახვა"
                               },
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n<circle cx=\"12\" cy=\"12\" r=\"10\" opacity=\"0.12\"></circle>\r\n <path d=\"M7 13h10v2H7zM7 9h10v2H7z\"></path>\r\n</svg>",
                                   Title = "MeetDesk Microsoft 365 Business-თან ერთად",
                                   Description = "ყველა თანამშრომლობის ფუნქცია პლუს აპლიკაციები, როგორიცაა Word, Excel და უსაფრთხო ღრუბლოვანი საცავი.",
                                   More = "დეტალების ნახვა"
                               }
                           }
                       },

                       new PlansAll
                       {
                           Img = "/images/enterprise.png",
                           MenuItem = "ქსელი",
                           Plans = new List<PlansCard>()
                           {
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M4 4h16v16H4z\" opacity=\"0.12\"></path>\r\n <path d=\"M7 7h6v10H7zM15 7h2v10h-2z\"></path>\r\n</svg>",
                                   Title = "MettDesk E3",
                                   Description = "გაფართოებული შეხვედრები, უსაფრთხოება, შესაბამისობა და მოწყობილობების მართვა დიდი ორგანიზაციებისთვის.",
                                   More = "დათვალიერება"
                               },
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M12 2l9 5v10l-9 5-9-5V7z\" opacity=\"0.12\"></path>\r\n <path d=\"M7 9h10v2H7zM7 12h10v2H7z\"></path>\r\n </svg>",
                                   Title = "MettDesk E5",
                                   Description = "პრემიუმ ხარისხის ხმოვანი, ანალიტიკური და უმაღლესი დონის უსაფრთხოების დამატებები მისიის კრიტიკულად მნიშვნელოვანი საჭიროებებისთვის.",
                                   More = "დათვალიერება"
                               }
                           }
                       },

                       new PlansAll
                       {
                           Img = "/images/education.png",
                           MenuItem = "განათლება",
                           Plans = new List<PlansCard>()
                           {
                               new PlansCard
                               {
                                   Icon = "<svg viewBox=\"0 0 24 24\" width=\"28\" height=\"28\" fill=\"#0078d4\">\r\n <path d=\"M2 7l10-5 10 5-10 5z\"></path>\r\n<path d=\"M4 12v5l8 4 8-4v-5\" opacity=\"0.12\"></path>\r\n</svg>",
                                   Title = "MeetDesk განათლებისთვის",
                                   Description = "შექმენით კლასები, დავალებები და უზრუნველყავით თანამშრომლობა სკოლებისთვის.",
                                   More = "მეტის გაგება"
                               }
                           }
                       }

                    }
                }
            };
            return plansSections;
        }
    }
}
