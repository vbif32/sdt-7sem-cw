using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dao;
using Entities;
using WpfApp.EntitiesVM;

// ReSharper disable PossibleInvalidOperationException

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditTeacherWindow.xaml
    /// </summary>
    public partial class EditTeacherWindow : Window
    {
        public EntitiesVMRegistry EntitiesVmRegistry { get; }

        public EditTeacherWindow(Window owner)
        {
            Owner = owner;
            EntitiesVmRegistry = ((MainWindow)Owner).EntitiesVmRegistry;
            InitializeComponent();
            UpdateSource();
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            if (TeacherListBox.SelectedItem == null)
            {
                if (!IsRequiredFieldsFilled())
                    return;
                EntitiesVmRegistry.Teachers.Add(Build());
            }
            else
                TeacherListBox.SelectedItem = null;
            UpdateSource();
        }

        private void RemoveTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            EntitiesVmRegistry.Teachers.Remove(((TeacherVM)TeacherListBox.SelectedItem));
            UpdateSource();
        }

        private void SaveTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            if (TeacherListBox.SelectedItem == null)
                EntitiesVmRegistry.Teachers.Add(Build()); 
            UpdateSource();
        }

        private void UpdateSource()
        {
            if (TeacherListBox.SelectedItem == null)
            {
                SurnameTextBox.Text = String.Empty;
                NameTextBox.Text = String.Empty;
                MiddleNameTextBox.Text = String.Empty;
                RateTextBox.Text = String.Empty;
                PostComboBox.SelectedItem = String.Empty;
                ScientificDegreeFullTextBox.Text = String.Empty;
                ScientificDegreeShortTextBox.Text = String.Empty;
                FullTimeRadioButton.IsChecked = false;
                ExCompatibilityRadioButton.IsChecked = false;
                InCompatibilityRadioButton.IsChecked = false;
            }
            TeacherListBox.ItemsSource = EntitiesVmRegistry.Teachers;
            PostComboBox.ItemsSource = EntitiesVmRegistry.Posts;
        }

        private TeacherVM Build()
        {
            var workPlace = МестоРаботы.Основное;
            if ((bool) ExCompatibilityRadioButton.IsChecked)
                workPlace = МестоРаботы.ВнешнийСовместитель;
            if ((bool) InCompatibilityRadioButton.IsChecked)
                workPlace = МестоРаботы.ВнутреннийСовместитель;

            return new TeacherVM
            {
                Surname = SurnameTextBox.Text,
                Name = NameTextBox.Text,
                Patronymic = MiddleNameTextBox.Text,
                Rate = Convert.ToSingle(RateTextBox.Text),
                Post = (PostVM) PostComboBox.SelectedItem,
                AcademicDegreeFull = ScientificDegreeFullTextBox.Text,
                AcademicDegree = ScientificDegreeShortTextBox.Text,
                WorkPlace = workPlace
            };
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
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
            return (SurnameTextBox.Text.Length > 3) &&
                   (NameTextBox.Text.Length > 3) &&
                   (MiddleNameTextBox.Text.Length > 3) &&
                   (RateTextBox.Text.Length > 0) &&
                   (PostComboBox.SelectedItem != null) &&
                   ((bool)FullTimeRadioButton.IsChecked || (bool)InCompatibilityRadioButton.IsChecked || (bool)ExCompatibilityRadioButton.IsChecked);
        }

        private void TeacherListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TeacherListBox.SelectedItem == null)
                return;
            var должность = ((TeacherVM) TeacherListBox.SelectedItem).Post;
            foreach (var item in PostComboBox.Items)
                if (должность.Equals(item))
                    PostComboBox.SelectedItem = item;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EntitiesVmRegistry.SaveChanges();
        }
    }
}