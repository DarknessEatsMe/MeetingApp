using meetingApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using meetingApp.UserPageViewModels;

namespace meetingApp.Controllers
{
	public class UserController : Controller
	{
        public static string passErr = "Pass";

        [Authorize]
        public IActionResult UserPage(int? id)
		{
            using (MeetingAppContext db = new MeetingAppContext())
            {
                int? idInt = Convert.ToInt32(User.FindFirst(ClaimTypes.Name).Value);
                if (id != null && id == idInt)
                {
                    User? user = db.Users.FirstOrDefault(u => u.IdUser == id);


                    if (user != null)
                    {
                        Adress? adr = db.Adresses.FirstOrDefault(a => a.IdUser == user.IdUser);
                        City? city = db.Cities.FirstOrDefault(c => c.IdCity == adr.IdCity);
                        Description? descr = db.Descriptions.FirstOrDefault(d => d.IdUser == user.IdUser);
                        Photo? img = db.Photos.FirstOrDefault(i => i.IdUser == user.IdUser);

                        var matchesOfUser = from m in db.Matches.ToList()
                                            where m.IdUser1 == id
                                            select m;

                        var users = from u in db.Users.ToList()
                                    join m in db.Matches.ToList() on u.IdUser equals m.IdUser2
                                    where m.IdUser1 == id
                                    select u;



                        var myMatches = from m in db.Matches.ToList()
                                        where m.IdUser2 == id && m.StatId == 2
                                        select m;

                        var whoLikeMe = from u in db.Users.ToList()
                                        join m in db.Matches.ToList() on u.IdUser equals m.IdUser1
                                        where m.IdUser2 == id && m.StatId == 2
                                        select u;

                        var statuses = from s in db.Statuses.ToList()
                                       select s;

                        UserPageViewModel upvm = new UserPageViewModel{
                            User = user, 
                            Users = users, 
                            Matches = matchesOfUser, 
                            Statuses = statuses, 
                            MyMatches = myMatches, 
                            WhoLikeMe = whoLikeMe,
                            Photo = img,
                            City = city,
                            Description = descr,
                        };

                        return View(upvm);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NotFound();
                } 
            }
		}

        
        public IActionResult LoginPage()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserPage", "User", new { id = Convert.ToInt32(User.FindFirst(ClaimTypes.Name).Value) });
            }
            else { 
                return View();
            }
        }

        [HttpPost]
        public IActionResult LoginResult(string Login, string Pass)
        {
            using (MeetingAppContext db = new MeetingAppContext())
            {
                if (Login != null || Pass != null)
                {
                    Login? log = db.Logins.FirstOrDefault(p => p.Login1 == Login && p.Password == Pass);

                    if (log == null)
                    {
                        return NotFound();
                    }

                    var claims = new List<Claim> {
                        new Claim(ClaimTypes.Name, Convert.ToString(log.IdUser))
                    };

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("UserPage", "User", new { id = log.IdUser });
                }
                else
                {
                    return RedirectToAction("LoginPage");
                }
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user, string? descr, int cityId, string Login, string Pass)
        {
            using(MeetingAppContext db = new MeetingAppContext())
            {
                if (db.Logins.FirstOrDefault(l => l.Login1 == Login) != null)
                {
                    return RedirectToAction("Register");
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    Description des = new Description();
                    des.Decsr = descr;
                    des.IdUser = user.IdUser;
                    db.Descriptions.Add(des);

                    Adress adr = new Adress();
                    adr.IdCity = cityId;
                    adr.IdUser = user.IdUser;
                    db.Adresses.Add(adr);


                    if (Request.Form.Files.Count != 0)
                    {
                        var file = Request.Form.Files[0];

                        Photo photo = new Photo();
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);

                        photo.PhotoAdr = ms.ToArray();
                        photo.IdUser = user.IdUser;

                        ms.Close();
                        ms.Dispose();

                        db.Photos.Add(photo);
                    }

                    Login log = new Login();
                    log.Login1 = Login;
                    log.Password = Pass;
                    log.IdUser = user.IdUser;
                    db.Logins.Add(log);

                    db.SaveChanges();
                    return RedirectToAction("LoginPage", "User");
                }
            }
        }

