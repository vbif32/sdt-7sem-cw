﻿using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Services;
using Services.EntitiesViewModels;
using Button = System.Windows.Controls.Button;
using InitializingNewItemEventArgs = System.Windows.Controls.InitializingNewItemEventArgs;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using SelectedCellsChangedEventArgs = Microsoft.Windows.Controls.SelectedCellsChangedEventArgs;

// ReSharper disable PossibleMultipleEnumeration


namespace WpfApp
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ContextSingleton Context = ContextSingleton.Instance;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SubjectsDataGrid.ItemsSource = Context.EntitiesVmRegistry.Subjects;
                TeacherComboBoxColumn.ItemsSource = Context.EntitiesVmRegistry.Teachers;
                TeachersDataGrid.ItemsSource = Context.EntitiesVmRegistry.Teachers;
                SubjectComboBoxColumn.ItemsSource = Context.EntitiesVmRegistry.Subjects;
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
                new EditTeacherWindow(Context).ShowDialog();
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
                new EditPostWindow(Context).ShowDialog();
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
                new EditOtherWindow(Context).ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetEntries_Click(object sender, RoutedEventArgs e)
        {
            ControllerService.ResetEntries();
        }

        private void ImportNew101FormMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() != true) return;

            string mes;
            try
            {
                mes = ControllerService.ImportF101(openFileDialog.FileName);
            }
            catch (Exception exception)
            {
                mes = exception.Message;
                mes += "\n" + exception.StackTrace;

                Console.WriteLine(exception);
            }
            MessageBox.Show(mes, "Information", MessageBoxButton.OK);
        }

        private void SubjectsDataGrid_OnSelected(object sender, SelectedCellsChangedEventArgs cellsChangedEventArgs)
        {
            DetailSubjectDockPanel.IsEnabled = true;
        }


        private EntryVM CreateNewEntry(SubjectVM subject = null, TeacherVM teacher = null)
        {
            var newEntry = new EntryVM {Subject = subject, Teacher = teacher};
            Context.EntitiesVmRegistry.Entries.Add(newEntry);
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
            Context.EntitiesVmRegistry.Entries.Add(newEntry);
            newEntry.Subject = SubjectsDataGrid.SelectedItem as SubjectVM;
            newEntry.Teacher = Context.EntitiesVmRegistry.Teachers.FirstOrDefault();
        }

        private void EntriesByTeacherDataGrid_OnInitializingNewItem(object sender, InitializingNewItemEventArgs e)
        {
            var newEntry = e.NewItem as EntryVM;
            Context.EntitiesVmRegistry.Entries.Add(newEntry);
            newEntry.Teacher = TeachersDataGrid.SelectedItem as TeacherVM;
            newEntry.Subject = Context.EntitiesVmRegistry.Subjects.FirstOrDefault();
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
            Context.EntitiesVmRegistry.SaveEntries();
        }

        private void SaveEntriesByTeacherButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!IsRequiredFieldsFilled()) return;
            Context.EntitiesVmRegistry.SaveEntries();
        }

        private void ExportF106MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" };
            if (saveFileDialog.ShowDialog() == true)
            {
                if (File.Exists(saveFileDialog.FileName) && IsFileLocked(saveFileDialog.FileName))
                    MessageBox.Show("Выбранный файл заблокирован!");
                ControllerService.ExportToF106(saveFileDialog.FileName);
                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
        }

        private void ExportF115MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new SaveFileDialog {Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"};
            if (openFileDialog.ShowDialog() != true)
            {
                //Converter.ExportToF115(EntitiesVmRegistry, openFileDialog.FileName);    
            }
        }

        private void ExportF13MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog {Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*"};
            if (saveFileDialog.ShowDialog() == true)
            {
                if (File.Exists(saveFileDialog.FileName) && IsFileLocked(saveFileDialog.FileName))
                    MessageBox.Show("Выбранный файл заблокирован!");
                ControllerService.ExportToF13(saveFileDialog.FileName);
                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
        }

        private void ExportIPMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog ();
            folderBrowserDialog.ShowDialog();
            foreach (var teacher in Context.EntitiesVmRegistry.Teachers)
                ControllerService.ExportToIP(folderBrowserDialog.SelectedPath + teacher.Surname + "ИП.xlsx", teacher);
            MessageBox.Show("ИП выгружены");
        }

        private void ExportIP_Click(object sender, RoutedEventArgs e)
        {
            var commandParameter = ((Button) sender).CommandParameter;
            var saveFileDialog = new SaveFileDialog { Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*" };
            if (saveFileDialog.ShowDialog() == true)
            {
                if (File.Exists(saveFileDialog.FileName) && IsFileLocked(saveFileDialog.FileName))
                    MessageBox.Show("Выбранный файл заблокирован!");
                ControllerService.ExportToIP(saveFileDialog.FileName, (TeacherVM)commandParameter);
                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
        }

        private void ResetSubjects_Click(object sender, RoutedEventArgs e)
        {
            ControllerService.ResetSubjects();
        }

        private void EntriesBySubjectDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsRequiredFieldsFilled())
                Context.EntitiesVmRegistry.SaveEntries();
        }

        private void EntriesByTeacherDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsRequiredFieldsFilled())
                Context.EntitiesVmRegistry.SaveEntries();
        }

        private bool IsFileLocked(string path) => IsFileLocked(new FileInfo(path));


        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }
            return false;
        }
    }
}