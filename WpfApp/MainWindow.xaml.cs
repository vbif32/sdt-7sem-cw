using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Dao;
using Entities;
using EntitiesViewModels;
using Microsoft.Win32;
using Services;
using InitializingNewItemEventArgs = System.Windows.Controls.InitializingNewItemEventArgs;
using SelectedCellsChangedEventArgs = Microsoft.Windows.Controls.SelectedCellsChangedEventArgs;

// ReSharper disable PossibleMultipleEnumeration


namespace WpfApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DaoRegistry _daoRegistry;
        private EntitiesVMRegistry _entitiesVmRegistry;
        private LiteDbModel _model;

        public MainWindow()
        {
            InitializeComponent();
        }

        public LiteDbModel Model => _model ?? (_model = LiteDbModel.CreateModel());
        public DaoRegistry DaoRegistry => _daoRegistry ?? (_daoRegistry = new DaoRegistry(Model));

        public EntitiesVMRegistry EntitiesVmRegistry =>
            _entitiesVmRegistry ?? (_entitiesVmRegistry = new EntitiesVMRegistry(DaoRegistry));

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SubjectsDataGrid.ItemsSource = EntitiesVmRegistry.Subjects;
                TeacherComboBoxColumn.ItemsSource = EntitiesVmRegistry.Teachers;
                TeachersDataGrid.ItemsSource = EntitiesVmRegistry.Teachers;
                SubjectComboBoxColumn.ItemsSource = EntitiesVmRegistry.Subjects;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка при загрузке базы данных",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TeacherMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EditTeacherWindow(this).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PositionsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EditPostWindow(this).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OtherMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new EditOtherWindow(this).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetEntries_Click(object sender, RoutedEventArgs e)
        {
            EntitiesVmRegistry.Entries.Clear();
            EntitiesVmRegistry.SaveChanges();
            DaoRegistry.EntryDao.DeleteAll();
        }

        private void ImportNew101FormMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != true) return;

            ResetSubjects();
            var f101 = Converter.F101FromExcel(openFileDialog.FileName);
            var subjects = Converter.Convert(f101);
            if (!subjects.Any())
                MessageBox.Show("Не получилось извлечь предметы", "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            DaoRegistry.SubjectDao.Insert(subjects);
            EntitiesVmRegistry.ResetCollections();
        }

        private void ResetSubjects()
        {
            EntitiesVmRegistry.Entries.Clear();
            DaoRegistry.EntryDao.DeleteAll();
            EntitiesVmRegistry.Subjects.Clear();
            DaoRegistry.SubjectDao.DeleteAll();
        }


        private void SubjectsDataGrid_OnSelected(object sender, SelectedCellsChangedEventArgs cellsChangedEventArgs)
        {
            DetailSubjectDockPanel.IsEnabled = true;
        }


        private EntryVM CreateNewEntry(SubjectVM subject = null, TeacherVM teacher = null)
        {
            var newEntry = new EntryVM {Subject = subject, Teacher = teacher};
            EntitiesVmRegistry.Entries.Add(newEntry);
            return newEntry;
        }

        private bool IsRequiredFieldsFilled()
        {
            return EntriesBySubjectDataGrid.Items.OfType<EntryVM>().All(entry => entry.Teacher != null);
        }

        private static bool IsTextAllowed(string text)
        {
            var regex = new Regex("[^0-9.]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void OnPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                var text = (string) e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                    e.CancelCommand();
            }
            else
            {
                e.CancelCommand();
            }
        }

        private void EntriesBySubjectDataGrid_OnInitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newEntry = e.NewItem as EntryVM;
            EntitiesVmRegistry.Entries.Add(newEntry);
            newEntry.Subject = SubjectsDataGrid.SelectedItem as SubjectVM;
            newEntry.Teacher = EntitiesVmRegistry.Teachers.FirstOrDefault();
        }

        private void EntriesByTeacherDataGrid_OnInitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newEntry = e.NewItem as EntryVM;
            EntitiesVmRegistry.Entries.Add(newEntry);
            newEntry.Teacher = TeachersDataGrid.SelectedItem as TeacherVM;
            newEntry.Subject = EntitiesVmRegistry.Subjects.FirstOrDefault();
        }

        private void TeachersDataGrid_OnSelectedCellsChangedted(object sender, SelectedCellsChangedEventArgs e)
        {
            DetailTeacherDockPanel.IsEnabled = true;
        }

        private void TeacherToSubjectButton_OnClick(object sender, RoutedEventArgs e)
        {
            var commandParameter = ((Button) sender).CommandParameter;
            SubjectsTabItem.IsSelected = true;
            SubjectsDataGrid.SelectedItem = commandParameter;
        }

        private void SubjectToTeacherButton_OnClick(object sender, RoutedEventArgs e)
        {
            var commandParameter = ((Button) sender).CommandParameter;
            TeachersTabItem.IsSelected = true;
            TeachersDataGrid.SelectedItem = commandParameter;
        }

        private void SaveEntriesBySubjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled()) return;
            EntitiesVmRegistry.SaveEntries();
        }

        private void SaveEntriesByTeacherButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled()) return;
            EntitiesVmRegistry.SaveEntries();
        }

        private void ExportF106MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != true) return;
            //Converter.ToF106(EntitiesVmRegistry, openFileDialog.FileName);
        }

        private void ExportF115MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != true) return;
            //Converter.ToF115(EntitiesVmRegistry, openFileDialog.FileName);
        }

        private void ExportF13MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (saveFileDialog.ShowDialog() != true) return;
            Converter.ToF16(EntitiesVmRegistry, saveFileDialog.FileName);
        }

        
    }
}