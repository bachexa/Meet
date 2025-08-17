using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionsSectionController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var dsList = BuildSolutionSectionList();
            var section = dsList.FirstOrDefault(s => s.Language == lang);

            if (section == null)
                return NotFound();

            return Ok(section); // single object
        }

        private List<SolutionsSection> BuildSolutionSectionList()
        {
            var solutionSections = new List<SolutionsSection>{
                new SolutionsSection
                {
                    SolutionName = "SOLUTIONS",
                    SolutionDescription = "Streamline communications all in one place via <span>MeetDesk</span>",
                    Language = "en",
                    Cards = new List<SolutionCard>
                    {
                        new SolutionCard
                        {
                            SolutionCardName = "Meet",
                            SolutionCardDescription = "Make meetings more impactful with features like PowerPoint Live, Microsoft Whiteboard,and AI-generated meeting notes.",
                            SolutionCardButton = "Learn more",
                            SolutionCardSilderImg = "images/solutions-sample.png",
                            Language = "en"
                        },
                        new SolutionCard
                        {
                            SolutionCardName = "Call",
                            SolutionCardDescription = "Connect instantly with high-quality voice and video calls.",
                            SolutionCardButton = "Learn more",
                            SolutionCardSilderImg = "images/solutions-sample-2.png",
                            Language = "en"
                        },
                        new SolutionCard
                        {
                            SolutionCardName = "Collaborate",
                            SolutionCardDescription = "Work together seamlessly with shared documents and tasks.",
                            SolutionCardButton = "Learn more",
                            SolutionCardSilderImg = "solutions-sample-3.png",
                            Language= "en"
                        },
                        new SolutionCard
                        {
                            SolutionCardName = "Chat",
                            SolutionCardDescription = "Stay connected with direct and group messaging.",
                            SolutionCardButton = "Learn more",
                            SolutionCardSilderImg = "images/solutions-sample.png",
                            Language= "en"
                        },
                        new SolutionCard
                        {
                            SolutionCardName = "Search Free Persons",
                            SolutionCardDescription = "Stay connected with direct and group messaging.",
                            SolutionCardButton = "Learn more",
                            SolutionCardSilderImg = "images/solutions-sample-2.png",
                            Language= "en"
                        }
                    }
                },

                new SolutionsSection
                {
                    SolutionName = "გადაწყვეტილებები",
                    SolutionDescription = "გაამარტივეთ კომუნიკაციები ერთ ადგილას <span>MeetDesk</span>",
                    Language = "ka",
                    Cards = new List<SolutionCard>
                    {
                        new SolutionCard
                        {
                            SolutionCardName = "შეხვედრა",
                            SolutionCardDescription = "გახადეთ შეხვედრები უფრო შთამბეჭდავი ისეთი ფუნქციებით, როგორიცაა PowerPoint Live, Microsoft Whiteboard და ხელოვნური ინტელექტის მიერ გენერირებული შეხვედრების ჩანაწერები.",
                            SolutionCardButton = "შეიტყვეთ მეტი",
                            SolutionCardSilderImg = "images/solutions-sample.png",
                            Language = "ka"
                        },
                        new SolutionCard
                        {
                            SolutionCardName = "ზარი",
                            SolutionCardDescription = "მყისიერად დაუკავშირდით მაღალი ხარისხის ხმოვან და ვიდეო ზარებს.",
                            SolutionCardButton = "შეიტყვეთ მეტი",
                            SolutionCardSilderImg = "images/solutions-sample-2.png",
                            Language = "ka"
                        },
                        new SolutionCard
                        {
                            SolutionCardName = "თანამშრომლობა",
                            SolutionCardDescription = "შეუფერხებლად იმუშავეთ ერთად გაზიარებულ დოკუმენტებთან და დავალებებთან.",
                            SolutionCardButton = "შეიტყვეთ მეტი",
                            SolutionCardSilderImg = "solutions-sample-3.png",
                            Language= "ka"
                        },
                        new SolutionCard
                        {
                            SolutionCardName = "ჩატი",
                            SolutionCardDescription = "დარჩით კავშირზე პირდაპირი და ჯგუფური შეტყობინებებით.",
                            SolutionCardButton = "შეიტყვეთ მეტი",
                            SolutionCardSilderImg = "images/solutions-sample.png",
                            Language= "ka"
                        },
                        new SolutionCard
                        {
                            SolutionCardName = "თავისუფალი პირების ძიება",
                            SolutionCardDescription = "დარჩით კავშირზე პირდაპირი და ჯგუფური შეტყობინებებით.",
                            SolutionCardButton = "შეიტყვეთ მეტი",
                            SolutionCardSilderImg = "images/solutions-sample-2.png",
                            Language= "ka"
                        }
                    }
                }

            };

            return solutionSections;
        }


    }
}
