using System.Diagnostics;

namespace MultiGry.FilesEncryptor
{
    public class FakeProcess : IProcess
    {
        public string ExpectedFileName { get; set; }
        public string ExpectedArguments { get; set; }
        public bool ExpectedUseShellExecute { get; set; }
        public bool ExpectedRedirectStandardOutput { get; set; }

        public bool AreAllDataCorrect_AfterCallingStartMethod { get; private set; }

        public FakeProcess() => 
            AreAllDataCorrect_AfterCallingStartMethod = false;

        public Process Start(ProcessStartInfo Info)
        {
            if (ExpectedFileName == Info.FileName &&
                ExpectedArguments == Info.Arguments &&
                ExpectedUseShellExecute == Info.UseShellExecute &&
                ExpectedRedirectStandardOutput == Info.RedirectStandardOutput)
                AreAllDataCorrect_AfterCallingStartMethod = true;
                
            return null;
        }
    }
}
