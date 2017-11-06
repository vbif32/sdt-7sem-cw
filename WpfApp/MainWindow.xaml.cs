using System.Windows;

namespace WpfApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TeacherMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var editTeacherWindow = new EditTeacherWindow();
            editTeacherWindow.ShowDialog();
        }

        private void PositionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var editPostWindow = new EditPostWindow();
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