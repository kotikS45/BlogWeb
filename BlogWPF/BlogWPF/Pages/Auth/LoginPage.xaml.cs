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
        private Frame frame;
        private Action action;
        public LoginPage(Frame frame, Action action)
        {
            InitializeComponent();
            UsernameTextBox.Text = "admin";
            PasswordBox.Password = "admin";
            this.action = action;
            this.frame = frame;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginViewModel = new AccountLogin { UserNameOrEmail = UsernameTextBox.Text, Password = PasswordBox.Password };
            await AccountController.LoginAsync(loginViewModel);
            ToPosts();
            action();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new RegistrationPage(frame, action));
        }

        private void ToPosts()
        {
            frame.Navigate(new PostListPage(frame));
        }
    }
}
