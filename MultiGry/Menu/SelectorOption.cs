using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry.Menu
{
    public class SelectorOption : ISelectorOption
    {
        private int OptionNumber;
        private List<IMenuOption> MenuOptions;
        private IFakeConsole DummyConsole;

        public SelectorOption(List<IMenuOption> Options)
        {
            MenuOptions = Options;
            DummyConsole = new FakeConsole();
        }

        public SelectorOption(List<IMenuOption> Options, IFakeConsole DummyConsole)
        {
            MenuOptions = Options;
            this.DummyConsole = DummyConsole;
        }

        public OptionsCategory SelectingOption()
        {
            try
            {
                return TryToSelectRightOption();
            }
            catch (InvalidOperationException)
            {
                return OptionsCategory.Wrong;
            }
        }

        private OptionsCategory TryToSelectRightOption()
        {
            GetOptionNumber();

            if (CheckSelectedOption())
            {
                DummyConsole.Clear();
                return RunningSelectedOption();
            }

            else
                throw new InvalidOperationException();
        }

        private void GetOptionNumber() =>
            OptionNumber = DummyConsole.ReadKey().Key - ConsoleKey.D0;

        private bool CheckSelectedOption() =>
            OptionNumber >= 1 && OptionNumber <= MenuOptions.Count;

        /// <return> the last element of the "MenuOptions" list should have
        /// the dynamic type "ExitOption" and its method "OptionExecuting" returns 
        /// "OptionsCategory.ExitTheProgram" or "OptionsCategory.CanceledExit". 
        /// For other dynamic types, "OptionsCategory.NormalOption" should be 
        /// returned, although it is possible that this method may also return 
        /// other values, for example, if the option provides the option to exit
        /// the method then it may return "OptionsCategory.ExitTheProgram"
        /// </return>
        private OptionsCategory RunningSelectedOption() =>
            MenuOptions[OptionNumber - 1].OptionExecuting();
    }
}
