using AvaloniaMarket.Models;

namespace AvaloniaMarket.ViewModels;

public class ProfilePageViewModel : PageViewModelBase
{
    public string Name { get; set; } = Session.User!.Username;
    public bool AdminMenu { get; set; } = Session.User.IsAdmin;

    public ProfilePageViewModel()
    {
        Session.User ??= new User { Username = "admin", IsAdmin = true, UserId = 1 };
    }
    
}