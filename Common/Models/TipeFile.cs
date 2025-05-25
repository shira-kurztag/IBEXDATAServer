using System;
using System.Collections.Generic;

namespace IBEXDATA.Models;

public partial class TipeFile
{
    public int Code { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Magardoc> Magardocs { get; set; } = new List<Magardoc>();
}
