using System;
using System.Collections.Generic;

namespace AvaloniaMarket.Models;

public partial class PromoCode
{
    public int PromoCodeId { get; set; }

    public string Code { get; set; } = null!;

    public decimal Discount { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
