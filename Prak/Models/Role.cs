using System;
using System.Collections.Generic;

namespace Prak.Models;

public partial class Role
{
    public int RoleLvl { get; set; }

    public string Names { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
