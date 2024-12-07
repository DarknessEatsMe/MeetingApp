using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string SecName { get; set; } = null!;

    public string? FatherName { get; set; }

    public char? Sex { get; set; }

    public DateTime Birthday { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual Adress? Adress { get; set; }

    public virtual Description? Description { get; set; }

    public virtual Login? Login { get; set; }

    public virtual ICollection<Match> MatchIdUser1Navigations { get; set; } = new List<Match>();

    public virtual ICollection<Match> MatchIdUser2Navigations { get; set; } = new List<Match>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Photo? Photo { get; set; }
}
