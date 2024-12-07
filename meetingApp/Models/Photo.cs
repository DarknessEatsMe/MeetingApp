using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public byte[]? PhotoAdr { get; set; }

    public int IdUser { get; set; }

    public virtual User IdUserNavigation { get; set; } = null!;
}
