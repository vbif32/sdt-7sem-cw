using System.ComponentModel;
using System.Linq;
using System.Windows;
using Services;
using Services.EntitiesViewModels;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditTeacherWindow.xaml
    /// </summary>
    public partial class EditTeacherWindow : Window
    {
        public EditTeacherWindow(ContextSingleton context)
        {
            Context = context;
            InitializeComponent();
        }

        public ContextSingleton Context { get; }

        private void EditTeacherWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Context.EntitiesVmRegistry.Teachers.Count == 0)
                Context.EntitiesVmRegistry.Teachers.Add(new TeacherVM());
            TeacherListBox.ItemsSource = Context.EntitiesVmRegistry.Teachers;
            PostComboBox.ItemsSource = Context.EntitiesVmRegistry.Posts.Select(pvm => pvm.ModelObject);
            TeacherListBox.SelectedIndex = 0;
        }

        private void EditTeacherWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Context.EntitiesVmRegistry.SaveTeachers();
        }

        private void AddTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            Context.EntitiesVmRegistry.Teachers.Add(new TeacherVM());
            TeacherListBox.SelectedIndex = TeacherListBox.Items.Count - 1;
        }

        private void RemoveTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            Context.EntitiesVmRegistry.Teachers.Remove((TeacherVM) TeacherListBox.SelectedItem);
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
        //    var post = ((TeacherVM) TeacherListBox.SelectedItem).ModelObject;
        //    foreach (var item in PostComboBox.Items)
        //        if (post.Equals(item))
        //            PostComboBox.SelectedItem = item;
        //}
    }
}