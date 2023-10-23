using System;
using System.Collections.Generic;

namespace efcore.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public DateTime? DateCreated { get; set; }

    public int AuthorId { get; set; }

    public int ArticleId { get; set; }

    public int? Level { get; set; }

    public int? ParentId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

    public virtual Comment? Parent { get; set; }
}
