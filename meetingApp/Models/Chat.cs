using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class Chat
{
    public int ChatId { get; set; }

    public int IdMatch { get; set; }

    public string ChatName { get; set; } = null!;

    public virtual Match IdMatchNavigation { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
}
