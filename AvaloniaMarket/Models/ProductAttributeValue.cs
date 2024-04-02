﻿using System;
using System.Collections.Generic;

namespace AvaloniaMarket.Models;

public partial class ProductAttributeValue
{
    public int ProductId { get; set; }

    public int AttributeId { get; set; }

    public string? Value { get; set; }

    public virtual ProductAttribute Attribute { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
