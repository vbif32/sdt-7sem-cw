﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Dao;
using Entities;
using EntitiesViewModels;
using Microsoft.Win32;
using Services;
using SelectedCellsChangedEventArgs = Microsoft.Windows.Controls.SelectedCellsChangedEventArgs;

// ReSharper disable PossibleMultipleEnumeration


namespace WpfApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiteDbModel _model;
        private DaoRegistry _daoRegistry;
        private EntitiesVMRegistry _entitiesVmRegistry;


        public MainWindow()
        {
            InitializeComponent();
        }

        public LiteDbModel Model => _model ?? (_model = LiteDbModel.CreateModel());
        public DaoRegistry DaoRegistry => _daoRegistry ?? (_daoRegistry = new DaoRegistry(Model));

        public EntitiesVMRegistry EntitiesVmRegistry =>
            _entitiesVmRegistry ?? (_entitiesVmRegistry = new EntitiesVMRegistry(DaoRegistry));

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SubjectsDataGrid.ItemsSource = EntitiesVmRegistry.Subjects;
                TeacherComboBoxColumn.ItemsSource = EntitiesVmRegistry.Teachers;
                TeachersDataGrid.ItemsSource = EntitiesVmRegistry.Teachers;
                SubjectComboBoxColumn.ItemsSource = EntitiesVmRegistry.Subjects;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка при загрузке базы данных",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TeacherMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EditTeacherWindow(this).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PositionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EditPostWindow(this).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetEntries_Click(object sender, RoutedEventArgs e)
        {
            EntitiesVmRegistry.Entries.Clear();
            DaoRegistry.EntryDao.DeleteAll();
        }

        private void ImportNew101FormMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != true) return;

            ResetSubjects();
            var f101 = Converter.F101FromExcel(openFileDialog.FileName);
            var subjects = Converter.Convert(f101);
            if (!subjects.Any())
                MessageBox.Show("Не получилось извлечь предметы", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            DaoRegistry.SubjectDao.Insert(subjects);
            EntitiesVmRegistry.ResetCollections();
        }

        private void ResetSubjects()
        {
            EntitiesVmRegistry.Entries.Clear();
            DaoRegistry.EntryDao.DeleteAll();
            EntitiesVmRegistry.Subjects.Clear();
            DaoRegistry.SubjectDao.DeleteAll();
        }


        private void SubjectsDataGrid_OnSelected(object sender, SelectedCellsChangedEventArgs cellsChangedEventArgs)
        {
            DetailSubjectDockPanel.IsEnabled = true;
        }


        private EntryVM CreateNewEntry(SubjectVM subject = null, TeacherVM teacher = null)
        {
            var newEntry = new EntryVM {Subject = subject, Teacher = teacher};
            EntitiesVmRegistry.Entries.Add(newEntry);
            return newEntry;
        }

        private bool IsRequiredFieldsFilled()
        {
            var entries = EntriesBySubjectDataGrid.Items;
            return entries.OfType<EntryVM>().All(entry => entry.Teacher != null);
        }

        private void SaveEntriesBySubjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled()) return;
            foreach (var entry in ((SubjectVM)SubjectsDataGrid.SelectedItem).Entries)
                entry.Save();
            EntitiesVmRegistry.SaveEntries();
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
            {
                e.CancelCommand();
            }
        }

        private void EntriesBySubjectDataGrid_OnInitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newEntry = e.NewItem as EntryVM;
            newEntry.Subject = SubjectsDataGrid.SelectedItem as SubjectVM;
            newEntry.Teacher = EntitiesVmRegistry.Teachers.FirstOrDefault();
        }

        private void InitialRowColoring()
        {
            //var RowDataContaxt = e.Row.DataContext as SubjectVM;
            //if (RowDataContaxt != null)
            //{
            //    if (RowDataContaxt.Sales == 50)
            //        e.Row.Background = FindResource("RedBackgroundBrush") as Brush;
            //    else if (RowDataContaxt.Sales == 60)
            //        e.Row.Background = FindResource("GreenBackgroundBrush") as Brush;
            //}
        }

        private void TeachersDataGrid_OnSelectedCellsChangedted(object sender, SelectedCellsChangedEventArgs e)
        {
            DetailTeacherDockPanel.IsEnabled = true;
        }

        private void TeacherToSubjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            var commandParameter = ((Button)sender).CommandParameter;
            SubjectsTabItem.IsSelected = true;
            SubjectsDataGrid.SelectedItem = commandParameter;
        }

        private void SubjectToTeacherButton_OnClick(object sender, RoutedEventArgs e)
        {
            var commandParameter = ((Button)sender).CommandParameter;
            TeachersTabItem.IsSelected = true;
            TeachersDataGrid.SelectedItem = commandParameter;
        }

        private void SaveEntriesByTeacherButton_OnClick(object sender, RoutedEventArgs e)

        {
        }


    }
}