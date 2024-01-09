using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class AnnounceCandidate
{
    public int Id { get; set; }

    public int Announceid { get; set; }

    public int Candidatesid { get; set; }

    public virtual Announce Announce { get; set; } = null!;

    public virtual User Candidates { get; set; } = null!;
}
