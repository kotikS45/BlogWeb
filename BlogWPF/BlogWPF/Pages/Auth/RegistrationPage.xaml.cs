using BlogWPF.Data.Entities.Identity;
using BlogWPF.Data;
using BlogWPF.Models.Account;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlogWPF.Controllers;
using BlogWPF.Pages.Post;

namespace BlogWPF.Pages.Auth
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private Frame frame;
        private Action action;

        public RegistrationPage(Frame frame, Action action)
        {
            InitializeComponent();
            this.action = action;
            this.frame = frame;
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var registerModel = new AccountRegister { Username = UsernameTextBox.Text, Email = EmailTextBox.Text, Password = PasswordBox.Password };
            await AccountController.RegistrationAsync(registerModel);
            ToPosts();
            action();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new LoginPage(frame, action));
        }

        private void ToPosts()
        {
            frame.Navigate(new PostListPage(frame));
        }
    }
}
