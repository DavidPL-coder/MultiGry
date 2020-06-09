using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry.FilesEncryptor
{
    class FakeFile : IFakeFile
    {
        public StreamWriter CreateText(string path) => 
            File.CreateText(path);

        public bool Exists(string path) => 
            File.Exists(path);
    }
}
