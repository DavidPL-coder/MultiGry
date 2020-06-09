using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MultiGry.FilesEncryptor
{
    public class ReaderFile
    {
        private StreamReader StreamReader;

        public ReaderFile(string Path)
        {
            StreamReader = new StreamReader(Path, 
                                            Encoding.GetEncoding("Windows-1250"));
        }

        public ReaderFile(MemoryStream FakeFileContent)
        {            
            StreamReader = new StreamReader(FakeFileContent, 
                                            Encoding.GetEncoding("Windows-1250"));
        }

        /// <returns> the method returns the text from the file as a list. 
        /// Each element of the list is one line from the file </returns>
        /// <exception cref = "ArgumentException"> when Path is empty </exception>
        /// <exception cref = "ArgumentNullException"> when Path is null </exception>
        /// <exception cref = "FileNotFoundException"> 
        /// when file doesn't exist 
        /// </exception>
        /// <exception cref = "DirectoryNotFoundException"> 
        /// when Path to file doesn't exist
        /// </exception>
        /// <exception cref = "NotSupportedException">
        /// when the path is in the wrong format
        /// </exception>
        public List<string> ReadData()
        {   
            var TextFromFile = new List<string>();

            using (StreamReader)
            {
                string tmp;
                while ((tmp = StreamReader.ReadLine()) != null)
                    TextFromFile.Add(tmp); 
            }

            return TextFromFile;
        }
    }
}
