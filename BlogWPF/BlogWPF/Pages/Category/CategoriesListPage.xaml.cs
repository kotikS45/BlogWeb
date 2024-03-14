using BlogWPF.Controllers;
using BlogWPF.Models.Category;
using BlogWPF.Pages.Category;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BlogWPF.Pages.Auth
{
    public partial class CategoriesListPage : Page
    {
        private Frame frame;

        public CategoriesListPage(Frame frame)
        {
            InitializeComponent();
            LoadCategories();
            this.frame = frame;
        }

        private async void LoadCategories()
        {
            List<CategoryItem> categories = await CategoryController.GetCategoryListAsync();

            CategoryListView.ItemsSource = categories;
        }
        private void CategoryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListView.SelectedItem != null)
            {
                CategoryItem selectedCategory = (CategoryItem)CategoryListView.SelectedItem;

                frame.Navigate(new CategoryPage(frame, selectedCategory.Id));
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            frame.Navigate(new CategoryCreatePage(frame));
        }
    }
}
