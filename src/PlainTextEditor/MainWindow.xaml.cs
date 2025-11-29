using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace PlainTextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EditorModel _model;

        public MainWindow()
        {
            InitializeComponent();
            _model = new EditorModel();

            _model.PropertyChanged += Model_PropertyChanged;
            UpdateTitle();
        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
             if (e.PropertyName == nameof(EditorModel.IsDirty) || e.PropertyName == nameof(EditorModel.FilePath))
             {
                 UpdateTitle();
                 this.DataContext = _model;
             }
        }

        private void UpdateTitle()
        {
            string title = "Plain Text Editor - " + _model.FileName;
            if (_model.IsDirty)
            {
                title += "*";
            }
            this.Title = title;
        }

        private void EditorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_model.Content != EditorTextBox.Text)
            {
                _model.Content = EditorTextBox.Text;
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmDiscardChanges())
            {
                _model.New();
                EditorTextBox.Text = _model.Content;
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmDiscardChanges())
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                    DefaultExt = ".txt"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        _model.Load(openFileDialog.FileName);
                        EditorTextBox.Text = _model.Content;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error opening file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Plain Text Editor\nVersion 1.0\n\nA simple text editor for Module 8.", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool Save()
        {
            if (string.IsNullOrEmpty(_model.FilePath))
            {
                return SaveAs();
            }

            try
            {
                _model.Save(_model.FilePath);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool SaveAs()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = ".txt",
                FileName = _model.FileName
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    _model.Save(saveFileDialog.FileName);
                    return true;
                }
                catch (Exception ex)
                {
                     MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                     return false;
                }
            }
            return false;
        }

        private bool ConfirmDiscardChanges()
        {
            if (!_model.IsDirty) return true;

            var result = MessageBox.Show("You have unsaved changes. Do you want to save them?", "Unsaved Changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                return Save();
            }
            else if (result == MessageBoxResult.No)
            {
                return true;
            }
            else // Cancel
            {
                return false;
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (!ConfirmDiscardChanges())
            {
                e.Cancel = true;
            }
            base.OnClosing(e);
        }
    }
}
