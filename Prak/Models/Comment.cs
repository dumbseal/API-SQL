using System;
using System.Collections.Generic;

namespace Prak.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int UserIds { get; set; }

    public string Content { get; set; } = null!;

    public int? PositiveRatings { get; set; }

    public int? NegativeRatings { get; set; }

    public virtual Answer? Answer { get; set; }

    public virtual User UserIds1 { get; set; } = null!;

    public virtual Article UserIdsNavigation { get; set; } = null!;
}
