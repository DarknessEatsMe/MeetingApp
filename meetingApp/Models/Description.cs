using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class Description
{
    public int DescrId { get; set; }

    public string? Decsr { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
