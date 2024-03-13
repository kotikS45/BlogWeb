﻿using BlogWPF.Controllers;
using BlogWPF.Models.Category;
using BlogWPF.Models.Tag;
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
    /// Interaction logic for TagsListPage.xaml
    /// </summary>
    public partial class TagsListPage : Page
    {
        public TagsListPage()
        {
            InitializeComponent();
            LoadCategories();
        }

        private async void LoadCategories()
        {
            List<TagItem> tags = await TagController.GetTagsListAsync();

            TagListView.ItemsSource = tags;
        }
        private void TagListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TagListView.SelectedItem != null)
            {
                TagItem selectedTag = (TagItem)TagListView.SelectedItem;

                NavigationService navigationService = NavigationService.GetNavigationService(this);
                if (navigationService != null)
                {
                    navigationService.Navigate(new TagPage(selectedTag.Id));
                }
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService navigationService = NavigationService.GetNavigationService(this);
            if (navigationService != null)
            {
                navigationService.Navigate(new TagCreatePage());
            }
        }
    }
}
