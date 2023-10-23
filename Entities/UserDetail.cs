using System;
using System.Collections.Generic;

namespace efcore.Entities;

public partial class UserDetail
{
    public int Id { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public DateOnly? DateBirth { get; set; }

    public string? Descrip { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
