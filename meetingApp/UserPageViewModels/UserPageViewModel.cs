using meetingApp.Models;
using System;

namespace meetingApp.UserPageViewModels
{
    public class UserPageViewModel
    {

        public User? User { get; set; }
        public City? City { get; set; }
        public Description? Description { get; set; }
        public Photo? Photo { get; set; } 
        public Adress? Adress { get; set; }
        public IEnumerable<User>? Users { get; set; }
        public IEnumerable<User>? WhoLikeMe { get; set; }
        public IEnumerable<Match>? Matches { get; set; }
        public IEnumerable<Match>? MyMatches { get; set; }
        public IEnumerable<Status>? Statuses { get; set; }
        public IEnumerable<Chat>? Chats { get; set; }

    }
}
