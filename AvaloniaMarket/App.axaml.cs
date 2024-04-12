using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaMarket.ViewModels;
using AvaloniaMarket.Views;

namespace AvaloniaMarket;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MainWindow window = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
            desktop.MainWindow = window;
            Session.Window = window;
        }

        base.OnFrameworkInitializationCompleted();
    }
}