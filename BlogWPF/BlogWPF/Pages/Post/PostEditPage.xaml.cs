using BlogWPF.Controllers;
using BlogWPF.Models.Category;
using BlogWPF.Models.Post;
using BlogWPF.Models.Tag;
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
    /// Interaction logic for PostEditPage.xaml
    /// </summary>
    public partial class PostEditPage : Page
    {
        private Frame frame;
        private PostItem postItem { get; set; }
        public PostEdit Post { get; set; } = new PostEdit();
        public List<CategoryItem> Categories { get; set; }
        public List<TagItem> Tags { get; set; }

        public PostEditPage(Frame frame, int id)
        {
            InitializeComponent();
            LoadPost(id);
            DataContext = Post;
            this.frame = frame;
        }

        private async void LoadPost(int id)
        {
            postItem = await PostController.GetById(id);

            TitleTextBox.Text = postItem.Title;
            DescriptionTextBox.Text = postItem.Description;
            ShortDescriptionTextBox.Text = postItem.ShortDescription;
            MetaTextBox.Text = postItem.Meta;
            PublishedBox.IsChecked = postItem.Published;
            UrlSlugTextBox.Text = postItem.UrlSlug;
            Post.Tags = new List<int>();
            foreach (var tag in postItem.Tags)
            {
                Post.Tags.Add(tag.Id);
            }

            LoadCategories();
            LoadTags();
        }

        private async void LoadCategories()
        {
            Categories = await CategoryController.GetCategoryListAsync();
            CategoriesBox.ItemsSource = Categories;
            if (Categories != null && postItem != null)
            {
                CategoryItem selectedCategory = Categories.FirstOrDefault(c => c.Id == postItem.Category.Id);
                if (selectedCategory != null)
                {
                    CategoriesBox.SelectedItem = selectedCategory;
                }
            }
        }

        private async void LoadTags()
        {
            Tags = await TagController.GetTagsListAsync();
            foreach (var tag in Tags)
            {
                var item = postItem.Tags.FirstOrDefault(x => x.Id == tag.Id);

                CheckBox checkBox = new CheckBox();
                checkBox.Content = tag.Name;
                checkBox.Tag = tag;
                checkBox.IsChecked = item != null;
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;

                TagsBox.Items.Add(checkBox);
            }
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Post.Published = PublishedBox.IsChecked == true;
            Post.Title = TitleTextBox.Text;
            Post.Description = DescriptionTextBox.Text;
            Post.ShortDescription = ShortDescriptionTextBox.Text;
            Post.Meta = MetaTextBox.Text;
            Post.UrlSlug = UrlSlugTextBox.Text;
            Post.Id = postItem.Id;
            Post.CategoryId = (CategoriesBox.SelectedItem as CategoryItem).Id;

            var result = await PostController.UpdatePost(Post);
            if (result)
            {
                MessageBox.Show("Post updated successfully!");

                frame.Navigate(new PostListPage(frame));
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var selectedTag = checkBox.Tag as TagItem;
                Post.Tags.Add(selectedTag.Id);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox != null)
            {
                var selectedTag = checkBox.Tag as TagItem;
                Post.Tags.Remove(selectedTag.Id);
            }
        }
    }
}
