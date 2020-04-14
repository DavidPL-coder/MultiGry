using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiGry.FilesEncryptor
{
    class EncoderFile
    {
        private EncoderOperations Operation;
        private FilesEncryptorOption Option;
        private string FilePath;
        private List<string> TextFromFile;

        public EncoderFile(FilesEncryptorOption Option) =>
            this.Option = Option;

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
            Option.DisplayRequestForFilePath();
            FilePath = Option.GetFilePathFromUser();

            if (File.Exists(FilePath))
                FileContentEncoding();

            else
                Option.DisplayOnlyMessageAbout_FileDoesNotExist();
        }

        private void FileContentEncoding()
        {
            var ReaderFile = new ReaderFile();
            TextFromFile = ReaderFile.ReadDataFromFile(FilePath);

            EncodeFile();
            DisplayingMessageAboutEncodingCompleted();
        }

        private void EncodeFile()
        {            
            using (var StreamWriter = GetStreamWriter())
                foreach (var item in TextFromFile)
                {
                    string TextToSaveToFile = EncodeText(item);
                    StreamWriter.WriteLine(TextToSaveToFile);
                }
        }

        private StreamWriter GetStreamWriter() => 
            new StreamWriter(FilePath, false, Encoding.GetEncoding("Windows-1250"));

        private string EncodeText(string item)
        {
            var TextEncoder = new TextEncoder();
            return Operation == EncoderOperations.Encryption 
                              ? TextEncoder.EncryptingText(item)
                              : TextEncoder.DecryptingText(item);
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
