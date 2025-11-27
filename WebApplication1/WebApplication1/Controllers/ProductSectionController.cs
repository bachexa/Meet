
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
            return new List<ProductSectionModel>
            {
                // ========== EN ==========
                new ProductSectionModel
                {
                    ProductSectionTitle = "Find the right MeetDesk plan",
                    ProductSectionDescription = "Choose the plan that fits your needs",
                    Language = "en",
                    ProductCards = new List<ProductCardModel>
                    {
                        // Home
                        new ProductCardModel
                        {
                            ProductCardTitle = "Meet Desk (Free)",
                            ProductCardDescription = "Send messages, schedule calls for up to 60 minutes, and create communities.",
                            ProductCardButton = "Learn more",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Setup Desk",
                            ProductCardDescription = "Setup Desk apps for enhanced collaboration and security.",
                            ProductCardButton = "Get started",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                                    <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },

                        // Business
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams Essentials",
                            ProductCardDescription = "Affordable meetings & chat for small teams with no Microsoft 365 subscription needed.",
                            ProductCardButton = "See details",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
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
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                                    <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "business"
                        },

                        // Enterprise
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams E3",
                            ProductCardDescription = "Advanced meetings, security, compliance, and device management for large orgs.",
                            ProductCardButton = "Explore",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "enterprise"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams E5",
                            ProductCardDescription = "Premium voice, analytics, and top-tier security add-ons for mission-critical needs.",
                            ProductCardButton = "Explore",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
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
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
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
                    ProductCards = new List<ProductCardModel>
                    {
                        // Home
                        new ProductCardModel
                        {
                            ProductCardTitle = "Meet Desk (უფასო)",
                            ProductCardDescription = "გადაგზავნეთ შეტყობინებები, დაგეგმეთ ზარები 60 წუთამდე და შექმენით საზოგადოებები.",
                            ProductCardButton = "გაიგე მეტი",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Setup Desk",
                            ProductCardDescription = "Setup Desk აპლიკაციები თანამშრომლობისა და უსაფრთხოების გასაუმჯობესებლად.",
                            ProductCardButton = "დაიწყეთ",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                                    <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "home"
                        },

                        // Business
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams Essentials",
                            ProductCardDescription = "აფორმი შეხვედრები და ჩეთი მცირე გუნდებისთვის Microsoft 365-ის გამოწერის გარეშე.",
                            ProductCardButton = "დეტალები",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <circle cx='12' cy='12' r='10' opacity='0.12'></circle>
                                                    <path d='M7 13h10v2H7zM7 9h10v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "business"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams with Microsoft 365 Business",
                            ProductCardDescription = "ყველა თანამშრომლობის ფუნქცია Word, Excel და უსაფრთხო Cloud შენახვით.",
                            ProductCardButton = "დეტალები",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <rect x='3' y='4' width='18' height='14' rx='3' opacity='0.12'></rect>
                                                    <path d='M7 9h10v2H7zM7 12h6v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "business"
                        },

                        // Enterprise
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams E3",
                            ProductCardDescription = "გაწვდილი შეხვედრები, უსაფრთხოება და მოწყობილობების მართვა დიდ ორგანიზაციებისთვის.",
                            ProductCardButton = "გაიგე მეტი",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <path d='M4 4h16v16H4z' opacity='0.12'></path>
                                                    <path d='M7 7h6v10H7zM15 7h2v10h-2z'></path>
                                                </svg>",
                            ProductCardPanel = "enterprise"
                        },
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams E5",
                            ProductCardDescription = "პრემიუმ ხმოვანი, ანალიტიკური და უსაფრთხოების დამატებები მნიშვნელოვანი საჭიროებისთვის.",
                            ProductCardButton = "გაიგე მეტი",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
                                                    <path d='M12 2l9 5v10l-9 5-9-5V7z' opacity='0.12'></path>
                                                    <path d='M7 9h10v2H7zM7 12h10v2H7z'></path>
                                                </svg>",
                            ProductCardPanel = "enterprise"
                        },

                        // Education
                        new ProductCardModel
                        {
                            ProductCardTitle = "Teams for Education",
                            ProductCardDescription = "კლასების, დავალებების და უსაფრთხო თანამშრომლობის შექმნა სკოლებისთვის.",
                            ProductCardButton = "გაიგე მეტი",
                            ProductCardSvg = @"<svg viewBox='0 0 24 24' width='28' height='28' fill='#0078d4'>
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