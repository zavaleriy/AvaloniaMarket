using System;
using System.Collections.Generic;

namespace AvaloniaMarket.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ShippingInfo> ShippingInfos { get; set; } = new List<ShippingInfo>();

    public virtual OrderStatus? Status { get; set; }

    public virtual User? User { get; set; }
}
