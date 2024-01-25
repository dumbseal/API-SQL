using System;
using System.Collections.Generic;

namespace Prak.Models;

public partial class User
{
    public string Names { get; set; } = null!;

    public int UserIds { get; set; }

    public string Passwords { get; set; } = null!;

    public string Logins { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? ReadingCategory { get; set; }

    public int? MadeArticles { get; set; }

    public int RoleLvl { get; set; }

    public string? Comments { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<Comment> CommentsNavigation { get; set; } = new List<Comment>();

    public virtual Role RoleLvlNavigation { get; set; } = null!;
}