        public IActionResult Exit()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Chats(int? id)
        {
            using (MeetingAppContext db = new MeetingAppContext())
            {

                var chats = from c in db.Chats.ToList()
                            join m in db.Matches.ToList() on c.IdMatch equals m.IdMatch
                            where m.IdUser1 == id || m.IdUser2 == id
                            select c;
                User? user = db.Users.FirstOrDefault(u => u.IdUser == id);

                UserPageViewModel upvm = new UserPageViewModel { User = user, Chats = chats};
                return View(upvm);
            }
        }

        public IActionResult ShowChat(int? id)
        {
            if(id != null)
            {
                int? uId = Convert.ToInt32(User.FindFirst(ClaimTypes.Name).Value);
                using (MeetingAppContext db = new MeetingAppContext())
                {
                    Chat? c = db.Chats.FirstOrDefault(c => c.ChatId == id);
                    var msgs = from msg in db.Messages.ToList()
                               where msg.ChatId == id
                               orderby msg.MsgDate descending
                               select msg;

                    Match? m = db.Matches.FirstOrDefault(m => m.IdMatch == c.IdMatch);

                    User? u1 = db.Users.FirstOrDefault(u => u.IdUser == m.IdUser1);
                    User? u2 = db.Users.FirstOrDefault(u => u.IdUser == m.IdUser2);
                    User[] usersArr = { u1, u2 };
                    IEnumerable<User> users = usersArr.ToList();

                    User? user = db.Users.FirstOrDefault(u => u.IdUser == uId);

                    MessagesViewModels mvm = new MessagesViewModels { Chat = c, Messages = msgs, Users = users, User = user };
                    return View(mvm);
                }
            }
            return NotFound();
        }

        public IActionResult SendMsg(int uId, int chatId, string message)
        {
            using(MeetingAppContext db = new MeetingAppContext())
            {
                Message msg = new Message();
                msg.ChatId = chatId;
                msg.IdUser = uId;
                msg.Msg = message;
                msg.MsgDate = DateTime.Now;

                db.Messages.Add(msg);
                db.SaveChanges();

                return RedirectToAction("ShowChat", "User", new { id = chatId });
            }
        }

        public IActionResult ChangeInfo(int? id)
        {
            using(MeetingAppContext db = new MeetingAppContext())
            {
                User? u = db.Users.FirstOrDefault(u => u.IdUser == id);
                Description? descr = db.Descriptions.FirstOrDefault(d => d.IdUser == id);
                Adress? adr = db.Adresses.FirstOrDefault(a => a.IdUser == id);
                City? c = db.Cities.FirstOrDefault(c => c.IdCity == adr.IdCity);
                UserPageViewModel upvm = new UserPageViewModel { User = u, Description = descr, Adress = adr, City = c};
                return View(upvm);
            }
        }

        [HttpPost]
        public IActionResult ChangeInfo(User user, string? descr, int cityId)
        {
            using(MeetingAppContext db = new MeetingAppContext())
            {

                db.Users.Update(user);
                db.SaveChanges();

                Description? d = db.Descriptions.FirstOrDefault(d => d.IdUser == user.IdUser);
                if(d != null)
                {
                    d.Decsr = descr;
                    db.Descriptions.Update(d);
                }
                else
                {
                    Description des = new Description();
                    des.Decsr = descr;
                    des.IdUser = user.IdUser;
                    db.Descriptions.Add(des);
                }

                Adress? adr = db.Adresses.FirstOrDefault(a => a.IdUser == user.IdUser);
                adr.IdCity = cityId;
                db.Adresses.Update(adr);

                Photo? img = db.Photos.FirstOrDefault(p => p.IdUser == user.IdUser);

                if(Request.Form.Files.Count != 0)
                {
                    var file = Request.Form.Files[0];

                    if(img == null)
                    {
                        Photo photo = new Photo();
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);

                        photo.PhotoAdr = ms.ToArray();
                        photo.IdUser = user.IdUser;

                        ms.Close();
                        ms.Dispose();

                        db.Photos.Add(photo);
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        file.CopyTo(ms);

                        img.PhotoAdr = ms.ToArray();

                        ms.Close();
                        ms.Dispose();

                        db.Photos.Update(img);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("UserPage", "User", new { id = user.IdUser });
            }
        }
    }
}
