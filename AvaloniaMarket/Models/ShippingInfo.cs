using System;
using System.Collections.Generic;

namespace AvaloniaMarket.Models;

public partial class ShippingInfo
{
    public int ShippingId { get; set; }

    public int? OrderId { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public DateTime? ShippingDate { get; set; }

    public string DeliveryStatus { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
