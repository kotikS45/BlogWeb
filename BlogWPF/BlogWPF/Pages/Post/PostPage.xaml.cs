using BlogWPF.Controllers;
using BlogWPF.Models.Post;
using BlogWPF.Models.Tag;
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
    /// Interaction logic for PostPage.xaml
    /// </summary>
    public partial class PostPage : Page
    {
        private Frame frame;
        public PostItem PostItem { get; set; }

        public PostPage(Frame frame, int id = 1)
        {
            InitializeComponent();
            LoadPostAsync(id);
            this.frame = frame;
        }

        private async void LoadPostAsync(int id)
        {
            var item = await PostController.GetById(id);
            PostItem = item;
            DataContext = this;
            LoadTags();
        }

        private void LoadTags()
        {
            var box = this.TagsBox;

            for (int i = 0; i < PostItem.Tags.Count; i++)
            {
                var button = new Button();
                button.Content = PostItem.Tags[i].Name;
                button.Margin = new Thickness(0, 0, 1, 0);

                ControlTemplate template = new ControlTemplate(typeof(Button));

                FrameworkElementFactory borderFactory = new FrameworkElementFactory(typeof(Border));

                borderFactory.SetValue(Border.BackgroundProperty, new TemplateBindingExtension(Button.BackgroundProperty));
                borderFactory.SetValue(Border.BorderBrushProperty, new TemplateBindingExtension(Button.BorderBrushProperty));
                borderFactory.SetValue(Border.BorderThicknessProperty, new TemplateBindingExtension(Button.BorderThicknessProperty));

                var radius = new CornerRadius();
                if (i == 0)
                {
                    radius.BottomLeft = 10;
                }
                if (i == PostItem.Tags.Count - 1)
                {
                    radius.TopRight = 10;
                }
                borderFactory.SetValue(Border.CornerRadiusProperty, radius);
                borderFactory.SetValue(Border.PaddingProperty, new TemplateBindingExtension(Button.PaddingProperty));

                FrameworkElementFactory contentPresenterFactory = new FrameworkElementFactory(typeof(ContentPresenter));
                contentPresenterFactory.SetValue(ContentPresenter.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                contentPresenterFactory.SetValue(ContentPresenter.VerticalAlignmentProperty, VerticalAlignment.Center);

                borderFactory.AppendChild(contentPresenterFactory);

                template.VisualTree = borderFactory;

                button.Template = template;

                box.Children.Add(button);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            frame.Navigate(new PostEditPage(frame, PostItem.Id));
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await PostController.DeletePost(PostItem.Id);

            if (result)
            {
                frame.Navigate(new PostListPage(frame));
            }
        }
    }
}
