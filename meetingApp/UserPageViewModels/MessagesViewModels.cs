using meetingApp.Models;

namespace meetingApp.UserPageViewModels
{
    public class MessagesViewModels
    {
        public User? User { get; set; }
        public IEnumerable<User>? Users { get; set; }
        public Chat? Chat { get; set; }
        public IEnumerable<Message>? Messages { get; set; }
        
    }
}
