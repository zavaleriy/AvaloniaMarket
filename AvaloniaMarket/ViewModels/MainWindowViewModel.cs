using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI;

namespace AvaloniaMarket.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    
    private PageViewModelBase _currentPage;
    
    public PageViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }
    
    public ICommand GoRecommendationCommand { get; }
    public ICommand GoCartCommand { get; }
    public ICommand GoFavoriteCommand { get; }
    
    public ICommand GoOrdersCommand { get; }
    public ICommand GoLoginCommand { get; }
    
    public string? SearchText { get; set; }
    public ICommand SearchCommand { get; }
    
    public MainWindowViewModel()
    {
        _currentPage = new RecommendationPageViewModel();
        SearchCommand = ReactiveCommand.Create(SearchItem);
        GoRecommendationCommand = ReactiveCommand.Create(GoRecommendation);
        GoCartCommand = ReactiveCommand.Create(GoCart);
        GoFavoriteCommand = ReactiveCommand.Create(GoFavorite);
        GoOrdersCommand = ReactiveCommand.Create(GoOrders);
        GoLoginCommand = ReactiveCommand.Create(GoLogin);
    }

    private void SearchItem() => CurrentPage = new SearchPageViewModel(SearchText);
    
    private void GoRecommendation() => CurrentPage = new RecommendationPageViewModel();

    private void GoCart() => CurrentPage = new CartPageViewModel();

    private void GoFavorite() => CurrentPage = new FavoritePageViewModel();

    private void GoOrders() => CurrentPage = new OrdersPageViewModel();

    private void GoLogin()
    {
        CurrentPage = Session.User == null
            ? new LoginPageViewModel()
            : new ProfilePageViewModel();
        
    }

}