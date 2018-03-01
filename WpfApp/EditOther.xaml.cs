using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Entities;
using Services;
using Services.EntitiesViewModels;
using WpfApp.Converters;
using Xceed.Wpf.Toolkit;

namespace WpfApp
{
    /// <summary>
    ///     Логика взаимодействия для EditOtherWindow.xaml
    /// </summary>
    public partial class EditOtherWindow : Window
    {
        public EditOtherWindow(ContextSingleton context)
        {
            Context = context;
            InitializeComponent();
        }

        public ContextSingleton Context { get; }

        private void EditOtherWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Context.EntitiesVmRegistry.SaveSettings();
        }

        private void EditOtherWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FullDepartmentNameTextBox.SetBinding(TextBox.TextProperty,
                new Binding("Value")
                {
                    Source = Context.EntitiesVmRegistry.Settings[(int)Settings.FullDepartmentName]
                });
            ShortDepartmentNameTextBox.SetBinding(TextBox.TextProperty,
                new Binding("Value")
                {
                    Source = Context.EntitiesVmRegistry.Settings[(int)Settings.ShortDepartmentName]
                });


            var multiBinding = new MultiBinding {Converter = new YearTextBoxMultiBindingConverter()};
            multiBinding.Bindings.Add(new Binding("Value")
            {
                Source = Context.EntitiesVmRegistry.Settings[(int)Settings.StartYear]
            });
            multiBinding.Bindings.Add(new Binding("Value")
            {
                Source = Context.EntitiesVmRegistry.Settings[(int)Settings.EndYear]
            });

            multiBinding.NotifyOnSourceUpdated = true; //this is important. 
            YearTextBox.SetBinding(TextBox.TextProperty, multiBinding);
        }

        private void Spinner_OnSpin(object sender, SpinEventArgs e)
        {
            if (e.Direction == SpinDirection.Increase)
            {
                Context.EntitiesVmRegistry.Settings[(int)Settings.StartYear].IntValue++;
                Context.EntitiesVmRegistry.Settings[(int)Settings.EndYear].IntValue++;
            }
            else
            {
                Context.EntitiesVmRegistry.Settings[(int)Settings.StartYear].IntValue--;
                Context.EntitiesVmRegistry.Settings[(int)Settings.EndYear].IntValue--;
            }
        }
    }
}