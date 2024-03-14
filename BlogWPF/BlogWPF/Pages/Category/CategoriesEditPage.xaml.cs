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
    /// Interaction logic for CategoriesEditPage.xaml
    /// </summary>
    public partial class CategoriesEditPage : Page
    {
        private Frame frame;
        public CategoryEdit Category { get; set; }
        public CategoriesEditPage(Frame frame, int id)
        {
            InitializeComponent();
            Category = new CategoryEdit();
            LoadCategoryAsync(id);
            this.frame = frame;
        }

        private async void LoadCategoryAsync(int id)
        {
            var item = await CategoryController.GetById(id);

            Category.Id = item.Id;
            TitleTextBox.Text = item.Name;
            NameTextBox.Text = item.Name;
            UrlSlugTextBox.Text = item.UrlSlug;
            DescriptionTextBox.Text = item.Description;
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Category.Name = NameTextBox.Text;
            Category.UrlSlug = UrlSlugTextBox.Text;
            Category.Description = DescriptionTextBox.Text;

            var result = await CategoryController.UpdateCategory(Category);
            if (result)
            {
                frame.Navigate(new CategoriesListPage(frame));
            }
        }
    }
}
