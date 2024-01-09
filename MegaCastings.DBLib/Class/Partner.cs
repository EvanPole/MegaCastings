using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class Partner
{
    public int Id { get; set; }

    public string? Label { get; set; }

    public string? Siret { get; set; }

    public string? Desc { get; set; }

    public DateTime? Datetime { get; set; }

    public int? Bigcategoryid { get; set; }

    public int? Packid { get; set; }

    public int? Isactive { get; set; }

    public virtual BigCategory? Bigcategory { get; set; }

    public virtual Pack? Pack { get; set; }
}
