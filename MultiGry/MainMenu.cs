using System;
using System.Collections.Generic;

namespace MultiGry
{
    class MainMenu
    {
        private int OptionNumber;
        private List<IMenuOption> MenuOptions;

        public MainMenu(List<IMenuOption> MenuOptions) =>
            this.MenuOptions = MenuOptions;

        /// <exception cref = "InvalidOperationException"> 
        /// when the number of menu options is greater than 9 
        /// </exception>
        public void ExecutingMainMenuOperation()
        {
            if (MenuOptions.Count > 9)
                throw new InvalidOperationException("Zbyt duża ilość opcji!");

            EventLoop();
        }

        private void EventLoop()
        {
            OptionsCategory CategoryOfOptionSelected = 0;

            while (CategoryOfOptionSelected != OptionsCategory.ExitTheProgram)
            {
                Console.Clear();
                DisplayingMenu();
                CategoryOfOptionSelected = SelectingOption();
            }
        }

        private void DisplayingMenu()
        {
            Console.WriteLine("Wybierz jedną z poniższych gier/aplikacji " +
                              "naciskają odpowiedni klawisz:");

            for (int i = 0; i < MenuOptions.Count; ++i)
                Console.WriteLine((i + 1) + ". " + MenuOptions[i].NameOption);
        }

        private OptionsCategory SelectingOption()
        {
            try
            {
                return TryToSelectRightOption();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Naciśnięto niewłaściwy klawisz! (dostępne" +
                                  " są tylko 1-" + MenuOptions.Count + ")");

                return OptionsCategory.Wrong;
            }
        }

        private OptionsCategory TryToSelectRightOption()
        {
            GetOptionNumber();

            if (CheckSelectedOption())
            {
                Console.Clear();
                return RunningSelectedOption();
            }

            else
                throw new InvalidOperationException();
        }

        private void GetOptionNumber() =>
            OptionNumber = Console.ReadKey().Key - ConsoleKey.D0;

        private bool CheckSelectedOption() =>
            OptionNumber >= 1 && OptionNumber <= MenuOptions.Count;

        /// <return> the last element of the "MenuOptions" list should have
        /// the dynamic type "ExitOption" and its method "OptionExecuting" returns 
        /// "OptionsCategory.ExitTheProgram" or "OptionsCategory.CanceledExit". 
        /// For other dynamic types, "OptionsCategory.NormalOption" should be 
        /// returned, although it is possible that this method may also return 
        /// other values, for example, if the option provides the option to exit
        /// the program then it may return "OptionsCategory.ExitTheProgram"
        /// </return>
        private OptionsCategory RunningSelectedOption() =>
            MenuOptions[OptionNumber - 1].OptionExecuting();
    }
}
