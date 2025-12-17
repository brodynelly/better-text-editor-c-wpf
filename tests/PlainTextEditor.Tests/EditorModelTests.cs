using Xunit;
using PlainTextEditor;
using System.IO;

namespace PlainTextEditor.Tests
{
    public class EditorModelTests
    {
        [Fact]
        public void New_ShouldInitializeEmpty()
        {
            var model = new EditorModel();
            Assert.Equal(string.Empty, model.Content);
            Assert.Null(model.FilePath);
            Assert.False(model.IsDirty);
            Assert.Equal("Untitled", model.FileName);
        }

        [Fact]
        public void ContentChange_ShouldSetIsDirty()
        {
            var model = new EditorModel();
            model.Content = "Hello World";
            Assert.True(model.IsDirty);
            Assert.Equal("Hello World", model.Content);
        }

        [Fact]
        public void Save_ShouldResetIsDirty()
        {
            var model = new EditorModel();
            model.Content = "Test Content";
            string tempFile = Path.GetTempFileName();

            try
            {
                model.Save(tempFile);

                Assert.False(model.IsDirty);
                Assert.Equal(tempFile, model.FilePath);
                Assert.Equal(File.ReadAllText(tempFile), "Test Content");
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        [Fact]
        public void Load_ShouldLoadContentAndResetIsDirty()
        {
            var model = new EditorModel();
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, "Loaded Content");

            try
            {
                model.Load(tempFile);

                Assert.False(model.IsDirty);
                Assert.Equal(tempFile, model.FilePath);
                Assert.Equal("Loaded Content", model.Content);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
