using System;

namespace MultiGry.FilesEncryptor
{
    public class FileCreator : IFileCreator
    {
        private string FilePath;
        private IFakeFile File;
        private IFakeConsole DummyConsole;
        private MessageDisplay MessageDisplay;

        public FileCreator()
        {
            File = new FakeFile();
            DummyConsole = new FakeConsole();
            MessageDisplay = new MessageDisplay(DummyConsole);
        }

        public FileCreator(IFakeFile File, IFakeConsole DummyConsole)
        {
            this.File = File;
            this.DummyConsole = DummyConsole;
            MessageDisplay = new MessageDisplay(DummyConsole);
        }

        public void FileCreation()
        {
            RequestDisplay.DisplayRequestForFilePath(GetMessageToUser());
            FilePath = GetterFilePath.GetFilePathFromUser(DummyConsole);
                       
            if (!File.Exists(FilePath))
                TryToCreateFile();

            else
                MessageDisplay.DisplayOnlyTheMessage("Ten plik już istnieje " +
                                                     "(w danej ścieżce)!");
        }

        private string GetMessageToUser() =>
            "Podaj nazwę pliku (plik będzie w ścieżce " +
            "programu, lub można użyć ścieżki względnej): ";

        private void TryToCreateFile()
        {
            try
            {
                using (File.CreateText(FilePath))
                    MessageDisplay.DisplayOnlyTheMessage("Utworzono nowy plik!");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                System.Threading.Thread.Sleep(2000);
            }
        } 
    }
}
