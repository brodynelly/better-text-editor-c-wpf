# Plain Text Editor

A lightweight, robust plain text editor built with C# and WPF for .NET 8.

## Features

- **Standard File Operations**: Create, Open, Save, Save As.
- **Smart State Management**: Tracks unsaved changes ("dirty" state) and prompts user before data loss.
- **Dynamic UI**: Resizable window with responsive text area.
- **Validation**: Ensures proper file handling.

## Requirements

- Windows OS (WPF dependency)
- .NET 8 SDK

## Getting Started

### Building

1. Clone the repository.
2. Open `PlainTextEditor.sln` in Visual Studio or use the CLI:
   ```bash
   dotnet build
   ```

### Running

```bash
dotnet run --project src/PlainTextEditor/PlainTextEditor.csproj
```

### Testing

```bash
dotnet test
```

## Architecture

The application follows a pragmatic separation of concerns:
- **Model**: `EditorModel` manages the document state (content, file path, dirty flag) and implements `INotifyPropertyChanged`.
- **View**: `MainWindow.xaml` defines the UI layout.
- **Controller/Logic**: `MainWindow.xaml.cs` binds the UI events to the Model operations.

## Contributing

Please check `AGENTS.md` (if available) or standard contribution guidelines.
