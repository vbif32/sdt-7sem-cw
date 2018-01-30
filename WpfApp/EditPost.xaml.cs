using System.ComponentModel;
using System.Windows;
using EntitiesViewModels;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditPostWindow.xaml
    /// </summary>
    public partial class EditPostWindow : Window
    {
        public EditPostWindow(Window owner)
        {
            Owner = owner;
            EntitiesVmRegistry = ((MainWindow) Owner).EntitiesVmRegistry;
            InitializeComponent();
        }

        public EntitiesVMRegistry EntitiesVmRegistry { get; }

        private void EditPostWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (EntitiesVmRegistry.Posts.Count == 0)
                EntitiesVmRegistry.Posts.Add(new PostVM());
            PostListBox.ItemsSource = EntitiesVmRegistry.Posts;
            PostListBox.SelectedIndex = 0;
        }

        private void EditPostWindow_OnClosing(object sender, CancelEventArgs e)
        {
            EntitiesVmRegistry.SavePosts();
        }

        private void AddPostButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            EntitiesVmRegistry.Posts.Add(new PostVM());
            PostListBox.SelectedIndex = PostListBox.Items.Count - 1;
        }

        private void RemovePostButton_Click(object sender, RoutedEventArgs e)
        {
            EntitiesVmRegistry.Posts.Remove((PostVM) PostListBox.SelectedItem);
        }

        private bool IsRequiredFieldsFilled()
        {
            return FullNameTextBox.Text.Length > 3 &&
                   ShortNameTextBox.Text.Length > 0 &&
                   HoursIntegerUpDown.Value.Value > 0;
        }
    }
}