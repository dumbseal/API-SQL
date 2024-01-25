using System;
using System.Collections.Generic;

namespace Prak.Models;

public partial class Article
{
    public int ArticleId { get; set; }

    public int UserIds { get; set; }

    public string Content { get; set; } = null!;

    public int? PositiveRatings { get; set; }

    public int? NegativeRatings { get; set; }

    public string Title { get; set; } = null!;

    public DateTime DatePublished { get; set; }

    public int Theme { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User UserIdsNavigation { get; set; } = null!;
}
