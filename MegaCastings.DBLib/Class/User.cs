using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace MegaCastings.DBLib.Class;

public partial class User
{
    public uint Id { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public string? Email { get; set; }
}
