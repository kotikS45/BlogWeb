using BlogWPF.Data.Entities.Identity;
using BlogWPF.Data;
using BlogWPF.Models.Account;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BlogWPF.Controllers;
using BlogWPF.Pages.Category;
using System.Windows.Navigation;
using BlogWPF.Pages.Post;

namespace BlogWPF.Pages.Auth
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            UsernameTextBox.Text = "admin";
            PasswordBox.Password = "admin";
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginViewModel = new AccountLogin { UserNameOrEmail = UsernameTextBox.Text, Password = PasswordBox.Password };
            await AccountController.LoginAsync(loginViewModel);
            ToPosts();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.Navigate(new RegistrationPage());
            }
        }

        private void ToPosts()
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.Navigate(new PostListPage());
            }
        }
    }
}
