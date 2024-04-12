using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AvaloniaMarket.Helpers;
using AvaloniaMarket.Models;
using ReactiveUI;

namespace AvaloniaMarket.ViewModels;

public class FavoritePageViewModel : PageViewModelBase
{

    private ObservableCollection<Product> _renderFavorite;

    public ObservableCollection<Product> RenderedFavorite
    {
        get => _renderFavorite;
        set => this.RaiseAndSetIfChanged(ref _renderFavorite, value);
    }
    
    public ICommand DeleteItemCommand { get; set; }
    
    public FavoritePageViewModel()
    {
        RenderedFavorite = new(Session.Favorite.Join(DbHelper.Database.Products, c => c, p => p.ProductId, (c, p) => p));
        DeleteItemCommand = ReactiveCommand.Create<int>(RemoveItem);
    }
    
    private void RemoveItem(int product)
    {
        int idx = Session.Favorite.FindIndex(c => c == product);
        Session.Favorite.RemoveAt(idx);
        RenderedFavorite.RemoveAt(idx);
    }
    
}