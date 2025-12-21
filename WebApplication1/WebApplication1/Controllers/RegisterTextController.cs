
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterTextController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromQuery] string lang = "en")
        {
            var texts = new Dictionary<string, object>
            {
                ["en"] = new
                {
                    title = "Create a new account",
                    first = "First name",
                    last = "Last name",
                    bday = "Birthday",
                    gender = "Gender",
                    female = "Female",
                    male = "Male",
                    custom = "Other",
                    phoneMail = "Mobile number or email",
                    password = "Password",
                    ConfirmPassword = "Confirm Password",
                    submit = "Sign Up",
                    back = "Back to login",
                },
                ["ka"] = new
                {
                    title = "ახალი ანგარიშის შექმნა",
                    first = "სახელი",
                    last = "გვარი",
                    bday = "დაბადების თარიღი",
                    gender = "სქესი",
                    female = "ქალი",
                    male = "კაცი",
                    custom = "სხვა",
                    phoneMail = "ტელეფონის ნომერი ან მეილი",
                    password = "პაროლი",
                    ConfirmPassword = "დაადასტურეთ პაროლი",
                    submit = "რეგისტრაცია",
                    back = "უკან ავტორიზაციაზე",
                }
            };

            if (!texts.ContainsKey(lang)) lang = "en";
            return Ok(texts[lang]);
        }
    }
}