using System;

namespace MultiGry.BinaryClock
{
    public class BinaryClockOption : IMenuOption
    {
        public string NameOption => "Zegar binarny";
        private IFakeConsole DummyConsole;
        private TimeDisplay TimeDisplay;

        public BinaryClockOption()
        {
            DummyConsole = new FakeConsole();
            TimeDisplay = new TimeDisplay();
        }

        public BinaryClockOption(IFakeConsole DummyConsole, TimeDisplay TimeDisplay)
        {
            this.DummyConsole = DummyConsole;
            this.TimeDisplay = TimeDisplay;
        }

        public OptionsCategory OptionExecuting()
        {
            while (!DummyConsole.KeyAvailable())
            {
                TimeDisplay.DisplayCurrentTime();
                System.Threading.Thread.Sleep(1000);
                DummyConsole.Clear();
            }

            return OptionsCategory.NormalOption;
        }
    }
}
