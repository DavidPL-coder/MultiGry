using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiGry.FilesEncryptor
{
    public class EncoderFile : IEncoderFile
    {
        private IFakeConsole DummyConsole;
        private IFakeFile File;
        private MessageDisplay MessageDisplay;
        private MemoryStream StreamToRead;
        private MemoryStream StreamToWrite;
        private EncoderOperations Operation;
        private string FilePath;

        public EncoderFile()
        {
            DummyConsole = new FakeConsole();
            File = new FakeFile();
            MessageDisplay = new MessageDisplay(DummyConsole);
            StreamToRead = null;
            StreamToWrite = null;
        }

        public EncoderFile(IFakeConsole DummyConsole, IFakeFile File, 
                           MemoryStream StreamToRead, MemoryStream StreamToWrite)
        {            
            this.DummyConsole = DummyConsole;
            this.File = File;
            MessageDisplay = new MessageDisplay(DummyConsole);
            this.StreamToRead = StreamToRead;
            this.StreamToWrite = StreamToWrite;
        }

        public void FileEncryption()
        {
            Operation = EncoderOperations.Encryption;
            FileEncoding();
        }

        public void FileDecryption()
        {
            Operation = EncoderOperations.Decryption;
            FileEncoding();
        }

        private void FileEncoding()
        {
            RequestDisplay.DisplayRequestForFilePath();
            FilePath = GetterFilePath.GetFilePathFromUser(DummyConsole);

            if (File.Exists(FilePath))
                FileContentEncoding();

            else
                MessageDisplay.DisplayOnlyMessageAbout_FileDoesNotExist();
        }

        private void FileContentEncoding()
        {
            // if we test this class, MemoryStream will not be null, 
            // otherwise class will not be tested and then the field will be null:
            var Reader = (StreamToRead == null) ? new ReaderFile(FilePath)
                                                : new ReaderFile(StreamToRead);
            var TextFromFile = Reader.ReadData();

            EncodeFile(TextFromFile);
            DisplayingMessageAboutEncodingCompleted();
        }

        private void EncodeFile(List<string> TextFromFile)
        {            
            using (var Writer = GetStreamWriter())
                foreach (var item in TextFromFile)
                {
                    string TextToSaveToFile = EncodeText(item);
                    Writer.WriteLine(TextToSaveToFile);
                }
        }

        private StreamWriter GetStreamWriter()
        {
            if (StreamToWrite == null)
                return new StreamWriter(FilePath, false,
                                        Encoding.GetEncoding("Windows-1250"));

            else
                return new StreamWriter(StreamToWrite,
                                        Encoding.GetEncoding("Windows-1250"));
        }

        private string EncodeText(string Text)
        {
            var TextEncoder = new TextEncoder();
            return Operation == EncoderOperations.Encryption 
                                ? TextEncoder.EncryptingText(Text)
                                : TextEncoder.DecryptingText(Text);
        }

        private void DisplayingMessageAboutEncodingCompleted()
        {
            if (Operation == EncoderOperations.Encryption)
                Console.WriteLine("Zaszyfrowano plik!");

            else
                Console.WriteLine("Odkodowano plik!");

            System.Threading.Thread.Sleep(1500);
        }
    }
}
