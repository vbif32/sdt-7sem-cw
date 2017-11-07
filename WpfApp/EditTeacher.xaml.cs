using System;
using System.Windows;
using System.Windows.Controls;
using Dao;
using Entities;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditTeacherWindow.xaml
    /// </summary>
    public partial class EditTeacherWindow : Window
    {
        public DaoRegistry DaoRegistry { get; }

        public EditTeacherWindow(Window owner)
        {
            Owner = owner;
            DaoRegistry = ((MainWindow)Owner).DaoRegistry;
            InitializeComponent();
            UpdateSource();
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeacherListBox.SelectedItem == null)
                DaoRegistry.TeacherDao.Insert(Build());
            else
                TeacherListBox.SelectedItem = null;
            UpdateSource();
        }

        private void RemoveTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            DaoRegistry.TeacherDao.Delete(((Преподаватель)TeacherListBox.SelectedItem).Id);
            UpdateSource();
        }

        private void SaveTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeacherListBox.SelectedItem == null)
                DaoRegistry.TeacherDao.Insert(Build());
            else
                DaoRegistry.TeacherDao.Update((Преподаватель)TeacherListBox.SelectedItem);
            UpdateSource();
        }

        private void UpdateSource()
        {
            TeacherListBox.ItemsSource = DaoRegistry.TeacherDao.FindAll();
            PositionComboBox.ItemsSource = DaoRegistry.PostDao.FindAll();
        }

        private Преподаватель Build()
        {
            var место = МестоРаботы.Основное;
            if ((bool) ExCompatibilityRadioButton.IsChecked)
                место = МестоРаботы.ВнешнийСовместитель;
            if ((bool) InCompatibilityRadioButton.IsChecked)
                место = МестоРаботы.ВнутреннийСовместитель;

            return new Преподаватель
            {
                Фамилия = SurnameTextBox.Text,
                Имя = NameTextBox.Text,
                Отчество = MiddleNameTextBox.Text,
                Ставка = Convert.ToSingle(RateTextBox.Text),
                Должность = (Должность) PositionComboBox.SelectedItem,
                УченаяСтепень = SurnameTextBox.Text,
                УченаяСтепеньПолная = SurnameTextBox.Text,
                МестоРаботы = место
            };
        }
    }
}