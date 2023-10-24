using System;
using System.Collections.Generic;

namespace efcore.Entities;

public partial class Article
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateTime? DateCreated { get; set; }

    public int AuthorId { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public override string ToString(){
                    return $"Id: {Id}, Title: {Title}, Content: {Content}";

    }


}
