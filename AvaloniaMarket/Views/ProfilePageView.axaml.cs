using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaMarket.ViewModels;

namespace AvaloniaMarket.Views;

public partial class ProfilePageView : UserControl
{
    public ProfilePageView()
    {
        InitializeComponent();
    }

    private void Create_OnClick(object? sender, RoutedEventArgs e)
    {
        Session.Window.ContentControl.Content = new AdminPageViewModel(true);
    }
}