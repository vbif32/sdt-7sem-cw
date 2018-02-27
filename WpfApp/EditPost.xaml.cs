using System.ComponentModel;
using System.Windows;
using Services;
using Services.EntitiesViewModels;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditPostWindow.xaml
    /// </summary>
    public partial class EditPostWindow : Window
    {
        public EditPostWindow(ContextSingleton context)
        {
            Context = context;
            InitializeComponent();
        }

        public ContextSingleton Context { get; }

        private void EditPostWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (Context.EntitiesVmRegistry.Posts.Count == 0)
                Context.EntitiesVmRegistry.Posts.Add(new PostVM());
            PostListBox.ItemsSource = Context.EntitiesVmRegistry.Posts;
            PostListBox.SelectedIndex = 0;
        }

        private void EditPostWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Context.EntitiesVmRegistry.SavePosts();
        }

        private void AddPostButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled())
                return;
            Context.EntitiesVmRegistry.Posts.Add(new PostVM());
            PostListBox.SelectedIndex = PostListBox.Items.Count - 1;
        }

        private void RemovePostButton_Click(object sender, RoutedEventArgs e)
        {
            Context.EntitiesVmRegistry.Posts.Remove((PostVM) PostListBox.SelectedItem);
        }

        private bool IsRequiredFieldsFilled()
        {
            return FullNameTextBox.Text.Length > 3 &&
                   ShortNameTextBox.Text.Length > 0 &&
                   HoursIntegerUpDown.Value.Value > 0;
        }
    }
}