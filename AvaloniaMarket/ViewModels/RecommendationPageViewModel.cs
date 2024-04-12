using System;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaMarket.Helpers;
using AvaloniaMarket.Models;

namespace AvaloniaMarket.ViewModels;

public class RecommendationPageViewModel : PageViewModelBase
{
    public ObservableCollection<Product> Products { get; set; }
    
    public RecommendationPageViewModel()
    {
        Products = new(DbHelper.Database.Products.ToList());
    }
    
}