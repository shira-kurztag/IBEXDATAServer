using System;
using System.Collections.Generic;

namespace IBEXDATA.Models;

public partial class Magardoc
{
    public int Id { get; set; }

    public string UniqId { get; set; } = null!;

    public string LogId { get; set; } = null!;

    public string FileNameShow { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int DocId { get; set; }

    public virtual TipeFile Doc { get; set; } = null!;
}
