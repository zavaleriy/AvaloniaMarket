using ReactiveUI;

namespace AvaloniaMarket.Models;

public class ProductAmount : ReactiveObject
{
    public Product Product { get; set; }

    private int _amount;

    public int Amount
    {
        get => _amount;
        set => this.RaiseAndSetIfChanged(ref _amount, value);
    }
}