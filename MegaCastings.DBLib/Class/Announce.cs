using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class Announce
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int? Bigcategoryid { get; set; }

    public int? Subcategoryid { get; set; }

    public DateTime? Datetime { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public int? Contracttypeid { get; set; }

    public int? Hourlyrate { get; set; }

    public sbyte? Isactive { get; set; }

    public virtual ICollection<AnnounceCandidate> AnnounceCandidates { get; set; } = new List<AnnounceCandidate>();

    public virtual BigCategory? Bigcategory { get; set; }

    public virtual Contract? Contracttype { get; set; }

    public virtual SubCategory? Subcategory { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
