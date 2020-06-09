using System.IO;

namespace MultiGry.FilesEncryptor
{
    public interface IFakeFile
    {
        bool Exists(string path);
        StreamWriter CreateText(string path);
    }
}
