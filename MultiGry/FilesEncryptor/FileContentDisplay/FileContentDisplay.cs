using System;
using System.Collections.Generic;
using System.IO;

namespace MultiGry.FilesEncryptor
{
    public class FileContentDisplay : IFileContentDisplay
    {       
        private IFakeFile File;
        private IFakeConsole DummyConsole;
        private MessageDisplay MessageDisplay;
        private string FilePath;
        private List<string> TextFromFile;
        private MemoryStream MemoryStream;

        public FileContentDisplay()
        {
            File = new FakeFile();
            DummyConsole = new FakeConsole();
            MessageDisplay = new MessageDisplay(DummyConsole);
            MemoryStream = null;
        }

        public FileContentDisplay(IFakeFile File, IFakeConsole DummyConsole, 
                                  MemoryStream MemoryStream)
        {
            this.File = File;
            this.DummyConsole = DummyConsole;
            MessageDisplay = new MessageDisplay(DummyConsole);
            this.MemoryStream = MemoryStream;
        }

        public void OpenFile()
        {
            RequestDisplay.DisplayRequestForFilePath();
            FilePath = GetterFilePath.GetFilePathFromUser(DummyConsole);

            if (File.Exists(FilePath))
                ReadContentsOfFile();

            else
                MessageDisplay.DisplayOnlyMessageAbout_FileDoesNotExist();
        }

        private void ReadContentsOfFile()
        {
            SetTextFromFile();
            DisplayOnlyFileContents();
            DummyConsole.ReadKey();
        }

        private void SetTextFromFile()
        {
            // if we test this class, MemoryStream will not be null, 
            // otherwise class will not be tested and then the field will be null:
            var ReaderFile = (MemoryStream == null) ? new ReaderFile(FilePath)
                                                    : new ReaderFile(MemoryStream);
            TextFromFile = ReaderFile.ReadData();
        }

        private void DisplayOnlyFileContents()
        {
            DummyConsole.Clear();
            Console.WriteLine("Zawartość pliku: ");
            foreach (var item in TextFromFile)
                Console.WriteLine(item);
        }

        public void ReadEncryptedFile()
        {
            RequestDisplay.DisplayRequestForFilePath();
            FilePath = GetterFilePath.GetFilePathFromUser(DummyConsole);

            if (File.Exists(FilePath))
                ReadEncryptedContent();

            else
                MessageDisplay.DisplayOnlyMessageAbout_FileDoesNotExist();
        }

        private void ReadEncryptedContent()
        {
            DummyConsole.Clear();
            SetTextFromFile();
            DisplayDecryptedContent();
            DummyConsole.ReadKey();
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
