using System.Diagnostics;

namespace MultiGry.FilesEncryptor
{
    class SurrogateProcess : IProcess
    {
        public Process Start(ProcessStartInfo Info) => 
            Process.Start(Info);
    }
}
