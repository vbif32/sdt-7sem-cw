using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Dao;
using Entities;
using LiteDB;
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
            PostListBox.ItemsSource = EntitiesVmRegistry.Posts;
        }

        private void AddPostButton_Click(object sender, RoutedEventArgs e)
        {
            if (PostListBox.SelectedItem == null)
            {
                if (!IsRequiredFieldsFilled())
                    return;
                EntitiesVmRegistry.Posts.Add(NewPost());
            }
            else
                PostListBox.SelectedItem = NewPost();
        }

        private void RemovePostButton_Click(object sender, RoutedEventArgs e) => EntitiesVmRegistry.Posts.Remove((PostVM)PostListBox.SelectedItem);

        private void SavePostButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            if (PostListBox.SelectedItem == null)
                EntitiesVmRegistry.Posts.Add(NewPost());
        }

        private PostVM NewPost()
        {
            return new PostVM
            {
                Name = ShortNameTextBox.Text,
                FullName = FullNameTextBox.Text,
                Hours = int.Parse(HoursTextBox.Text)
            };
        }
        private bool IsRequiredFieldsFilled()
        {
            return (FullNameTextBox.Text.Length > 3) &&
                   (ShortNameTextBox.Text.Length > 0) &&
                   (HoursTextBox.Text.Length > 0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //EntitiesVmRegistry.SaveChanges();
        }
    }
}