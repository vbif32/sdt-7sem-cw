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
// ReSharper disable PossibleMultipleEnumeration


namespace WpfApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DaoRegistry _daoRegistry;
        public DaoRegistry DaoRegistry => _daoRegistry ?? (_daoRegistry = new DaoRegistry(Model));

        private LiteDbModel _model;
        public LiteDbModel Model => _model ?? (_model = LiteDbModel.CreateModel());

        private static readonly ObservableCollection<Запись> EntriesBySubject = new ObservableCollection<Запись>();
        private static ObservableCollection<Запись> _entriesByTeacher = new ObservableCollection<Запись>();
        private static readonly ObservableCollection<Предмет> Subjects = new ObservableCollection<Предмет>();
        private static readonly ObservableCollection<Преподаватель> Teachers = new ObservableCollection<Преподаватель>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SubjectsDataGrid.ItemsSource = Subjects;
                EntriesBySubjectDataGrid.ItemsSource = EntriesBySubject;
                TeacherComboBoxColumn.ItemsSource = DaoRegistry.TeacherDao.FindAll();

                UpdateSubjects();
                UpdateTeachers();
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
            editTeacherWindow.ShowDialog();
            UpdateTeachers();
        }

        private void PositionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var editPostWindow = new EditPostWindow(this);
            editPostWindow.ShowDialog();
        }

        private void ResetDistribution_Click(object sender, RoutedEventArgs e)
        {
            DaoRegistry.EntryDao.DeleteAll();
        }

        private void ResetSubjects_Click(object sender, RoutedEventArgs e)
        {
            DaoRegistry.EntryDao.DeleteAll();
            DaoRegistry.SubjectDao.DeleteAll();
            UpdateSubjects();
        }

        private void ImportNew101FormMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != true) return;

            DaoRegistry.SubjectDao.DeleteAll();
            DaoRegistry.EntryDao.DeleteAll();
            var f101 = ImportExport.F101Import.LoadF101(openFileDialog.FileName);
            var subjects = Converting.Ф101ToПредмет.Convert(f101);
            if (!subjects.Any())
                MessageBox.Show("Не получилось извлечь предметы", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            DaoRegistry.SubjectDao.Insert(subjects);
            UpdateSubjects();
        }

        private void UpdateSubjects()
        {
            Subjects.Clear();
            var subjects = GetSubjects();
            foreach (var subject in subjects)
                Subjects.Add(subject);
        }

        private void UpdateTeachers()
        {
            Teachers.Clear();
            var teachers = GetTeachers();
            foreach (var teacher in teachers)
                Teachers.Add(teacher);
        }

        private void UpdateEntriesBySubject()
        {
            EntriesBySubject.Clear();
            var записи = GetEntriesBySubject((Предмет)SubjectsDataGrid.SelectedItem);
            foreach (var запись in записи)
                EntriesBySubject.Add(запись);
        }

        private IEnumerable<Предмет> GetSubjects() => DaoRegistry.SubjectDao.FindAll();
        private IEnumerable<Преподаватель> GetTeachers() => DaoRegistry.TeacherDao.FindAll();
        private IEnumerable<Запись> GetEntriesBySubject(Предмет предмет) => DaoRegistry.EntryDao.Find(x => x.Предмет == предмет);

        private void SubjectsDataGrid_OnSelected(object sender, SelectedCellsChangedEventArgs selectedCellsChangedEventArgs)
        {
            DetailSubjectDockPanel.IsEnabled = true;
            UpdateEntriesBySubject();
        }

        private void AddEntryButton_OnClick(object sender, RoutedEventArgs e)
        {
            EntriesBySubject.Add(new Запись { Предмет = (Предмет)SubjectsDataGrid.SelectedItem });
        }

        private void EntriesBySubjectDataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SubjectSumTextBox.Text = EntriesBySubject.Aggregate(0f, (s, a) => s + a.Нагрузка.Сумма).ToString();
        }

        private void EntriesBySubjectDataGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            EntriesBySubjectDataGrid.CurrentItem = new Запись {Предмет = (Предмет) SubjectsDataGrid.SelectedItem};
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
            return записи.Cast<Запись>().All(запись => запись.Преподаватель != null);
        }

        private void SaveEntriesButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled()) return;
            var записи = EntriesBySubjectDataGrid.Items;
            var entryDao = DaoRegistry.EntryDao;
            foreach (Запись запись in записи)
                if (!entryDao.Update(запись))
                    entryDao.Insert(запись);
        }
    }
}