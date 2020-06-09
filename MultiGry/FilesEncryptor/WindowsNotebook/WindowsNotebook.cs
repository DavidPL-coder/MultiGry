using System.Diagnostics;

namespace MultiGry.FilesEncryptor
{
    public class WindowsNotebook : IWindowsNotebook
    {
        private IFakeConsole DummyConsole;
        private IFakeFile File;        
        private string FilePath;
        private MessageDisplay MessageDisplay;
        private IProcess DummyProcess;

        public WindowsNotebook()
        {
            DummyConsole = new FakeConsole();
            File = new FakeFile();
            MessageDisplay = new MessageDisplay(DummyConsole);
            DummyProcess = new SurrogateProcess();
        }

        public WindowsNotebook(IFakeConsole DummyConsole, IFakeFile FakeFile, 
                               IProcess DummyProcess)
        {
            this.DummyConsole = DummyConsole;
            File = FakeFile;
            MessageDisplay = new MessageDisplay(DummyConsole);
            this.DummyProcess = DummyProcess;
        }

        public void EditFile()
        {
            RequestDisplay.DisplayRequestForFilePath();
            FilePath = GetterFilePath.GetFilePathFromUser(DummyConsole);

            if (File.Exists(FilePath))
                LaunchingNotebook();

            else
                MessageDisplay.DisplayOnlyMessageAbout_FileDoesNotExist();
        }

        private void LaunchingNotebook()
        {
            var NotepadInfo = new ProcessStartInfo
            {
                FileName = "notepad.exe",
                Arguments = FilePath,
                UseShellExecute = false,
                RedirectStandardOutput = false,
            };
            
            using (DummyProcess.Start(NotepadInfo))
                ;
        }
    }
}
