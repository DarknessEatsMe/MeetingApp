using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using meetingApp.Models;
using System.Security.Claims;

namespace meetingApp.Controllers
{
    public class MeetingController : Controller
    {
        private static List<int> idUserBefore = new List<int>() { 0, 0, 0, 0, 0};

        [Authorize]
        public IActionResult MeetingPage()
        {

            return View();
        }

        public JsonResult GetJsonUser()
        {
            using (MeetingAppContext db = new MeetingAppContext())
            {
                var usersID = from user in db.Users.ToList()
                            orderby user.IdUser
                            select user.IdUser;
                int randomUserId = rndId(usersID);
                while (randomUserId == Convert.ToInt32(User.FindFirst(ClaimTypes.Name).Value) || idUserBefore.Contains(randomUserId))
                {
                    randomUserId = rndId(usersID);
                }
                User? rndUser = db.Users.FirstOrDefault(u => u.IdUser == randomUserId);
                idUserBefore.RemoveAt(0);
                idUserBefore.Add(randomUserId);
                return Json(rndUser);
            }
        }

        public JsonResult GetAdress(int id)
        {
            using (MeetingAppContext db = new MeetingAppContext())
            {
                Adress? adr = db.Adresses.FirstOrDefault(u => u.IdUser == id);
                City? c = db.Cities.FirstOrDefault(c => c.IdCity == adr.IdCity);
                return Json(c);
            }
        }

        public JsonResult GetDescr(int id)
        {
            using (MeetingAppContext db = new MeetingAppContext())
            {
                Description? des = db.Descriptions.FirstOrDefault(d => d.IdUser == id);
                if(des != null)
                {
                    return Json(des);
                }
                return Json(null);
            }
        }

        public JsonResult GetImg(int id)
        {
            using (MeetingAppContext db = new MeetingAppContext())
            {
                Photo? img = db.Photos.FirstOrDefault(p => p.IdUser == id);
                if(img != null) return Json(img);
                return Json(null);
            }
        }

        public IActionResult DoMatch(int UserId)
        {
            using(MeetingAppContext db = new MeetingAppContext())
            {
                Match? match = new Match();
                match.IdUser1 = Convert.ToInt32(User.FindFirst(ClaimTypes.Name).Value);
                match.IdUser2 = UserId;
                db.Matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("UserPage", "User", new {id = match.IdUser1});
            }
        }

        [Authorize]
        public IActionResult Like(int? idMatch)
        {
            using(MeetingAppContext db = new MeetingAppContext())
            {
                if (idMatch != null)
                {
                    Match? match = db.Matches.FirstOrDefault(m => m.IdMatch == idMatch);
                    if (match != null)
                    {
                        match.StatId = 1;
                        db.Matches.Update(match);
                        db.SaveChanges();
                        return RedirectToAction("UserPage", "User", new { id = User.FindFirst(ClaimTypes.Name).Value });
                    }
                    else
                    {
                        return RedirectToAction("UserPage", "User", new { id = User.FindFirst(ClaimTypes.Name).Value });
                    }
                }
                else
                {
                    return NotFound();
                }
            }
        }


        [NonAction]
        private int rndId(IEnumerable<int> usersId)
        {
            Random random = new Random();
            int index = random.Next(0, usersId.Count());
            return usersId.ElementAt(index);

        }
    }
}
