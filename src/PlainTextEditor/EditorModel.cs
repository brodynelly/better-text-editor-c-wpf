using System.IO;

namespace PlainTextEditor
{
    // The "Model" representing the document state
    public class EditorModel : ObservableObject
    {
        private string _content = string.Empty;
        private string? _filePath;
        private bool _isDirty;

        public string Content
        {
            get => _content;
            set
            {
                if (SetProperty(ref _content, value))
                {
                    IsDirty = true;
                }
            }
        }

        public string? FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }

        public bool IsDirty
        {
            get => _isDirty;
            set => SetProperty(ref _isDirty, value);
        }

        public string FileName => string.IsNullOrEmpty(FilePath) ? "Untitled" : Path.GetFileName(FilePath);

        public void New()
        {
            _content = string.Empty;
            _filePath = null;
            _isDirty = false;
            OnPropertyChanged(nameof(Content));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsDirty));
        }

        public void Load(string path)
        {
            _content = File.ReadAllText(path);
            _filePath = path;
            _isDirty = false;
            OnPropertyChanged(nameof(Content));
            OnPropertyChanged(nameof(FilePath));
            OnPropertyChanged(nameof(IsDirty));
        }

        public void Save(string path)
        {
            File.WriteAllText(path, Content);
            FilePath = path;
            IsDirty = false;
        }
    }
}
