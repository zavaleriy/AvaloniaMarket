using System.Collections.Generic;
using System.Linq;
using AvaloniaMarket.Models;
using AvaloniaMarket.Views;

namespace AvaloniaMarket;

public class Session
{
    public static List<int> Cart { get; set; } = new();
    public static List<int> Favorite { get; set; } = new();
    
    public static User? User;

    public static MainWindow Window;

}