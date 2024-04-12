using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Avalonia.Media;
using AvaloniaMarket.Helpers;
using ReactiveUI;

namespace AvaloniaMarket.ViewModels;

public class LoginPageViewModel : PageViewModelBase
{
    public string? Login { get; set; }
    public string? Password { get; set; }

    private bool _formEnabled;
    public bool FormEnabled
    {
        get => _formEnabled;
        set
        {
            _formEnabled = value;
            this.RaisePropertyChanged();
        } 
    }
    
    public ICommand SignInCommand { get; }

    public LoginPageViewModel()
    {
        FormEnabled = true;
        SignInCommand = ReactiveCommand.Create(SignIn);
    }

    private void SignIn()
    {
        if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password))
        {
            var pwdHash = ComputeHash(Password);
            Session.User = DbHelper.Database.Users.Where(u => u.Username == Login && u.Password == pwdHash).ToList().First();
            if (Session.User.Username != "")
            {
                FormEnabled = false;
                Session.Window.LoginBlock.Text = "Профиль";
                if (Session.User.IsAdmin)
                    Session.Window.LoginBlock.Foreground = Brushes.Red;
                Session.Window.ContentControl.Content = new RecommendationPageViewModel();
            }
        }
    }

    private string ComputeHash(string input)
    {
        byte[] inpBytes = Encoding.UTF8.GetBytes(input);
        using SHA384 sha384 = SHA384.Create();
        byte[] hashBytes = sha384.ComputeHash(inpBytes);
        StringBuilder sb = new();

        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }

        return sb.ToString();
    }
    
    
}