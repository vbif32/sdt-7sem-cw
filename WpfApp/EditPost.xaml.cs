using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Entities;
using WpfApp.EntitiesVM;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditPostWindow.xaml
    /// </summary>
    public partial class EditPostWindow : Window
    {
        public EntitiesVMRegistry EntitiesVmRegistry { get; }

        public EditPostWindow(Window owner)
        {
            Owner = owner;
            EntitiesVmRegistry = ((MainWindow)Owner).EntitiesVmRegistry;
            InitializeComponent();
        }

        private void AddPostButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            EntitiesVmRegistry.Posts.Add(new PostVM());
        }

        private void RemovePostButton_Click(object sender, RoutedEventArgs e) => EntitiesVmRegistry.Posts.Remove((PostVM)PostListBox.SelectedItem);

        private void SavePostButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            if (PostListBox.SelectedItem == null)
                EntitiesVmRegistry.Posts.Add(Build());
            PostListBox.SelectedItem = null;
        }

        private PostVM Build()
        {
            return new PostVM()
            {
                FullName = FullNameTextBox.Text,
                Name = ShortNameTextBox.Text,
                Hours = Convert.ToInt32(HoursTextBox.Text),
            };
        }

        private bool IsRequiredFieldsFilled()
        {
            return FullNameTextBox.Text.Length > 3 &&
                   ShortNameTextBox.Text.Length > 0 &&
                   !Validation.GetHasError(HoursTextBox);
        }

        private void EditPostWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            PostListBox.ItemsSource = EntitiesVmRegistry.Posts;
            if (EntitiesVmRegistry.Posts.Count == 0)
                EntitiesVmRegistry.Posts.Add(new PostVM());
        }

        private void EditPostWindow_OnClosing(object sender, CancelEventArgs e)
        {
            EntitiesVmRegistry.SaveChanges();
        }
    }
}