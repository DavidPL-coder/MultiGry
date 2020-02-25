using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MultiGry
{
    // for encrypting files, the Caesar cipher was used
    class FilesEncryptionOption : IMenuOption
    {
        public string NameOption => "(De)Szyfrowanie plików";
        private char OptionNumberSelectedByUser;
        private string FilePath;
        private TextEncoder.Operations Operation;
        private List<string> TextFromFile;
        private TextEncoder textEncoder;

        public FilesEncryptionOption()
        {
            TextFromFile = new List<string>();
            textEncoder = new TextEncoder();
        }

        public OptionsCategory OptionExecuting()
        {
            do
            {
                DisplayOptions();
                UserSelectsOptions();
                TextFromFile.Clear();
            }
            while (OptionNumberSelectedByUser != '7');

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
                case '2': ReadFile(); break;
                case '3': FileEncryption(); break;
                case '4': FileDecryption(); break;
                case '5': ReadEncryptedFile(); break;
                case '6': EditingFileUsingNotebook(); break;
            }
        }

        private void FileCreation()
        {
            UserProvidesPathToFile(MessageToUser: "Podaj nazwę pliku (plik będzie utworzony w lokalizacji programu, można też użyć ścieżki względnej): ");

            if (!File.Exists(FilePath))
                using (File.CreateText(FilePath))
                    ;

            else
                DisplayMessage("Ten plik już istnieje (w danej ścieżce)!");
        }

        private void UserProvidesPathToFile(string MessageToUser = "Podaj nazwę pliku:")
        {
            Console.WriteLine(MessageToUser);
            FilePath = Console.ReadLine() + ".txt";
            Console.Clear();
        }

        private void DisplayMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        private void ReadFile()
        {
            UserProvidesPathToFile(MessageToUser: "Podaj nazwę pliku (lub ścieżkę względną):");

            if (File.Exists(FilePath))
                ReadContentsOfFile();

            else
                DisplayMessage("Podany plik nie istnieje!");
        }

        private void ReadContentsOfFile()
        {
            ReadDataFromFile();
            DisplayFileContents();
            Console.ReadKey();
        }

        private void ReadDataFromFile()
        {
            using (var streamReader = new StreamReader(FilePath, Encoding.GetEncoding("Windows-1250")))
            {
                string tmp;
                while (( tmp = streamReader.ReadLine() ) != null)
                    TextFromFile.Add(tmp);
            }
        }

        private void DisplayFileContents()
        {
            Console.WriteLine("Zawartość pliku: ");
            foreach (var item in TextFromFile)
                Console.WriteLine(item);
        }

        private void FileEncryption()
        {
            Operation = TextEncoder.Operations.Encryption;
            FileEncoding();
        }

        private void FileDecryption()
        {
            Operation = TextEncoder.Operations.Decryption;
            FileEncoding();
        }

        private void FileEncoding()
        {
            UserProvidesPathToFile();

            if (File.Exists(FilePath))
                FileContentEncoding();

            else
                DisplayMessage("Podany plik nie istnieje!");
        }

        private void FileContentEncoding()
        {
            ReadDataFromFile();
            EncodeFile();
            DisplayingMessageAboutEncodingCompleted();
            Console.ReadKey();
        }

        private void EncodeFile()
        {
            using (var streamWriter = new StreamWriter(FilePath, false, Encoding.GetEncoding("Windows-1250")))
            {
                foreach (var item in TextFromFile)
                {
                    string TextToSaveToFile = (Operation == TextEncoder.Operations.Encryption) ? textEncoder.EncryptingText(item)
                                                                                               : textEncoder.DecryptingText(item);
                    streamWriter.WriteLine(TextToSaveToFile);
                }
            }
        }

        private void DisplayingMessageAboutEncodingCompleted()
        {
            if (Operation == TextEncoder.Operations.Encryption)
                Console.WriteLine("Zaszyfrowano plik!");

            else
                Console.WriteLine("Odkodowano plik!");
        }

        private void ReadEncryptedFile()
        {
            UserProvidesPathToFile();

            if (File.Exists(FilePath))
                ReadEncryptedFileContent();

            else
                DisplayMessage("Podany plik nie istnieje!");
        }

        private void ReadEncryptedFileContent()
        {
            Operation = TextEncoder.Operations.Decryption;
            ReadDataFromFile();

            Console.WriteLine("Odszyfrowana zawartość pliku:");
            foreach (var item in TextFromFile)
                Console.WriteLine(textEncoder.DecryptingText(item));

            Console.ReadKey();
        }

        private void EditingFileUsingNotebook()
        {
            UserProvidesPathToFile();

            if (File.Exists(FilePath))
                LaunchingNotebook();

            else
                DisplayMessage("Podany plik nie istnieje!");
        }

        private void LaunchingNotebook()
        {
            ProcessStartInfo NotepadInfo = new ProcessStartInfo
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
