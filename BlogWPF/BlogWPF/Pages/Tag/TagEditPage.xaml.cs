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
    /// Interaction logic for TagEditPage.xaml
    /// </summary>
    public partial class TagEditPage : Page
    {
        public TagEdit Tag { get; set; }
        public TagEditPage(int id)
        {
            InitializeComponent();
            Tag = new TagEdit();
            LoadTagAsync(id);
        }

        private async void LoadTagAsync(int id)
        {
            var item = await TagController.GetById(id);

            Tag.Id = item.Id;
            TitleTextBox.Text = item.Name;
            NameTextBox.Text = item.Name;
            UrlSlugTextBox.Text = item.UrlSlug;
            DescriptionTextBox.Text = item.Description;
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            Tag.Name = NameTextBox.Text;
            Tag.UrlSlug = UrlSlugTextBox.Text;
            Tag.Description = DescriptionTextBox.Text;

            var result = await TagController.UpdateTag(Tag);
            if (result)
            {
                MessageBox.Show("Tag updated successfully!");

                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new TagsListPage());
                }
            }
            else
            {
                MessageBox.Show("Failed update tag");
            }
        }
    }
}
