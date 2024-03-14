using BlogWPF.Controllers;
using BlogWPF.Models.Category;
using BlogWPF.Models.Post;
using BlogWPF.Models.Tag;
using BlogWPF.Pages.Auth;
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
    /// Interaction logic for PostCreatePage.xaml
    /// </summary>
    public partial class PostCreatePage : Page
    {
        private Frame frame;
        public PostCreate Post { get; set; } = new PostCreate() { Tags = new List<int>() };
        public List<CategoryItem> Categories { get; set; }
        public List<TagItem> Tags { get; set; }

        public PostCreatePage(Frame frame)
        {
            InitializeComponent();
            LoadCategories();
            LoadTags();
            DataContext = Post;
            this.frame = frame;
        }

        private async void LoadCategories()
        {
            Categories = await CategoryController.GetCategoryListAsync();
            CategoriesBox.ItemsSource = Categories;
        }

        private async void LoadTags()
        {
            Tags = await TagController.GetTagsListAsync();
            TagsBox.ItemsSource = Tags;
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await PostController.CreatePost(Post);
            if (result)
            {
                MessageBox.Show("Post created successfully!");

                frame.Navigate(new PostListPage(frame));
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                TagItem selectedTag = checkBox.DataContext as TagItem;
                Post.Tags.Add(selectedTag.Id);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                TagItem selectedTag = checkBox.DataContext as TagItem;
                Post.Tags.Remove(selectedTag.Id);
            }
        }
    }
}
