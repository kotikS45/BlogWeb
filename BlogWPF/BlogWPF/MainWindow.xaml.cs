using BlogWPF.Controllers;
using BlogWPF.Data.Entities.Identity;
using BlogWPF.Pages.Auth;
using BlogWPF.Pages.Post;
using BlogWPF.Pages.Tag;
using BlogWPF.Services;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace BlogWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserEntity User { get; set; }
        private UIElement Profile { get; set; }
        private UIElement Menu { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Login();
        }

        private async void Login()
        {
            var token = TokenManager.Token;
            if (token == null || token.Length == 0)
            {
                NavigateToLoginPage();
            }
            else
            {
                User = await AccountController.GetUserByTokenAsync(Encoding.UTF8.GetString(token));
                Profile = GetProfile();
                MainGrid.Children.Add(Profile);
                Menu = GetMenu();
                MainGrid.Children.Add(Menu);
                NavigateToPostsPage();
            }
        }

        private UIElement GetProfile()
        {
            var img = "iVBORw0KGgoAAAANSUhEUgAAADIAAAAyCAMAAAAp4XiDAAAAnFBMVEVHcEwjHyAjHyAiHh8fGxwjHyAjHyAjHyAjHyAhHR4jHyAiHh8jHyAjHyAgHB0jHyAjHyAfGxwiHh8jHyAjHyAjHyD///8gHB0xLS7X1taCgID5+fmSkJA3MzTn5+f8/Pzw8PBgXV2/vr5OS0ylo6RraGknIySysbHf3t8+Ozz19fUtKSrNzMx6eHhGQ0Ryb3CIhoedm5tVUlLGxcV60hssAAAAFXRSTlMAhBbU+QmTIzHeV8ttoek8d/K6RGGwkko3AAAC9ElEQVRIx41W15aCMBQUQRGxYEkuXem9qP//bxtA3SSgu3OOT2a4bW6ZzcZYyPv1RtM26728mP0D85NwVA4SEEgH5Sic5n8QZE09QFF7TWvbbePVBRxUTf5GEBTQvVvpWriH5ZY3TwdF+ERanEVwzDLHDPLSdEBcTwYlb6WiMvAEjKqQlhMhrVSo/QuexCWp4Thybi+iKMUfkUZIXXE2RFRdqSeWaxguHdW14jiyiirqgRt6ma5nXuhSaajQkYpnsYWIsuEHCAB1v8Cn7ESw/c2bBgH1vUQn7weAk1DxBNL5HYiiU197OG9Gxykp67ryStsWTMpnj2IQjvcb48WE5TO/u4xKrx8jBgXlQJrthqwtwaayaQJLoT3ALQh9ghWHkonl8RTPoqTjiF2i1xBROsnvPOVOFewSSesueBTi/1JwiEgC5irtF740PKVhRK2r89lqF9DiIt/hQPuArwHJ2VqKGMmnGVuX2mXaoAtmw2SRwGZsxDf2XxM2M4GpyiA/ilJxfW2TyiyB+w52m/hJgti8cn/eiGbGFJwn96KTfuElFp6gjBzrnStD2w75YfNyTOPD/w4TtFGSOxNJaLet2dqhb3Cx9EnmSmk9zFrvmrhHrAfmw+JLORd1SjBGUwBbSigagxMMI8skAzQCZAkry9kZIms8KBiOnrxDgTXTYr4zyeiGxuPVYsPAWEI7VD34wOiaxn1WZZgX+8MwLmz0BTYzLhbDUEoz+MwYeuA9lEhputEXwjcrKOlH34oZsA/nm5XMILFKm98xfoQmx/5nzyDzuTE+k0Vk5vhRf0py8MC5iVRmke2VmHDSKJ6qftyk3UoSuTV2UlDj4jysgR9K8Z00Gll84n60LFXoFlBq1/GLRaSMHC+54osfwHE1ucR1k0jHTarAKeK40LN7FRrWsMS3k3fJYqNA1pbkzTUtfb8c9qvVnwrnT/ePvNwNB8mzTy/XPw6SXgiCKCFy9lTd2VN5pEH/OHvY40r633H1POFO3064HzhXtHiLepxmAAAAAElFTkSuQmCC";

            Border border = new Border();
            border.Name = "ProfileBox";
            border.CornerRadius = new CornerRadius(0, 20, 20, 0);
            border.Margin = new Thickness(10, 5, 0, 0);
            border.Height = 40;
            border.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF512DA8"));

            WrapPanel panel = new WrapPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Right;

            Image image = new Image();
            image.Width = 40;
            image.Height = 40;
            image.Margin = new Thickness(5, 0, 0, 0);
            byte[] imageBytes = Convert.FromBase64String(img);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            if (User.Image == null)
            {
                bitmapImage.StreamSource = new MemoryStream(imageBytes);
                bitmapImage.EndInit();
            }
            else
            {
                bitmapImage.StreamSource = new MemoryStream(Convert.FromBase64String(User.Image));
                bitmapImage.EndInit();
            }
            image.Source = bitmapImage;

            TextBlock textBlock = new TextBlock();
            textBlock.Text = User.Username;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.Foreground = Brushes.White;
            textBlock.FontSize = 18;
            textBlock.FontWeight = FontWeights.Bold;

            Button button = new Button();
            button.Content = "Log Out";
            button.Click += LogOutButton_Click;
            button.VerticalAlignment = VerticalAlignment.Center;

            panel.Children.Add(button);
            panel.Children.Add(textBlock);
            panel.Children.Add(image);

            border.Child = panel;

            return border;
        }

        public UIElement GetMenu()
        {
            WrapPanel wrapPanel = new WrapPanel();
            wrapPanel.HorizontalAlignment = HorizontalAlignment.Center;

            Button categoriesButton = new Button();
            categoriesButton.Width = 100;
            categoriesButton.VerticalAlignment = VerticalAlignment.Top;
            categoriesButton.Content = "Categories";
            categoriesButton.Click += CategoriesPageButton_Click;

            Button postsButton = new Button();
            postsButton.Margin = new Thickness(3, 0, 3, 0);
            postsButton.Width = 100;
            postsButton.Content = "Posts";
            postsButton.Click += PostsPageButton_Click;

            Button tagsButton = new Button();
            tagsButton.Width = 100;
            tagsButton.VerticalAlignment = VerticalAlignment.Top;
            tagsButton.Content = "Tags";
            tagsButton.Click += TagsPageButton_Click;

            wrapPanel.Children.Add(categoriesButton);
            wrapPanel.Children.Add(postsButton);
            wrapPanel.Children.Add(tagsButton);

            Grid.SetColumn(wrapPanel, 1);

            return wrapPanel;
        }

        private void LoginPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToLoginPage();
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            TokenManager.Token = null;
            MainGrid.Children.Remove(Profile);
            MainGrid.Children.Remove(Menu);
            Login();
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
            mainFrame.Navigate(new LoginPage(mainFrame, Login));
        }

        private void NavigateToRegistrationPage()
        {
            mainFrame.Navigate(new RegistrationPage(mainFrame, Login));
        }

        private void NavigateToCategoriesPage()
        {
            mainFrame.Navigate(new CategoriesListPage(mainFrame));
        }

        private void NavigateToTagsPage()
        {
            mainFrame.Navigate(new TagsListPage(mainFrame));
        }

        private void NavigateToPostsPage()
        {
            mainFrame.Navigate(new PostListPage(mainFrame));
        }
    }
}
