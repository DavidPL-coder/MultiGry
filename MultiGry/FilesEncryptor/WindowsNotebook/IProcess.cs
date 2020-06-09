using System.Diagnostics;

namespace MultiGry.FilesEncryptor
{
    public interface IProcess
    {
        Process Start(ProcessStartInfo Info);
    }
}
