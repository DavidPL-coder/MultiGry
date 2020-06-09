using System;

namespace MultiGry.FilesEncryptor
{
    public class MessageDisplay
    {
        private IFakeConsole DummyConsole;

        public MessageDisplay() => 
            DummyConsole = new FakeConsole();

        public MessageDisplay(IFakeConsole DummyConsole) => 
            this.DummyConsole = DummyConsole;

        public void DisplayOnlyTheMessage(string Message)
        {
            DummyConsole.Clear();
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        public void DisplayOnlyMessageAbout_FileDoesNotExist()
        {
            var Message = "Podany plik nie istnieje!" +
                          " Upewnij się czy wprowadziłeś odpowiednią ścieżkę!";

            DisplayOnlyTheMessage(Message);
        }
    }
}
