using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using AvaloniaMarket.Helpers;
using AvaloniaMarket.Models;

namespace AvaloniaMarket.ViewModels;

public class ItemPageViewModel : PageViewModelBase
{
    public Product Product { get; set; }
    public Category Category { get; set; }
    public IEnumerable<dynamic> Attributes { get; set; }
    public List<Image> Images { get; set; }

    public bool AdminPanel { get; set; } = false;
    
    public ItemPageViewModel(int? prId)
    {
        Product = DbHelper.Database.Products.Single(p => p.ProductId == prId);
        Category = DbHelper.Database.Categories.Single(c => c.CategoryId == Product.CategoryId);
        Images = new();
        
        string[] images = DbHelper.Database.ProductImages.Where(i => i.ProductId == prId)
            .OrderBy(i => i.ImageId)
            .Select(i => i.ImageUrl)
            .ToArray();
        
        // avares://AvaloniaMarket/Assets/Items/*/*.jpg
        foreach (var image in images)
            Images.Add(new Image 
                {Source = ImageHelper.LoadFromResource(new Uri(String.Concat("avares://AvaloniaMarket", image)))}
            );

        Attributes = from pav in DbHelper.Database.ProductAttributeValues
            join pa in DbHelper.Database.ProductAttributes on pav.AttributeId equals pa.AttributeId
            join p in DbHelper.Database.Products on pav.ProductId equals p.ProductId
            where p.ProductId == prId
            select new { AtName = pa.AttributeName, AtValue = pav.Value };

        if (Session.User is null)
            AdminPanel = false;
        else if (Session.User.IsAdmin)
            AdminPanel = true;
    }

    // Preview
    public ItemPageViewModel()
    {
        Product = DbHelper.Database.Products.OrderBy(p => p.ProductId).First();
        Category = DbHelper.Database.Categories.Single(c => c.CategoryId == Product.CategoryId);
        Images = new();
        
        string[] images = DbHelper.Database.ProductImages.Where(i => i.ProductId == Product.ProductId)
            .OrderBy(i => i.ImageId)
            .Select(i => i.ImageUrl)
            .ToArray();
        
        foreach (var image in images)
            Images.Add(new Image 
                {Source = ImageHelper.LoadFromResource(new Uri(String.Concat("avares://AvaloniaMarket", image)))}
            );
        
        Attributes = from pav in DbHelper.Database.ProductAttributeValues
            join pa in DbHelper.Database.ProductAttributes on pav.AttributeId equals pa.AttributeId
            join p in DbHelper.Database.Products on pav.ProductId equals p.ProductId
            where p.ProductId == Product.ProductId
            select new { AtName = pa.AttributeName, AtValue = pav.Value };

        AdminPanel = true;
    }
    
}