using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Registration
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string AdName { get; set; } = null!;

    public decimal PricePerTimeUnit { get; set; }

    public virtual ICollection<AdOrder> AdOrders { get; set; } = new List<AdOrder>();
}
