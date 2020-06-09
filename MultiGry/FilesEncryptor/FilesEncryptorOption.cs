using System;

namespace MultiGry.FilesEncryptor
{
    public class FilesEncryptorOption : IMenuOption
    {
        public string NameOption => "(De)Szyfrator plików";
        private char OptionNumberSelectedByUser;
        private IFakeConsole DummyConsole;
        private IFileCreator FileCreator;
        private IFileContentDisplay FileContentDisplay;
        private IEncoderFile EncoderFile;
        private IWindowsNotebook WindowsNotebook;

        public FilesEncryptorOption()
        {
            DummyConsole = new FakeConsole();
            FileCreator = new FileCreator();
            FileContentDisplay = new FileContentDisplay();
            EncoderFile = new EncoderFile();
            WindowsNotebook = new WindowsNotebook();
        }

        public FilesEncryptorOption(IFakeConsole DummyConsole, IFileCreator FileCreator,
                                    IFileContentDisplay FileContentDisplay, 
                                    IEncoderFile EncoderFile,
                                    IWindowsNotebook WindowsNotebook)
        {
            this.DummyConsole = DummyConsole;
            this.FileCreator = FileCreator;
            this.FileContentDisplay = FileContentDisplay;
            this.EncoderFile = EncoderFile;
            this.WindowsNotebook = WindowsNotebook;
        }

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
            DummyConsole.Clear();
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
            OptionNumberSelectedByUser = DummyConsole.ReadKey().KeyChar;
            DummyConsole.Clear();

            switch (OptionNumberSelectedByUser)
            {
                case '1': FileCreator.FileCreation(); break;
                case '2': FileContentDisplay.OpenFile(); break;
                case '3': EncoderFile.FileEncryption(); break;
                case '4': EncoderFile.FileDecryption(); break;
                case '5': FileContentDisplay.ReadEncryptedFile(); break;
                case '6': WindowsNotebook.EditFile(); break;
            }
        }

        private bool DidNotUserSelectExitOption() =>
            OptionNumberSelectedByUser != '7';
    }
}
