using System;
using System.Collections.Generic;

namespace MegaCastings.DBLib.Class;

public partial class UsersAdmin
{
    public uint Id { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? Perm { get; set; }

    public sbyte? Isactive { get; set; }
}
