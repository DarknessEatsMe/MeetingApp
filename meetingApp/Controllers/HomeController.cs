using Microsoft.AspNetCore.Mvc;
using meetingApp.Models;
using System.Security.Claims;

namespace meetingApp.Controllers
{
    public class HomeController : Controller
    {

        public static string passErr = "Pass";

        public IActionResult Index()
        {
            using(MeetingAppContext db = new MeetingAppContext())
            {
                if (User.Identity.IsAuthenticated)
                {
                    ViewBag.Profile = "Профиль";
                    ViewBag.Exit = "Выйти";
                }
                else
                {
                    ViewBag.Profile = "Войти";
                    ViewBag.Exit = null;
                }
                return View();
            }
        }
    }
}
