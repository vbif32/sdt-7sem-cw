using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
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
                DaoRegistry.PostDao.Insert(Build());
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
    }
}