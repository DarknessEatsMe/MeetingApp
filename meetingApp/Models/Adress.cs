using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class Adress
{
    public int IdAdress { get; set; }

    public int IdCity { get; set; }

    public int IdUser { get; set; }

    public virtual City IdCityNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
