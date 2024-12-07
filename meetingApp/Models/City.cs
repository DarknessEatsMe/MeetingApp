using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class City
{
    public int IdCity { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();
}
