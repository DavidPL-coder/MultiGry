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
        }
    }

    interface IMenuOption
    {
        void GameExecuting();
    }

    class OptionSelector
    {
        private enum OptionsCategory
        {
            Game, Wrong, ExitTheProgram
        }

        private int OptionNumber;
        private List<IMenuOption> MenuOptions;
        
        public void SimulationOfMainMenuOperation()
        {
            do
            {
                if (TryToSelectTheRightOption() == OptionsCategory.ExitTheProgram)
                    return;
            }
            while (true);
        }

        private OptionsCategory TryToSelectTheRightOption()
        {
            try
            {
               return SelectingOption();
            }
            catch (InvalidOperationException exception)
            {
                Console.WriteLine(exception.Message);
                return OptionsCategory.Wrong;
            }
        }

        private OptionsCategory SelectingOption()
        {
            OptionNumber = GettingTheKeySelectingByTheUser() - ConsoleKey.D0;

            if (IsTheNumberOfTheSelectedOptionAppropriate())
                MenuOptions[OptionNumber - 1].GameExecuting();

            else if (OptionNumber == MenuOptions.Count)
                return OptionsCategory.ExitTheProgram;

            else
                throw new InvalidOperationException("Naciśnięto niewłaściwy klawisz! (dostępne są tylko 1-" + MenuOptions.Count + ")");

            return OptionsCategory.Game;
        }

        private ConsoleKey GettingTheKeySelectingByTheUser() =>
            Console.ReadKey().Key;

        private bool IsTheNumberOfTheSelectedOptionAppropriate() =>
            OptionNumber >= 1 && OptionNumber <= MenuOptions.Count;
    }
}
