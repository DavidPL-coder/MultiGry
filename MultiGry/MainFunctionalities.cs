using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    interface IMenuOption
    {
        void OptionExecuting();
        string NameOption { get; }
    }

    class DisplayOfTheMainApplicationMenu
    {
        public void DisplayingTheMenu(List<IMenuOption> options)
        {
            Console.WriteLine("Wybierz jedną z poniższych gier/aplikacji naciskają odpowiedni klawisz:");

            for (int i = 0; i < options.Count - 1; ++i)
                Console.WriteLine(i + ". " + options[i].NameOption);

            Console.WriteLine("2. Wyjście z programu");
        }
    }

    class MainMenu
    {
        private DisplayOfTheMainApplicationMenu MenuDisplay;
        private enum OptionsCategory
        {
            NotSelectedYet, Game, Wrong, ExitTheProgram
        }
        private int OptionNumber;
        private List<IMenuOption> MenuOptions;

        public MainMenu()
        {
            MenuOptions = new List<IMenuOption>();
            MenuDisplay = new DisplayOfTheMainApplicationMenu();
        }

        public void ExecutingTheMainMenuOperation()
        {
            MenuDisplay.DisplayingTheMenu(MenuOptions);
            OptionsCategory CategoryOfOptionSelected = 0;

            while (CategoryOfOptionSelected != OptionsCategory.ExitTheProgram)
                CategoryOfOptionSelected = SelectingOption();
        }

        private OptionsCategory SelectingOption()
        {
            try
            {
                return TryToSelectTheRightOption();
            }
            catch (InvalidOperationException InvalidKeyException)
            {
                Console.WriteLine(InvalidKeyException.Message);
                return OptionsCategory.Wrong;
            }
        }

        private OptionsCategory TryToSelectTheRightOption()
        {
            OptionNumber = GettingTheKeySelectingByTheUser() - ConsoleKey.D0;

            if (DidTheUserChooseToExitTheProgram())
                return OptionsCategory.ExitTheProgram;

            else if (IsTheNumberOfTheSelectedOptionAppropriate())
                return RunningTheSelectedGame();

            else
                throw new InvalidOperationException("Naciśnięto niewłaściwy klawisz! (dostępne są tylko 1-" + MenuOptions.Count + ")");
        }

        private ConsoleKey GettingTheKeySelectingByTheUser() =>
            Console.ReadKey().Key;

        private bool IsTheNumberOfTheSelectedOptionAppropriate() =>
            OptionNumber >= 1 && OptionNumber <= MenuOptions.Count;

        private OptionsCategory RunningTheSelectedGame()
        {
            MenuOptions[OptionNumber - 1].OptionExecuting();
            return OptionsCategory.Game;
        }

        private bool DidTheUserChooseToExitTheProgram() =>
            OptionNumber == MenuOptions.Count;
    }
}
