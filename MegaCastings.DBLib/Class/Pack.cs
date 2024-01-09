using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class Pack
{
    public int Id { get; set; }

    public string? Label { get; set; }

    public string? Desc { get; set; }

    public float? Price { get; set; }

    public virtual ICollection<Partner> Partners { get; set; } = new List<Partner>();
}
