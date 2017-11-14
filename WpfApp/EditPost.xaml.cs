using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Dao;
using Entities;
using LiteDB;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditPostWindow.xaml
    /// </summary>
    public partial class EditPostWindow : Window
    {
        public DaoRegistry DaoRegistry { get; }

        public EditPostWindow(Window owner)
        {
            Owner = owner;
            DaoRegistry = ((MainWindow)Owner).DaoRegistry;
            InitializeComponent();
            UpdateSource();
        }

        private void AddPostButton_Click(object sender, RoutedEventArgs e)
        {
            if (PostListBox.SelectedItem == null)
            {
                if (!IsRequiredFieldsFilled())
                    return;
                DaoRegistry.PostDao.Insert(Build());
            }
            else
                PostListBox.SelectedItem = null;
            UpdateSource();
        }

        private void RemovePostButton_Click(object sender, RoutedEventArgs e)
        {
            DaoRegistry.PostDao.Delete(((Должность)PostListBox.SelectedItem).Id);
            UpdateSource();
        }

        private void SavePostButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            if (PostListBox.SelectedItem == null)
                DaoRegistry.PostDao.Insert(Build());
            else
                DaoRegistry.PostDao.Update((Должность)PostListBox.SelectedItem);
            UpdateSource();
        }

        private void UpdateSource()
        {
            PostListBox.ItemsSource = DaoRegistry.PostDao.FindAll();
        }

        private Должность Build()
        {
            return new Должность
            {
                Название = ShortNameTextBox.Text,
                ПолноеНазвание = FullNameTextBox.Text,
                Часы = int.Parse(HoursTextBox.Text)
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


    }
}