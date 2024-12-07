using System;
using System.Collections.Generic;

namespace meetingApp.Models;

public partial class Login
{
    public int IdUser { get; set; }

    public string Login1 { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
