using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaMarket.ViewModels;

namespace AvaloniaMarket.Views;

public partial class CartPageView : UserControl
{
    private CartPageViewModel? _viewModel;
    public CartPageView()
    {
        InitializeComponent();
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _viewModel = DataContext as CartPageViewModel;
    }
}