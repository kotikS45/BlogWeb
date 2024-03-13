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

        public CategoriesListPage()
        {
            InitializeComponent();
            LoadCategories();
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

                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new CategoryPage(selectedCategory.Id));
                }
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.Navigate(new CategoryCreatePage());
            }
        }
    }
}
