using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using AvaloniaMarket.Helpers;
using AvaloniaMarket.ViewModels;

namespace AvaloniaMarket.Views;

public partial class ItemPageView : UserControl
{
    private ItemPageViewModel? _viewModel;
    public ItemPageView()
    {
        InitializeComponent();
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _viewModel = DataContext as ItemPageViewModel;
        if (Session.Cart.Contains(_viewModel!.Product.ProductId))
            BuyBtn.Content = "Перейти в корзину";
        if (Session.Favorite.Contains(_viewModel!.Product.ProductId))
            Fvr.Foreground = Brushes.Red;
        
        GenerateAttributes();
    }

    private void CategoryOnClick(object? sender, RoutedEventArgs e)
    {
        int cid = Convert.ToInt32((sender as Button)!.Tag);
        Session.Window.ContentControl.Content = new SearchPageViewModel(cid);
    }
    
    private void Next(object source, RoutedEventArgs args)
        => Slides.Next();
    

    private void Previous(object source, RoutedEventArgs args) 
        => Slides.Previous();


    private void Buy(object? sender, RoutedEventArgs e)
    {
        int elem = Convert.ToInt32((sender as Button)!.Tag);
        if (!Session.Cart.Contains(elem))
        {
            Session.Cart.Add(elem);
            (sender as Button)!.Content = "Перейти в корзину";
        }
        else
            Session.Window.ContentControl.Content = new CartPageViewModel();
    }

    private void Favorite(object? sender, RoutedEventArgs e)
    {
        int elem = Convert.ToInt32((sender as Button)!.Tag);
        if (!Session.Favorite.Contains(elem))
        {
            Session.Favorite.Add(elem);
            Fvr.Foreground = Brushes.Red;
        }
        else
        {
            Session.Favorite.Remove(elem);
            Fvr.Foreground = Brushes.DarkGray;
        }
    }

    private void GenerateAttributes()
    {
        foreach(var item in _viewModel!.Attributes)
        {
            AttributeKey.Children.Add(new TextBlock{Text = item.AtName});
            AttributeValue.Children.Add(new TextBlock{Text = item.AtValue});
        }
    }

    private void Edit_OnClick(object? sender, RoutedEventArgs e)
    {
        Session.Window.ContentControl.Content = new AdminPageViewModel(_viewModel!.Product.ProductId);
    }

    private bool _confirmDelete = false;
    private void Delete_OnClick(object? sender, RoutedEventArgs e)
    {
        if (!_confirmDelete)
        {
            (sender as Button)!.Content = "Вы точно хотите удалить?";
            _confirmDelete = true;
        }
        else
        {
            if (Session.Cart.Contains(_viewModel!.Product.ProductId))
                Session.Cart.Remove(_viewModel!.Product.ProductId);
            DbHelper.Database.Products.Remove(_viewModel!.Product);
            DbHelper.Database.SaveChanges();
            Session.Window.ContentControl.Content = new RecommendationPageViewModel();
        }
    }
}