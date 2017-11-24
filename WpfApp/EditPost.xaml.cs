using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
                EntitiesVmRegistry.Posts.Add(Build());
            }
            else
                PostListBox.SelectedItem = null;
        }

        private void RemovePostButton_Click(object sender, RoutedEventArgs e)
        {
            EntitiesVmRegistry.Posts.Remove((PostVM)PostListBox.SelectedItem);
        }

        private void SavePostButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            if (PostListBox.SelectedItem == null)
                EntitiesVmRegistry.Posts.Add(Build());
        }

        private PostVM Build()
        {
            return new PostVM
            {
                Name = ShortNameTextBox.Text,
                FullName = FullNameTextBox.Text,
                Hours = int.Parse(HoursTextBox.Text)
            };
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex("[^0-9]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        private bool IsRequiredFieldsFilled()
        {
            return (FullNameTextBox.Text.Length > 3) &&
                   (ShortNameTextBox.Text.Length > 0) &&
                   (HoursTextBox.Text.Length > 0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EntitiesVmRegistry.SaveChanges();
        }
    }
}