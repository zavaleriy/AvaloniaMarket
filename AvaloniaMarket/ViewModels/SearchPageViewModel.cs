namespace AvaloniaMarket.ViewModels;

public class SearchPageViewModel : PageViewModelBase
{

    public string Title { get; set; } = "Welcome to search";

    public SearchPageViewModel(string itemToSearch)
    {
        Title = itemToSearch;
    }



}