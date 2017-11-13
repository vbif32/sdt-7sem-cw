using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

        private static readonly ObservableCollection<Запись> _entriesBySubject = new ObservableCollection<Запись>();
        private static ObservableCollection<Запись> _entriesByTeacher = new ObservableCollection<Запись>();
        private static readonly ObservableCollection<Предмет> _subjects = new ObservableCollection<Предмет>();
        private static readonly ObservableCollection<Преподаватель> _teachers = new ObservableCollection<Преподаватель>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SubjectsDataGrid.ItemsSource = DaoRegistry.SubjectDao.FindAll();
                EntriesBySubjectDataGrid.ItemsSource = _entriesBySubject;
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
            var f101 = ImportExport.F101Import.LoadF101(openFileDialog.FileName);
            var subjects = Converting.Ф101ToПредмет.Convert(f101);
            if (!subjects.Any())
            {
                MessageBox.Show("Не получилось извлечь предметы", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            DaoRegistry.SubjectDao.Insert(subjects);
            UpdateSubjects();
        }

        private void UpdateSubjects()
        {
            _subjects.Clear();
            var subjects = GetSubjects();
            foreach (var subject in subjects)
                _subjects.Add(subject);
        }

        private void UpdateTeachers()
        {
            _teachers.Clear();
            var teachers = GetTeachers();
            foreach (var teacher in teachers)
                _teachers.Add(teacher);
        }

        private void UpdateEntriesBySubject()
        {
            _entriesBySubject.Clear();
            var записи = GetEntriesBySubject((Предмет)SubjectsDataGrid.SelectedItem);
            foreach (var запись in записи)
                _entriesBySubject.Add(запись);
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
            var newEntry = new Запись(); 
            _entriesBySubject.Add(newEntry);
        }

        private void EntriesBySubjectDataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            SubjectSumTextBox.Text = _entriesBySubject.Aggregate(0f, (s, a) => s + a.Нагрузка.Сумма).ToString();
        }

        private void EntriesBySubjectDataGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            EntriesBySubjectDataGrid.CurrentItem = new Запись {Предмет = (Предмет) SubjectsDataGrid.SelectedItem};
        }
    }
}