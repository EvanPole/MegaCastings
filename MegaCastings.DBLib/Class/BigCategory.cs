using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class BigCategory
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Announce> Announces { get; set; } = new List<Announce>();

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
