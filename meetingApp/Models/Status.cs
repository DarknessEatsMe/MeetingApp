using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class Status
{
    public int StatId { get; set; }

    public string Status1 { get; set; } = null!;

    public virtual ICollection<Match> Matches { get; set; } = new List<Match>();
}
