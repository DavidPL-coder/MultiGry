using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiGry.Menu
{
    public class MainMenu
    {
        private List<IMenuOption> Options;
        private IMenuDisplay MenuDisplay;
        private IFakeConsole DummyConsole;
        private ISelectorOption SelectorOption;

        public MainMenu(List<IMenuOption> Options)
        {
            this.Options = Options;
            MenuDisplay = new MenuDisplay(Options);
            DummyConsole = new FakeConsole();
            SelectorOption = new SelectorOption(Options, DummyConsole);
        }

        public MainMenu(List<IMenuOption> Options, IMenuDisplay MenuDisplay,
                        IFakeConsole DummyConsole, ISelectorOption SelectorOption)
        {
            this.Options = Options;
            this.MenuDisplay = MenuDisplay;
            this.DummyConsole = DummyConsole;
            this.SelectorOption = SelectorOption;
        }

        /// <exception cref = "InvalidOperationException"> 
        /// when the number of menu options is greater than 9 or less than 1, 
        /// or when the last item is not of type Exit.ExitOption
        /// </exception>
        public void ExecutingMenuOperation()
        {
            if (Options?.Count < 1 || Options?.Count > 9)
                throw new InvalidOperationException("Nieodpowiednia ilość opcji!");

            if (Options?.Last()?.GetType() != typeof(Exit.ExitOption))
                throw new InvalidOperationException("Ostatnia opcja nie jest opcją wyjścia");

            EventLoop();
        }

        private void EventLoop()
        {
            OptionsCategory CategoryOfOptionSelected = 0;

            while (CategoryOfOptionSelected != OptionsCategory.ExitTheProgram)
            {
                DummyConsole.Clear();
                MenuDisplay.DisplayingMenu();
                CategoryOfOptionSelected = SelectorOption.SelectingOption();
            }
        }
    }
}
