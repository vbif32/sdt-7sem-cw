using System.Windows;
using Dao;
using Entities;

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
            SubjectsDataGrid.ItemsSource = DaoRegistry.SubjectDao.FindAll();
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
        }

        private void ResetSubjects_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Open101MenuItem_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}