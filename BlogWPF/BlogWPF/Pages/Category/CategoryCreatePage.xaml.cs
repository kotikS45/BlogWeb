using BlogWPF.Controllers;
using BlogWPF.Models.Category;
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

namespace BlogWPF.Pages.Category
{
    /// <summary>
    /// Interaction logic for CategoryCreatePage.xaml
    /// </summary>
    public partial class CategoryCreatePage : Page
    {
        private Frame frame;

        public CategoryCreatePage(Frame frame)
        {
            InitializeComponent();
            this.frame = frame;
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string urlSlug = UrlSlugTextBox.Text;
            string description = DescriptionTextBox.Text;

            CategoryCreate category = new CategoryCreate
            {
                Name = name,
                UrlSlug = urlSlug,
                Description = description
            };

            var result = await CategoryController.CreateCategory(category);
            if (result)
            {
                frame.Navigate(new CategoriesListPage(frame));
            }
        }
    }
}
