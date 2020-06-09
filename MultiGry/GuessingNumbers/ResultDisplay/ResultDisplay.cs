using System;

namespace MultiGry.GuessingNumbers
{
    public class ResultDisplay : IResultDisplay
    {
        private IFakeConsole DummyConsole;

        public ResultDisplay(IFakeConsole DummyConsole) => 
            this.DummyConsole = DummyConsole;

        public void DisplayOnlyResult(int UserAttempt)
        {
            DummyConsole.Clear();
            Console.WriteLine("Odgadłeś tę liczbę w próbie " + UserAttempt);
            DummyConsole.ReadKey();
        }
    }
}
