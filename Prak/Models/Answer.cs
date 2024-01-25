using System;
using System.Collections.Generic;

namespace Prak.Models;

public partial class Answer
{
    public int CommentId { get; set; }

    public int UserIds { get; set; }

    public string Content { get; set; } = null!;

    public int? PositiveRatings { get; set; }

    public int? NegativeRatings { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User UserIdsNavigation { get; set; } = null!;
}
