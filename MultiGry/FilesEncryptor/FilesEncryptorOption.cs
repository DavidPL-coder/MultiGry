using System;

namespace MultiGry.FilesEncryptor
{
    class FilesEncryptorOption : IMenuOption
    {
        public string NameOption => "(De)Szyfrator plików";
        private char OptionNumberSelectedByUser;

        public OptionsCategory OptionExecuting()
        {
            do
            {
                DisplayOptions();
                UserSelectsOptions();
            }
            while (DidNotUserSelectExitOption());

            return OptionsCategory.NormalOption;
        }

        private void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("1. Utwórz plik");
            Console.WriteLine("2. Otwórz plik");
            Console.WriteLine("3. Szyfruj plik");
            Console.WriteLine("4. Odkoduj plik");
            Console.WriteLine("5. Odczytaj zaszyfrowany plik");
            Console.WriteLine("6. Edytuj plik");
            Console.WriteLine("7. Powrót do menu głównego");
        }

        private void UserSelectsOptions()
        {
            OptionNumberSelectedByUser = Console.ReadKey(true).KeyChar;
            Console.Clear();

            switch (OptionNumberSelectedByUser)
            {
                case '1': FileCreation(); break;
                case '2': OpenFile(); break;
                case '3': FileEncryption(); break;
                case '4': FileDecryption(); break;
                case '5': ReadEncryptedFile(); break;
                case '6': EditingFileUsingNotebook(); break;
            }
        }

        private void FileCreation()
        {
            var FileCreator = new FileCreator(this);
            FileCreator.FileCreation();
        }

        public string GetFilePathFromUser() =>
            Console.ReadLine() + ".txt";

        public void DisplayRequestForFilePath(string MessageToUser) =>
            Console.WriteLine(MessageToUser);

        public void DisplayRequestForFilePath() => 
            Console.WriteLine("Podaj nazwę pliku (lub ścieżkę względną): ");

        public void DisplayOnlyTheMessage(string Message)
        {
            Console.Clear();
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        public void DisplayOnlyMessageAbout_FileDoesNotExist()
        {
            var Message = "Podany plik nie istnieje!" +
                          " Upewnij się czy wprowadziłeś odpowiednią ścieżkę!";

            DisplayOnlyTheMessage(Message);
        }

        private void OpenFile()
        {
            var FileContentDisplay = new FileContentDisplay(this);
            FileContentDisplay.OpenFile();
        }

        private void FileEncryption()
        {
            var Encryptor = new EncoderFile(this);
            Encryptor.FileEncryption();
        }

        private void FileDecryption()
        {
            var Decryptor = new EncoderFile(this);
            Decryptor.FileDecryption();
        }

        private void ReadEncryptedFile()
        {
            var FileContentDisplay = new FileContentDisplay(this);
            FileContentDisplay.ReadEncryptedFile();
        }

        private void EditingFileUsingNotebook()
        {
            var Notebook = new WindowsNotebook(this);
            Notebook.OpenFileInNotebook();
        }

        private bool DidNotUserSelectExitOption() =>
            OptionNumberSelectedByUser != '7';
    }
}
