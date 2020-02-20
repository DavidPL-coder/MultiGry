using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MultiGry
{
    class FilesEncryptionOption : IMenuOption
    {
        public string NameOption => "(De)Szyfrowanie plików";

        public OptionsCategory OptionExecuting()
        {
            DisplayOptions();
            UserSelectsOptions();


            return OptionsCategory.NormalOption;
        }

        private void DisplayOptions()
        {
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
            char OptionNumberSelectedByUser = Console.ReadKey(true).KeyChar;
            Console.Clear();

            switch (OptionNumberSelectedByUser)
            {
                case '1':
                    FileCreation();
                    break;

                case '2':

                    Console.WriteLine("Podaj nazwę pliku do otwarcia (lub ścieżkę względną): ");
                    string FilePath = Console.ReadLine() + ".txt";
                    Console.Clear();

                    if (File.Exists(FilePath))
                    {
                        using (StreamReader streamReader = File.OpenText(FilePath))
                        {
                            Console.WriteLine("Zawartość pliku: ");

                            string tmp;
                            while (( tmp = streamReader.ReadLine() ) != null)
                                Console.WriteLine(tmp);
                        }

                        Console.ReadKey();
                    }

                    else
                    {
                        Console.WriteLine("Ten plik nie istnieje!");
                        System.Threading.Thread.Sleep(1500);
                    }

                    break;
            }
        }

        private void FileCreation()
        {
            Console.WriteLine("Podaj nazwę pliku (plik będzie utworzony w lokalizacji programu, można też użyć ścieżki względnej): ");
            string FilePath = Console.ReadLine() + ".txt";
            Console.Clear();

            if (!File.Exists(FilePath))
                using (File.CreateText(FilePath))
                    ;

            else
            {
                Console.WriteLine("Ten plik już istnieje (w danej ścieżce)!");
                System.Threading.Thread.Sleep(1500);
            }
        }
    }
}
