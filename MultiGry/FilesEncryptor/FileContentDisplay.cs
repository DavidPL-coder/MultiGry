using System;
using System.Collections.Generic;
using System.IO;

namespace MultiGry.FilesEncryptor
{
    class FileContentDisplay
    {
        private FilesEncryptorOption Option;
        private string FilePath;
        private List<string> TextFromFile;

        public FileContentDisplay(FilesEncryptorOption Option) =>
            this.Option = Option;

        public void OpenFile()
        {
            Option.DisplayRequestForFilePath();
            FilePath = Option.GetFilePathFromUser();

            if (File.Exists(FilePath))
                ReadContentsOfFile();

            else
                Option.DisplayOnlyMessageAbout_FileDoesNotExist();
        }

        private void ReadContentsOfFile()
        {
            SetTextFromFile();
            DisplayOnlyFileContents();
            Console.ReadKey();
        }

        private void SetTextFromFile()
        {
            var ReaderFile = new ReaderFile();
            TextFromFile = ReaderFile.ReadDataFromFile(FilePath);
        }

        private void DisplayOnlyFileContents()
        {
            Console.Clear();
            Console.WriteLine("Zawartość pliku: ");
            foreach (var item in TextFromFile)
                Console.WriteLine(item);
        }

        public void ReadEncryptedFile()
        {
            Option.DisplayRequestForFilePath();
            FilePath = Option.GetFilePathFromUser();

            if (File.Exists(FilePath))
                ReadEncryptedContent();

            else
                Option.DisplayOnlyMessageAbout_FileDoesNotExist();
        }

        private void ReadEncryptedContent()
        {
            Console.Clear();
            SetTextFromFile();
            DisplayDecryptedContent();
            Console.ReadKey();
        }

        private void DisplayDecryptedContent()
        {
            var TextEncoder = new TextEncoder();  
            
            Console.WriteLine("Odszyfrowana zawartość pliku:");
            foreach (var item in TextFromFile)
            {
                var DecryptedText = TextEncoder.DecryptingText(item);
                Console.WriteLine(DecryptedText);
            }
        }
    }
}
