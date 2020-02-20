using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace MultiGry
{
    class FilesEncryptionOption : IMenuOption
    {
        public string NameOption => "(De)Szyfrowanie plików";
        private char OptionNumberSelectedByUser;
        private string FilePath;

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
                ReadingLinesFromFile();
                Console.ReadKey();
            }

            else
                DisplayMessage("Podany plik nie istnieje!");
        }

        private void ReadingLinesFromFile()
        {
            using (var streamReader = new StreamReader(FilePath, Encoding.GetEncoding("Windows-1250")))
            {
                Console.WriteLine("Zawartość pliku: ");

                string tmp;
                while (( tmp = streamReader.ReadLine() ) != null)
                    Console.WriteLine(tmp);
            }
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
