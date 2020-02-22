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
        private Encryption.Operations Operation;       

        public OptionsCategory OptionExecuting()
        { 
            do
            {
                DisplayOptions();
                UserSelectsOptions();
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
                case '1':
                    FileCreation();
                    break;

                case '2':
                    ReadFile();
                    break;

                case '3':
                    FileEncryption();
                    break;

                case '4':
                    FileDecryption();
                    break;

                case '5':
                    ReadEncryptedFile();
                    break;

                case '6':
                    EditingFileUsingNotebook();
                    break;
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
            {
                var TextFromFile = new List<string>();
                ReadFataFromFile(TextFromFile);

                Console.WriteLine("Zawartość pliku: ");
                foreach (var item in TextFromFile)
                    Console.WriteLine(item);

                Console.ReadKey();
            }

            else
                DisplayMessage("Podany plik nie istnieje!");
        }

        private void ReadFataFromFile(List<string> TextFromFile)
        {
            using (var streamReader = new StreamReader(FilePath, Encoding.GetEncoding("Windows-1250")))
            {
                string tmp;
                while (( tmp = streamReader.ReadLine() ) != null)
                    TextFromFile.Add(tmp);
            }
        }

        private void FileEncryption()
        {
            Operation = Encryption.Operations.Encryption;
            FileEncoding();
        }

        private void FileDecryption()
        {
            Operation = Encryption.Operations.Decryption;
            FileEncoding();
        }

        private void FileEncoding()
        {
            UserProvidesPathToFile();           

            if (File.Exists(FilePath))
            {
                var TextFromFile = new List<string>();
                ReadFataFromFile(TextFromFile);
                EncodeFile(TextFromFile);
                DisplayingMessageAboutEncodingCompleted();
                Console.ReadKey();
            }

            else
                DisplayMessage("Podany plik nie istnieje!");
        }

        private void EncodeFile(List<string> TextFromFile)
        {
            var encryption = new Encryption();
            using (var streamWriter = new StreamWriter(FilePath, false, Encoding.GetEncoding("Windows-1250")))
            {
                for (int i = 0; i < TextFromFile.Count; ++i)
                {
                    if (Operation == Encryption.Operations.Encryption)
                        streamWriter.WriteLine(encryption.EncryptingText(TextFromFile[i]));

                    else
                        streamWriter.WriteLine(encryption.DecryptingText(TextFromFile[i]));
                }
            }
        }

        private void DisplayingMessageAboutEncodingCompleted()
        {
            if (Operation == Encryption.Operations.Encryption)
                Console.WriteLine("Zaszyfrowano plik!");

            else
                Console.WriteLine("Odkodowano plik!");
        }

        private void ReadEncryptedFile()
        {
            UserProvidesPathToFile();

            if (File.Exists(FilePath))
            {
                Operation = Encryption.Operations.Decryption;
                var TextFromFile = new List<string>();
                ReadFataFromFile(TextFromFile);

                Console.WriteLine("Odszyfrowana zawartość pliku:");
                var decryption = new Encryption();
                for (int i = 0; i < TextFromFile.Count; ++i)
                    Console.WriteLine(decryption.DecryptingText(TextFromFile[i]));

                Console.ReadKey();
            }

            else
                DisplayMessage("Podany plik nie istnieje!");
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
