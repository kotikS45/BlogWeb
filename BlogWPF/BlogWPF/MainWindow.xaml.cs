using BlogWPF.Pages.Auth;
using BlogWPF.Pages.Post;
using BlogWPF.Pages.Tag;
using BlogWPF.Services;
using System.Windows;

namespace BlogWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Login();
        }

        private void Login()
        {
            var token = TokenManager.Token;
            if (token == null || token.Length == 0)
            {
                NavigateToLoginPage();
            }
            else
            {
                NavigateToPostsPage();
            }
        }

        private void LoginPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToLoginPage();
        }

        private void RegistrationPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToRegistrationPage();
        }

        private void CategoriesPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToCategoriesPage();
        }

        private void TagsPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToTagsPage();
        }

        private void PostsPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPostsPage();
        }

        private void NavigateToLoginPage()
        {
            mainFrame.Navigate(new LoginPage());
        }

        private void NavigateToRegistrationPage()
        {
            mainFrame.Navigate(new RegistrationPage());
        }

        private void NavigateToCategoriesPage()
        {
            mainFrame.Navigate(new CategoriesListPage());
        }

        private void NavigateToTagsPage()
        {
            mainFrame.Navigate(new TagsListPage());
        }

        private void NavigateToPostsPage()
        {
            mainFrame.Navigate(new PostListPage());
        }
    }
}
