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
        private List<IMenuOption> MenuOptions;

        public void SelectingOption()
        {
            ConsoleKey KeySelectedByTheUser = GettingTheKeySelectingByTheUser();
            int OptionNumber = KeySelectedByTheUser - ConsoleKey.D0;

            if (OptionNumber >= 1 && OptionNumber <= MenuOptions.Count)
                MenuOptions[OptionNumber].GameExecuting();

            else
                throw new InvalidOperationException("Naciśnięto niewłaściwy klawisz! (dostępne są tylko 1-9)");
        }

        private ConsoleKey GettingTheKeySelectingByTheUser() =>
            Console.ReadKey().Key;
    }
}
