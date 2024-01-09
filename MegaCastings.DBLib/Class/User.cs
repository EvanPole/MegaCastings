using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class User
{
    public int Id { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateTime? Birthdate { get; set; }

    public int? Bigcategoryid { get; set; }

    public int? Subcategoryid { get; set; }

    public int? Annonceid { get; set; }

    public int? Isactive { get; set; }

    public virtual Announce? Annonce { get; set; }

    public virtual ICollection<AnnounceCandidate> AnnounceCandidates { get; set; } = new List<AnnounceCandidate>();

    public virtual BigCategory? Bigcategory { get; set; }

    public virtual SubCategory? Subcategory { get; set; }
}
