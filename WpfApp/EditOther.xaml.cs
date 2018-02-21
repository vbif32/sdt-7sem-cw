using System.ComponentModel;
using System.Windows;
using Services.EntitiesViewModels;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditOtherWindow.xaml
    /// </summary>
    public partial class EditOtherWindow : Window
    {
        public EditOtherWindow(Window owner)
        {
            Owner = owner;
            EntitiesVmRegistry = ((MainWindow) Owner).Context.EntitiesVmRegistry;
            InitializeComponent();
        }

        public EntitiesVMRegistry EntitiesVmRegistry { get; }

        private void EditOtherWindow_OnClosing(object sender, CancelEventArgs e)
        {
            // TODO: новый метод в 
            EntitiesVmRegistry.SavePosts();
        }

        private void EditOtherWindow_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}