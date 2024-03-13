using BlogWPF.Controllers;
using BlogWPF.Models.Category;
using BlogWPF.Models.Post;
using BlogWPF.Pages.Auth;
using BlogWPF.Pages.Post;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace BlogWPF.Pages.Category
{
    /// <summary>
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        public CategoryItem Category { get; set; }

        public CategoryPage(int id = 1)
        {
            InitializeComponent();
            LoadCategoryAsync(id);
            LoadPosts();
        }

        private async void LoadCategoryAsync(int id)
        {
            var item = await CategoryController.GetById(id);
            Category = item;
            DataContext = this;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new CategoriesEditPage(Category.Id));
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await CategoryController.DeleteCategory(Category.Id);

            if (result)
            {
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                navigationService.Navigate(new CategoriesListPage());
            }
        }

        private async void LoadPosts()
        {
            List<PostItem> posts = await PostController.GetPostListAsync();

            PostListView.ItemsSource = posts;
        }

        private void PostListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostListView.SelectedItem != null)
            {
                PostItem selectedPost = (PostItem)PostListView.SelectedItem;

                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new PostPage(selectedPost.Id));
                }
            }
        }
    }
}
