using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI;

namespace AvaloniaMarket.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand goRecomendationCommand { get; }
    public MainWindowViewModel()
    {
        _currentPage = Pages[0];
        goRecomendationCommand = ReactiveCommand.Create(goRecomendation);
    }

    private readonly PageViewModelBase[] Pages =
    {
        new RecomendationPageViewModel(),
        new ItemPageViewModel()
    };
    
    private PageViewModelBase _currentPage;
    
    public PageViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public void goRecomendation() => CurrentPage = Pages[0];

}