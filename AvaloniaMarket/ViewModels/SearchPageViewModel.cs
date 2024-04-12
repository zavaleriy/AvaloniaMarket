using System.Collections.Generic;
using System.Linq;
using AvaloniaMarket.Helpers;
using AvaloniaMarket.Models;

namespace AvaloniaMarket.ViewModels;

public class SearchPageViewModel : PageViewModelBase
{
    public List<Product> Products { get; set; }
    public Dictionary<int?, string> Images { get; set; }
    
    /// <summary>
    /// Find by name of product
    /// </summary>
    /// <param name="itemToSearch">Product name</param>
    public SearchPageViewModel(string? itemToSearch)
    {
        Products = itemToSearch is not null 
            ? DbHelper.Database.Products.Where(p => p.ProductName.Contains(itemToSearch)).ToList() 
            : DbHelper.Database.Products.ToList();

        Images = (from img in DbHelper.Database.ProductImages
            group img by img.ProductId into g
            select new
            {
                ProductId = g.Key,
                FirstImageUrl = g.OrderBy(i => i.ImageUrl).Select(i => i.ImageUrl).FirstOrDefault()
            }).ToDictionary(x => x.ProductId, x => x.FirstImageUrl);

    }
    
    /// <summary>
    /// Find by category
    /// </summary>
    public SearchPageViewModel(int categoryId)
    {
        Products = new(DbHelper.Database.Products.Where(p => p.CategoryId == categoryId));
        
        Images = (from img in DbHelper.Database.ProductImages
            group img by img.ProductId into g
            select new
            {
                ProductId = g.Key,
                FirstImageUrl = g.OrderBy(i => i.ImageUrl).Select(i => i.ImageUrl).FirstOrDefault()
            }).ToDictionary(x => x.ProductId, x => x.FirstImageUrl);
        
    }
    
    // Preview
    public SearchPageViewModel()
    {
        Products = DbHelper.Database.Products.ToList();
        
        Images = (from img in DbHelper.Database.ProductImages
            group img by img.ProductId into g
            select new
            {
                ProductId = g.Key,
                FirstImageUrl = g.OrderBy(i => i.ImageUrl).Select(i => i.ImageUrl).FirstOrDefault()
            }).ToDictionary(x => x.ProductId, x => x.FirstImageUrl);
    }
    
    public string? GetFirstImageUrl(int productId)
    {
        if (Images.TryGetValue(productId, out var firstImageUrl))
        {
            return firstImageUrl;
        }

        return null;
    }
    
}