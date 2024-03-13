using BlogWPF.Controllers;
using BlogWPF.Models.Category;
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

namespace BlogWPF.Pages.Tag
{
    /// <summary>
    /// Interaction logic for TagCreatePage.xaml
    /// </summary>
    public partial class TagCreatePage : Page
    {
        public TagCreatePage()
        {
            InitializeComponent();
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string urlSlug = UrlSlugTextBox.Text;
            string description = DescriptionTextBox.Text;

            TagCreate tag = new TagCreate
            {
                Name = name,
                UrlSlug = urlSlug,
                Description = description
            };

            var result = await TagController.CreateTag(tag);
            if (result)
            {
                MessageBox.Show("Tag created successfully!");

                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new TagsListPage());
                }
            }
            else
            {
                MessageBox.Show("Failed create tag");
            }
        }
    }
}
