using System.Diagnostics;
using System.IO;

namespace MultiGry.FilesEncryptor
{
    class WindowsNotebook
    {
        private FilesEncryptorOption Option;
        private string FilePath;

        public WindowsNotebook(FilesEncryptorOption Option) => 
            this.Option = Option;

        public void OpenFileInNotebook()
        {
            Option.DisplayRequestForFilePath();
            FilePath = Option.GetFilePathFromUser();

            if (File.Exists(FilePath))
                LaunchingNotebook();

            else
                Option.DisplayOnlyMessageAbout_FileDoesNotExist();
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

            using (Process.Start(NotepadInfo))
                ;
        }
    }
}
