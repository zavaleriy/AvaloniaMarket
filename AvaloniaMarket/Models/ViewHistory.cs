using System;
using System.Collections.Generic;

namespace AvaloniaMarket.Models;

public partial class ViewHistory
{
    public int ViewId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public DateTime? ViewDate { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
