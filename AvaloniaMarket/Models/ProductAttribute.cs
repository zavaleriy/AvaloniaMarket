using System;
using System.Collections.Generic;

namespace AvaloniaMarket.Models;

public partial class ProductAttribute
{
    public int AttributeId { get; set; }

    public string AttributeName { get; set; } = null!;

    public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; } = new List<ProductAttributeValue>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
