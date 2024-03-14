using BlogWPF.Controllers;
using BlogWPF.Models.Post;
using BlogWPF.Models.Tag;
using BlogWPF.Pages.Category;
using BlogWPF.Pages.Tag;
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

namespace BlogWPF.Pages.Post
{
    /// <summary>
    /// Interaction logic for PostListPage.xaml
    /// </summary>
    public partial class PostListPage : Page
    {
        private Frame frame;

        public PostListPage(Frame frame)
        {
            InitializeComponent();
            LoadPosts();
            this.frame = frame;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            frame.Navigate(new PostCreatePage(frame));
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

                frame.Navigate(new PostPage(frame, selectedPost.Id));
            }
        }
    }
}
