using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class Match
{
    public int IdMatch { get; set; }

    public int IdUser1 { get; set; }

    public int IdUser2 { get; set; }

    public int StatId { get; set; }

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public virtual User IdUser1Navigation { get; set; } = null!;

    public virtual User IdUser2Navigation { get; set; } = null!;

    public virtual Status Stat { get; set; } = null!;
}
