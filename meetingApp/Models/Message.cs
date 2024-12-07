using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class Message
{
    public int MsgId { get; set; }

    public int ChatId { get; set; }

    public int IdUser { get; set; }

    public string Msg { get; set; } = null!;

    public DateTime MsgDate { get; set; }

    public virtual Chat Chat { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
