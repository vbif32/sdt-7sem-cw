using System.ComponentModel;
using System.Linq;
using System.Windows;
using EntitiesViewModels;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditTeacherWindow.xaml
    /// </summary>
    public partial class EditTeacherWindow : Window
    {
        public EditTeacherWindow(Window owner)
        {
            Owner = owner;
            EntitiesVmRegistry = ((MainWindow) Owner).EntitiesVmRegistry;
            InitializeComponent();
        }

        public EntitiesVMRegistry EntitiesVmRegistry { get; }

        private void EditTeacherWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (EntitiesVmRegistry.Teachers.Count == 0)
                EntitiesVmRegistry.Teachers.Add(new TeacherVM());
            TeacherListBox.ItemsSource = EntitiesVmRegistry.Teachers;
            PostComboBox.ItemsSource = EntitiesVmRegistry.Posts.Select(pvm => pvm.Post);
            TeacherListBox.SelectedIndex = 0;
        }

        private void EditTeacherWindow_OnClosing(object sender, CancelEventArgs e)
        {
            EntitiesVmRegistry.SaveTeachers();
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            EntitiesVmRegistry.Teachers.Add(new TeacherVM());
            TeacherListBox.SelectedIndex = TeacherListBox.Items.Count - 1;
        }

        private void RemoveTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            EntitiesVmRegistry.Teachers.Remove((TeacherVM) TeacherListBox.SelectedItem);
        }

        private bool IsRequiredFieldsFilled()
        {
            return SurnameTextBox.Text.Length > 1
                   && NameTextBox.Text.Length > 1
                   && MiddleNameTextBox.Text.Length > 1
                   && RateSingleUpDown.Value > 0
                   && PostComboBox.SelectedItem != null
                   && ((bool) FullTimeRadioButton.IsChecked || (bool) InCompatibilityRadioButton.IsChecked ||
                       (bool) ExCompatibilityRadioButton.IsChecked)
                ;
        }

        //private void TeacherListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (TeacherListBox.SelectedItem == null)
        //        return;
        //    var должность = ((TeacherVM) TeacherListBox.SelectedItem).Post;
        //    foreach (var item in PostComboBox.Items)
        //        if (должность.Equals(item))
        //            PostComboBox.SelectedItem = item;
        //}
    }
}