using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class Contract
{
    public int Id { get; set; }

    public string Label { get; set; } = null!;

    public string Abbreviation { get; set; } = null!;

    public virtual ICollection<Announce> Announces { get; set; } = new List<Announce>();
}
