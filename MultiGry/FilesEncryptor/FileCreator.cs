using System;
using System.IO;

namespace MultiGry.FilesEncryptor
{
    class FileCreator
    {
        private FilesEncryptorOption Option;
        private string FilePath;

        public FileCreator(FilesEncryptorOption Option) =>
            this.Option = Option;

        public void FileCreation()
        {
            Option.DisplayRequestForFilePath(GetMessageToUser());
            FilePath = Option.GetFilePathFromUser();

            if (!File.Exists(FilePath))
                TryToCreateFile();

            else
                Option.DisplayOnlyTheMessage("Ten plik już istnieje (w danej ścieżce)!");
        }

        private string GetMessageToUser() =>
            "Podaj nazwę pliku (plik będzie w ścieżce " +
            "programu, lub można użyć ścieżki względnej): ";

        private void TryToCreateFile()
        {
            try
            {
                using (File.CreateText(FilePath))
                    Option.DisplayOnlyTheMessage("Utworzono nowy plik!");
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                System.Threading.Thread.Sleep(2000);
            }
        } 
    }
}
