using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class AdOrder
{
    public int Id { get; set; }

    public string Firm { get; set; } = null!;

    public string AdCode { get; set; } = null!;

    public string AdName { get; set; } = null!;

    public int DurationSeconds { get; set; }

    public decimal AdCost { get; set; }

    public virtual Registration AdCodeNavigation { get; set; } = null!;
}
