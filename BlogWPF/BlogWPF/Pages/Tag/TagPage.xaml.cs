using BlogWPF.Controllers;
using BlogWPF.Models.Category;
using BlogWPF.Models.Tag;
using BlogWPF.Pages.Auth;
using BlogWPF.Pages.Category;
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

namespace BlogWPF.Pages.Tag
{
    /// <summary>
    /// Interaction logic for TagPage.xaml
    /// </summary>
    public partial class TagPage : Page
    {
        public TagItem TagItem { get; set; }

        public TagPage(int id = 1)
        {
            InitializeComponent();
            LoadTagAsync(id);
        }

        private async void LoadTagAsync(int id)
        {
            var item = await TagController.GetById(id);
            TagItem = item;
            DataContext = this;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            navigationService.Navigate(new TagEditPage(TagItem.Id));
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var result = await TagController.DeleteTag(TagItem.Id);

            if (result)
            {
                NavigationService navigationService = NavigationService.GetNavigationService(this);
                navigationService.Navigate(new TagsListPage());
            }
        }
    }
}
