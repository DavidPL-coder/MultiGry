using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiGry.FilesEncryptor
{
    class ReaderFile
    {
        private List<string> TextFromFile;
        private readonly Encoding EncodingConsole;

        public ReaderFile()
        {
            EncodingConsole = Encoding.GetEncoding("Windows-1250");
            TextFromFile = new List<string>();
        }

        /// <returns> the method returns the text from the file as a list. 
        /// Each element of the list is one line from the file </returns>
        /// <exception cref = "ArgumentException"> when Path is empty </exception>
        /// <exception cref = "ArgumentNullException"></exception>
        /// <exception cref = "FileNotFoundException"> 
        /// when file doesn't exist 
        /// </exception>
        /// <exception cref = "DirectoryNotFoundException"> 
        /// when Path to file doesn't exist
        /// </exception>
        /// <exception cref = "NotSupportedException">
        /// when the path is in the wrong format
        /// </exception>
        public List<string> ReadDataFromFile(string Path)
        {
            using (var streamReader = new StreamReader(Path, EncodingConsole))
            {
                string tmp;
                while ((tmp = streamReader.ReadLine()) != null)
                    TextFromFile.Add(tmp);
            }

            return TextFromFile;
        }
    }
}
