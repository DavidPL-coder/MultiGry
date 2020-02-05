using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class DisplayOfTheMainApplicationMenu
    {
        public void DisplayingTheMenu()
        {
            Console.WriteLine("Wybierz jedną z poniższych gier/aplikacji naciskają odpowiedni klawisz:");
            Console.WriteLine("1. Papier, kamień, nożyce");
            Console.WriteLine("2. Wyjście z programu");
        }
    }

    interface IMenuOption
    {
        void GameExecuting();
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
            MenuDisplay = new DisplayOfTheMainApplicationMenu();
        }

        public void ExecutingTheMainMenuOperation()
        {
            MenuDisplay.DisplayingTheMenu();
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
            MenuOptions[OptionNumber - 1].GameExecuting();
            return OptionsCategory.Game;
        }

        private bool DidTheUserChooseToExitTheProgram() =>
            OptionNumber == MenuOptions.Count;
    }
}
