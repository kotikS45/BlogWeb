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
        private Frame frame;
        public CategoryItem Category { get; set; }

        public CategoryPage(Frame frame, int id = 1)
        {
            InitializeComponent();
            LoadContent(id);
            this.frame = frame;
        }

        private async void LoadContent(int id)
        {
            var item = await CategoryController.GetById(id);
            Category = item;
            DataContext = this;
            LoadPosts();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new CategoriesEditPage(frame, Category.Id));
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await CategoryController.DeleteCategory(Category.Id);

            if (result)
            {
                frame.Navigate(new CategoriesListPage(frame));
            }
        }

        private async void LoadPosts()
        {
            List<PostItem> posts = await PostController.GetPostListByCategoryAsync(Category.UrlSlug);

            PostListView.ItemsSource = posts;
        }

        private void PostListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PostListView.SelectedItem != null)
            {
                PostItem selectedPost = (PostItem)PostListView.SelectedItem;

                frame.Navigate(new PostPage(frame, selectedPost.Id));
            }
        }
    }
}
