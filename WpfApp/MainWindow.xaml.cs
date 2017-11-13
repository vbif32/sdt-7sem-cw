using System;
using System.Linq;
using System.Windows;
using Dao;
using Entities;
using Microsoft.Win32;


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


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SubjectsDataGrid.ItemsSource = DaoRegistry.SubjectDao.FindAll();
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
            SubjectsDataGrid.ItemsSource = DaoRegistry.SubjectDao.FindAll();
        }

    }
}