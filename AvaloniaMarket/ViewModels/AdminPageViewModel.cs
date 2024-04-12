using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AvaloniaMarket.Helpers;
using AvaloniaMarket.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace AvaloniaMarket.ViewModels;

public class AdminPageViewModel : PageViewModelBase
{
    public Product Product { get; set; }
    public List<string> Categories { get; set; }
    
    public List<dynamic> Attributes { get; set; }
    
    public bool ChangeProduct { get; set; }

    private string _selectedCategory;

    public string SelectedCategory
    {
        get => _selectedCategory;
        set => this.RaiseAndSetIfChanged(ref _selectedCategory, value);
    }
    
    public ICommand SaveCommand { get; set; }
    
    // Edit
    public AdminPageViewModel(int prid)
    {
        ChangeProduct = true;
        Product = DbHelper.Database.Products
            .Include(product => product.Category)
            .Single(p => p.ProductId == prid);
        
        LoadCategories();
        SelectedCategory = Product.Category.CategoryName;
        
        SaveCommand = ReactiveCommand.Create(SaveEdit);
    }

    // Create
    public AdminPageViewModel(bool create)
    {
        ChangeProduct = true;
        Product = new Product();
        LoadCategories();
        SaveCommand = ReactiveCommand.Create(SaveCreate);
    }

    // Preview | Edit
    public AdminPageViewModel()
    {
        ChangeProduct = true;
        Product = DbHelper.Database.Products
            .Include(product => product.Category)
            .First();
        
        LoadCategories();
        SelectedCategory = Product.Category.CategoryName;
    }

    private void LoadCategories()
    {
        Categories = new(DbHelper.Database.Categories.Select(c => c.CategoryName));
    }

    private void SaveEdit()
    {
        Product.CategoryId = DbHelper.Database.Categories.Single(c => c.CategoryName == SelectedCategory).CategoryId;
        DbHelper.Database.SaveChanges();
        Session.Window.ContentControl.Content = new ItemPageViewModel(Product.ProductId);
    }

    private void SaveCreate()
    {
        Product.Category = DbHelper.Database.Categories.Single(c => c.CategoryName == SelectedCategory);
        Product.CategoryId = Product.Category.CategoryId;
        DbHelper.Database.Products.Add(Product);
        DbHelper.Database.SaveChanges();
        Session.Window.ContentControl.Content = new RecommendationPageViewModel();
    }
    
}