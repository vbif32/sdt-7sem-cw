using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dao;
using Entities;
using Microsoft.Win32;
using WpfApp.EntitiesVM;

// ReSharper disable PossibleMultipleEnumeration


namespace WpfApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LiteDbModel _model;
        public LiteDbModel Model => _model ?? (_model = LiteDbModel.CreateModel());

        private DaoRegistry _daoRegistry;
        public DaoRegistry DaoRegistry => _daoRegistry ?? (_daoRegistry = new DaoRegistry(Model));

        private EntitiesVMRegistry _entitiesVmRegistry;
        public EntitiesVMRegistry EntitiesVmRegistry => _entitiesVmRegistry ?? (_entitiesVmRegistry = new EntitiesVMRegistry(DaoRegistry));

        private static readonly ObservableCollection<EntryVM> EntriesBySubject = new ObservableCollection<EntryVM>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SubjectsDataGrid.ItemsSource = EntitiesVmRegistry.Subjects;
                EntriesBySubjectDataGrid.ItemsSource = EntriesBySubject;
                TeacherComboBoxColumn.ItemsSource = EntitiesVmRegistry.Teachers;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка при загрузке базы данных",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void TeacherMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var editTeacherWindow = new EditTeacherWindow(this);
            try
            {
                editTeacherWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void PositionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var editPostWindow = new EditPostWindow(this);
            editPostWindow.ShowDialog();
        }

        private void ResetDistribution_Click(object sender, RoutedEventArgs e)
        {
            EntitiesVmRegistry.Entries.Clear();
            EntitiesVmRegistry.SaveChanges();
        }

        private void ResetSubjectsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EntitiesVmRegistry.Subjects.Clear();
            EntitiesVmRegistry.SaveChanges();
        }

        private void ResetSubjects()
        {
            EntitiesVmRegistry.Entries.Clear();
            EntitiesVmRegistry.Subjects.Clear();
            EntitiesVmRegistry.SaveChanges();
        }

        private void ImportNew101FormMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != true) return;

            ResetSubjects();
            var f101 = Services.Converter.F101FromExcel(openFileDialog.FileName);
            var subjects = Services.Converter.Convert(f101);
            if (!subjects.Any())
                MessageBox.Show("Не получилось извлечь предметы", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            DaoRegistry.SubjectDao.Insert(subjects);
            EntitiesVmRegistry.ResetCollections();
        }

        private void UpdateEntriesBySubject()
        {
            EntriesBySubject.Clear();
            var записи = GetEntriesBySubject((SubjectVM)SubjectsDataGrid.SelectedItem);
            foreach (var запись in записи)
                EntriesBySubject.Add(запись);
        }

        private IEnumerable<EntryVM> GetEntriesBySubject(SubjectVM subject) =>
            EntitiesVmRegistry.Entries.Where(e => e.Subject == subject);

        private void SubjectsDataGrid_OnSelected(object sender, SelectedCellsChangedEventArgs selectedCellsChangedEventArgs)
        {
            DetailSubjectDockPanel.IsEnabled = true;
            UpdateEntriesBySubject();
        }

        private void AddEntryButton_OnClick(object sender, RoutedEventArgs e)
        {
            EntriesBySubject.Add(new EntryVM { Subject = (SubjectVM)SubjectsDataGrid.SelectedItem });
        }

        private void EntriesBySubjectDataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SubjectSumTextBox.Text = EntriesBySubject.Aggregate(0f, (s, a) => s + a.Load.Amount).ToString();
        }

        private void EntriesBySubjectDataGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            EntriesBySubjectDataGrid.CurrentItem = new EntryVM {Subject = (SubjectVM) SubjectsDataGrid.SelectedItem};
        }

        private void DeleteEntityBySubjectButton_OnClick(object sender, RoutedEventArgs e)
        {
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
            var записи = EntriesBySubjectDataGrid.Items;
            return записи.Cast<EntryVM>().All(запись => запись.Teacher != null);
        }

        private void SaveEntriesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled()) return;
            EntitiesVmRegistry.SaveChanges();
            //foreach (EntryVM запись in записи)
            //    if (!entryDao.Update(запись))
            //        entryDao.Insert(запись);
        }
    }
}