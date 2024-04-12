using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using AvaloniaMarket.Helpers;
using AvaloniaMarket.ViewModels;

namespace AvaloniaMarket.Views;

public partial class SearchPageView : UserControl
{
    private SearchPageViewModel? _viewModel;
    public SearchPageView()
    {
        InitializeComponent();
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _viewModel = DataContext as SearchPageViewModel;

        if (_viewModel!.Products.Count == 1)
            Session.Window.ContentControl.Content = new ItemPageViewModel(_viewModel.Products[0].ProductId);
        else
            GenerateItems();
    }

    private void GenerateItems()
    {
        foreach (var product in _viewModel!.Products)
        {
            string? imageUrl = _viewModel.GetFirstImageUrl(product.ProductId);
            if (imageUrl is null) imageUrl = "/Assets/placeholder.png";
            var itemImage = new Image
            {
                Classes = { "item-image" },
                Source = ImageHelper.LoadFromResource(new Uri(String.Concat("avares://AvaloniaMarket", imageUrl)))
            };

            var itemName = new Button
            {
                Classes = { "item-name" },
                Content = product.ProductName,
                Tag = product.ProductId
            };
            itemName.Click += ItemOnClick;

            var itemPrice = new TextBlock
            {
                Text = string.Concat(product.Price, " Р")
            };

            var pathIcon = new PathIcon {Foreground = Brushes.DarkGray};
            if (Session.Favorite.Contains(product.ProductId))
                pathIcon.Foreground = Brushes.Red;
            
            var btnFvr = new Button
            {
                Classes = { "btn-favorite" },
                Content = pathIcon,
                Tag = product.ProductId
            };
            btnFvr.Click += BtnFvrOnClick;

            var btnBuy = new Button()
            {
                Content = "Купить",
                Tag = product.ProductId
            };
            btnBuy.Click += BtnBuyOnClick;
            if (Session.Cart.Contains(product.ProductId))
                btnBuy.Content = "В корзине";
            
            var extraMenu = new StackPanel()
            {
                Classes = { "extra-menu" },
                Children =
                {
                    btnFvr,
                    btnBuy
                }
            };
            
            var rows = new RowDefinitions("2*, 2*");
            var extraGrid = new Grid()
            {
                RowDefinitions = rows,
                Margin = new(0, 8, 10, 5),
                Children =
                {
                    itemPrice,
                    extraMenu
                }
            };
            
            var cols = new ColumnDefinitions("Auto, 2*, Auto");
            var itemGrid = new Grid()
            {
                ColumnDefinitions = cols,
                Children =
                {
                    itemImage,
                    itemName,
                    extraGrid
                }
            };
            
            Grid.SetColumn(itemImage, 0);
            Grid.SetColumn(itemName, 1);
            Grid.SetColumn(itemPrice, 2);
            Grid.SetColumn(extraGrid, 2);
            Grid.SetRow(itemPrice, 0);
            Grid.SetRow(extraMenu, 1);
        
            var itemBorder = new Border()
            {
                Child = itemGrid,
                Classes = { "item-border" }
            };
        
            ProductsSp.Children.Add(itemBorder);
        }
    }

    private void ItemOnClick(object? sender, RoutedEventArgs e)
    {
        int prid = Convert.ToInt32((sender as Button)!.Tag);
        Session.Window.ContentControl.Content = new ItemPageViewModel(prid);
    }

    private void BtnFvrOnClick(object? sender, RoutedEventArgs e)
    {
        int elem = Convert.ToInt32((sender as Button)!.Tag);
        if (!Session.Favorite.Contains(elem))
        {
            Session.Favorite.Add(elem);
            ((sender as Button)!.Content as PathIcon)!.Foreground = Brushes.Red;
        }
        else
        {
            Session.Favorite.Remove(elem);
            ((sender as Button)!.Content as PathIcon)!.Foreground = Brushes.DarkGray;
        }
    }

    private void BtnBuyOnClick(object? sender, RoutedEventArgs e)
    {
        int elem = Convert.ToInt32((sender as Button)!.Tag);
        if (!Session.Cart.Contains(elem))
        {
            Session.Cart.Add(elem);
            (sender as Button)!.Content = "В корзине";
        }
        else
            Session.Window.ContentControl.Content = new CartPageViewModel();
    }
    
}