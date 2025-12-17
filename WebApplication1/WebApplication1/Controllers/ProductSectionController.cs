using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSectionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var sections = BuildProductSections();
            var section = sections.FirstOrDefault(s => s.Language == lang);
            if (section == null) return NotFound();
            return Ok(section);
        }

        private List<ProductSectionModel> BuildProductSections()
        {
            // common hero images
            var heroHome = "/images/home.png";
            var heroBusiness = "/images/business.png";
            var heroEnterprise = "/images/enterprise.png";
            var heroEducation = "/images/education.png";

            return new List<ProductSectionModel>
            {
                // ========== EN ==========
                new ProductSectionModel
                {
                    ProductSectionTitle = "Find the right MeetDesk plan",
                    ProductSectionDescription = "Choose the plan that fits your needs",
                    Language = "en",
                    HeroImage = heroHome, 
                    PanelHeroImages = new Dictionary<string,string>
                    {
                        ["home"] = heroHome,
                        ["business"] = heroBusiness,
                        ["enterprise"] = heroEnterprise,
                        ["education"] = heroEducation
                    },
                    TabLabels = new Dictionary<string,string>
                    {
                        ["home"] = "Home",
                        ["business"] = "Business",
                        ["enterprise"] = "Enterprise",
                        ["education"] = "Education"
                    },
                    ProductCards = new List<ProductCardModel>
                    {
                        // Home
                        new ProductCardModel
                        {
                            ProductCardTitle = "Meet Desk (Free)",
                            ProductCardDescription = "Send messages, schedule calls for up to 60 minutes and create communities for every interest.",
                            ProductCardButton = "Learn more",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Easy Communication",
                            ProductCardDescription = "Bring calls, messages and group chats together in one place for simple and convenient communication.",
                            ProductCardButton = "Get started",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <circle cx='12' cy='12' r='10' opacity='0.12'></circle>
                                                    <path d='M7 13h10v2H7zM7 9h10v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Secure & Reliable",
                            ProductCardDescription = "A secure system with stable connections and a reliable platform for daily use at home or at work.",
                            ProductCardButton = "Learn More",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                                    <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Built for Everyone",
                            ProductCardDescription = "Easy to use for everyone - Families,Students and Business teams alike",
                            ProductCardButton = "Learn More",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },

                        // Business
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams Essentials",
                            ProductCardDescription = "Affordable meetings & chat for small teams with no Microsoft 365 subscription needed.",
                            ProductCardButton = "See details",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <circle cx='12' cy='12' r='10' opacity='0.12'></circle>
                                                    <path d='M7 13h10v2H7zM7 9h10v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "business"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams with Microsoft 365 Business",
                            ProductCardDescription = "All collaboration features plus apps like Word, Excel, and secure cloud storage.",
                            ProductCardButton = "See details",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                                    <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "business"
                        },

                        // Enterprise
                        new ProductCardModel
                        {
                            ProductCardTitle = "SecureMeet",
                            ProductCardDescription = "Advanced meetings, security, compliance, and device management for large orgs.",
                            ProductCardButton = "Explore",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "enterprise"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "SecureVoice",
                            ProductCardDescription = "Premium voice, analytics, and top-tier security add-ons for mission-critical needs.",
                            ProductCardButton = "Explore",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M12 2l9 5v10l-9 5-9-5V7z' opacity='0.12'></path>
                                                    <path d='M7 9h10v2H7zM7 12h10v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "enterprise"
                        },

                        // Education
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams for Education",
                            ProductCardDescription = "Build classes, assignments, and secure collaboration for schools.",
                            ProductCardButton = "Learn more",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M2 7l10-5 10 5-10 5z'></path>
                                                    <path d='M4 12v5l8 4 8-4v-5' opacity='0.12'></path>
                                                </svg>",
                            ProductCardPanel = "education"
                        }
                    }
                },

                // ========== KA ==========
                new ProductSectionModel
                {
                    ProductSectionTitle = "გაარჩიე MeetDesk გეგმა",
                    ProductSectionDescription = "შეარჩიეთ გეგმა თქვენს საჭიროებებზე",
                    Language = "ka",
                    HeroImage = heroHome,
                    PanelHeroImages = new Dictionary<string,string>
                    {
                        ["home"] = heroHome,
                        ["business"] = heroBusiness,
                        ["enterprise"] = heroEnterprise,
                        ["education"] = heroEducation
                    },
                    TabLabels = new Dictionary<string,string>
                    {
                        ["home"] = "მთავარი",
                        ["business"] = "ბიზნესი",
                        ["enterprise"] = "ინდუსტრია",
                        ["education"] = "განათლება"
                    },
                    ProductCards = new List<ProductCardModel>
                    {
                        // Home
                        new ProductCardModel
                        {
                            ProductCardTitle = "Meet Desk (უფასო)",
                            ProductCardDescription = "გადაგზავნეთ შეტყობინებები, დაგეგმეთ ზარები 60 წუთამდე და შექმენით საზოგადოებები.",
                            ProductCardButton = "გაიგე მეტი",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "მარტივი კომუნიკაცია",
                            ProductCardDescription = "ერთ სივრცეში გააერთიანე ზარები, შეტყობინებები და ჯგუფური საუბრები მარტივი და მოსახერხებელი კომუნიკაციისთვის.",
                            ProductCardButton = "Get started",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <circle cx='12' cy='12' r='10' opacity='0.12'></circle>
                                                    <path d='M7 13h10v2H7zM7 9h10v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "უსაფრთხო და საიმედო",
                            ProductCardDescription = "დაცული სისტემა, სტაბილური კავშირი და სანდო პლატფორმა ყოველდღიური გამოყენებისათვის, სახლში და სამუშაოშიც.",
                            ProductCardButton = "Learn More",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                                    <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "შექმნილია ყველასთვის",
                            ProductCardDescription = "გამოყენება მარტივია ყველასთვის- ოჯახისთვის, სტუდენტებისთვის და ბიზნეს გუნდებისთვის ერთნაირად.",
                            ProductCardButton = "Learn More",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },

                        // Business
                        new ProductCardModel
                        {
                            ProductCardTitle = "გუნდების აუცილებელი ნივთები",
                            ProductCardDescription = "ხელმისაწვდომი შეხვედრები და ჩათი მცირე გუნდებისთვის Microsoft 365-ის გამოწერის გარეშე.",
                            ProductCardButton = "დეტალები",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <circle cx='12' cy='12' r='10' opacity='0.12'></circle>
                                                    <path d='M7 13h10v2H7zM7 9h10v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "business"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "გუნდები Microsoft 365 Business-თან",
                            ProductCardDescription = "ყველა თანამშრომლობის ფუნქცია Word, Excel და უსაფრთხო Cloud შენახვით.",
                            ProductCardButton = "დეტალები",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                                    <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "business"
                        },

                        // Enterprise
                        new ProductCardModel
                        {
                            ProductCardTitle = "უსაფრთხო შეხვედრები",
                            ProductCardDescription = "გაწვდილი შეხვედრები, უსაფრთხოება და მოწყობილობების მართვა დიდ ორგანიზაციებისთვის.",
                            ProductCardButton = "გაიგე მეტი",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "enterprise"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "უსაფრთხო ხმოვანი სერვისი",
                            ProductCardDescription = "პრემიუმ ხმოვანი, ანალიტიკური და უსაფრთხოების დამატებები მნიშვნელოვანი საჭიროებისთვის.",
                            ProductCardButton = "გაიგე მეტი",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M12 2l9 5v10l-9 5-9-5V7z' opacity='0.12'></path>
                                                    <path d='M7 9h10v2H7zM7 12h10v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "enterprise"
                        },

                        // Education
                        new ProductCardModel
                        {
                            ProductCardTitle = "განათლების გუნდები",
                            ProductCardDescription = "კლასების, დავალებების და უსაფრთხო თანამშრომლობის შექმნა სკოლებისთვის.",
                            ProductCardButton = "გაიგე მეტი",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#1f5c4e'>
                                                    <path d='M2 7l10-5 10 5-10 5z'></path>
                                                    <path d='M4 12v5l8 4 8-4v-5' opacity='0.12'></path>
                                                </svg>",
                            ProductCardPanel = "education"
                        }
                    }
                }
            };
        }
    }
}