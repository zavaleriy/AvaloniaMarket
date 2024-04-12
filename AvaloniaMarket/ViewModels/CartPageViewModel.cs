using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using AvaloniaMarket.Helpers;
using AvaloniaMarket.Models;
using ReactiveUI;

namespace AvaloniaMarket.ViewModels;

public class CartPageViewModel : PageViewModelBase
{
    private ObservableCollection<ProductAmount> _renderedCart;
    
    public ObservableCollection<ProductAmount> RenderedCart
    {
        get => _renderedCart;
        set => this.RaiseAndSetIfChanged(ref _renderedCart, value);
    }
    
    public ObservableCollection<ProductAmount> SelectedProducts { get; set; }

    private string _total;

    public string? Total
    {
        get => _total;
        set => this.RaiseAndSetIfChanged(ref _total, value);

    }

    private string _productCount;

    public string ProductCount
    {
        get => _productCount;
        set => this.RaiseAndSetIfChanged(ref _productCount, value);
    }
    
    public string Promo { get; set; }
    
    public ICommand DeleteCommand { get; set; }
    
    public ICommand DeleteItemCommand { get; set; }
    
    public ICommand CheckPromoCommand { get; set; }
    
    public ICommand IncreaseCommand { get; set; }
    public ICommand DecreaseCommand { get; set; }
    
    public CartPageViewModel()
    {
        SelectedProducts = new();
        List<Product> products = DbHelper.Database.Products.ToList();
        RenderedCart = new();

        var groups = Session.Cart.GroupJoin(products, id => id, p => p.ProductId, (id, p) => new { Id = id, Product = p })
            .GroupBy(g => g.Id);
        foreach (var group in groups)
        {
            ProductAmount pa = new()
            {
                Product = products.First(p => p.ProductId == group.Key),
                Amount = group.Count()
            };
            RenderedCart.Add(pa);
        }

        CalcTotal();
        
        DeleteCommand = ReactiveCommand.Create(Remove);
        DeleteItemCommand = ReactiveCommand.Create<int>(RemoveItem);

        CheckPromoCommand = ReactiveCommand.Create(CheckPromo);

        IncreaseCommand = ReactiveCommand.Create<ProductAmount>(Increase);
        DecreaseCommand = ReactiveCommand.Create<ProductAmount>(Decrease);
    }
    
    private void Remove()
    {
        for (int i = SelectedProducts.Count - 1; i >= 0; i--)
        {
            int idx = RenderedCart.Select((p, index) => new { Product = p, Index = index })
                .FirstOrDefault(p => p.Product.Product.ProductId == SelectedProducts[i].Product.ProductId)?.Index ?? -1;
            if (idx >= 0)
            {
                int productId = SelectedProducts[i].Product.ProductId;
                int count = SelectedProducts[i].Amount;
                int cartIdx = Session.Cart.IndexOf(productId);
                if (cartIdx >= 0)
                {
                    Session.Cart.RemoveAt(cartIdx);
                    count--;
                    if (count > 0)
                    {
                        cartIdx = Session.Cart.IndexOf(productId);
                        Session.Cart.RemoveRange(cartIdx, count);
                    }
                }
                RenderedCart.RemoveAt(idx);
            }
        }
        CalcTotal();
    }

    private void RemoveItem(int product)
    {
        int idx = Session.Cart.FindIndex(c => c == product);
        Session.Cart.RemoveAll(p => p == product);
        RenderedCart.RemoveAt(idx);
        CalcTotal();
    }

    private List<PromoCode> ActivatedPromo = new();
    private void CheckPromo()
    {
        PromoCode promo = DbHelper.Database.PromoCodes.Single(p => p.Code == Promo);

        if ((bool)promo.IsActive 
            && !ActivatedPromo.Contains(promo)
            && Convert.ToDecimal(Total) > promo.Discount)
        {
            ActivatedPromo.Add(promo);
            Total = (Convert.ToDecimal(Total) - promo.Discount).ToString();
        }
    }

    private void Increase(ProductAmount pa)
    {
        if (pa.Amount + 1 > pa.Product.StockQuantity)
            return;
        pa.Amount++;
        Session.Cart.Add(pa.Product.ProductId);
        CalcTotal();
    }

    private void Decrease(ProductAmount pa)
    {
        if (pa.Amount - 1 == 0)
            return;
        pa.Amount--;
        Session.Cart.Remove(pa.Product.ProductId);
        CalcTotal();
    }

    private void CalcTotal()
    {
        Total = RenderedCart.Sum(a => a.Product.Price * a.Amount).ToString();
        ProductCount = Session.Cart.Count.ToString();
    }

}