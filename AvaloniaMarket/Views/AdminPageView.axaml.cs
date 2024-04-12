using System;
using System.Collections.Generic;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaMarket.Models;
using AvaloniaMarket.ViewModels;

namespace AvaloniaMarket.Views;

public partial class AdminPageView : UserControl
{
    private AdminPageViewModel? _viewModel;
    
    public AdminPageView()
    {
        InitializeComponent();
        
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _viewModel = DataContext as AdminPageViewModel;
    }
}