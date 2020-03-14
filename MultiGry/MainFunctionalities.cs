using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    enum OptionsCategory
    {
        NotSelectedYet, NormalOption, Wrong, ExitTheProgram, CanceledExit
    }

    interface IMenuOption
    {
        OptionsCategory OptionExecuting();
        string NameOption { get; }
    }

    class MainMenu
    {
        private int OptionNumber;
        private List<IMenuOption> MenuOptions;

        public MainMenu(List<IMenuOption> menuOptions) =>
            MenuOptions = menuOptions;

        public void ExecutingMainMenuOperation()
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
            Console.WriteLine("Wybierz jedną z poniższych gier/aplikacji naciskają odpowiedni klawisz:");

            for (int i = 1; i <= MenuOptions.Count; ++i)
                Console.WriteLine(i + ". " + MenuOptions[i - 1].NameOption);
        }

        private OptionsCategory SelectingOption()
        {
            try
            {
                return TryToSelectRightOption();
            }
            catch (InvalidOperationException InvalidKeyException)
            {
                Console.WriteLine(InvalidKeyException.Message);
                return OptionsCategory.Wrong;
            }
        }

        private OptionsCategory TryToSelectRightOption()
        {
            GetOptionNumber();

            if (ValidateSelectedOption())
            {
                Console.Clear();
                return RunningSelectedOption();
            }

            else
                throw new InvalidOperationException("Naciśnięto niewłaściwy klawisz! (dostępne są tylko 1-" + MenuOptions.Count + ")");
        }

        private void GetOptionNumber() =>
            OptionNumber = Console.ReadKey().Key - ConsoleKey.D0;

        private bool ValidateSelectedOption() =>
            OptionNumber >= 1 && OptionNumber <= MenuOptions.Count;

        /// <return> the last element of the "MenuOptions" list should have the dynamic type "ExitOption" and its "OptionExecuting" method returns 
        /// "OptionsCategory.ExitTheProgram" or "OptionsCategory.CanceledExit". For other dynamic types, "OptionsCategory.Game" should be returned, 
        /// although it is possible that this method may also return other values. </return>
        private OptionsCategory RunningSelectedOption() =>
            MenuOptions[OptionNumber - 1].OptionExecuting();
    }

    class ExitOption : IMenuOption
    {
        public string NameOption => "Wyjście z programu";

        public OptionsCategory OptionExecuting()
        {
            Console.WriteLine("Czy napewno chcesz wyjść z programu?");
            Console.WriteLine("(naciśnij enter aby wyjść, bądź inny klawisz aby anulować)");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
                return OptionsCategory.ExitTheProgram;

            else
                return OptionsCategory.CanceledExit;
        }
    }

    class DecisionOnFurtherCourseOfProgram
    {
        private IMenuOption Option;
        private ConsoleKey OptionChosenByUser;

        public DecisionOnFurtherCourseOfProgram(IMenuOption option) => 
            Option = option;

        public OptionsCategory UserDecidesWhatToDoNext()
        {
            DisplayOptions();
            OptionChosenByUser = Console.ReadKey().Key;
            Console.Clear();

            return ExecutingSpecificOperation();
        }

        private void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine("Co dalej chcesz robić?");
            Console.WriteLine("1. Zagrać jeszcze raz");
            Console.WriteLine("2. Powrócić do Menu");
            Console.WriteLine("3. Wyjść z programu");
        }

        private OptionsCategory ExecutingSpecificOperation()
        {
            switch (OptionChosenByUser)
            {
                case ConsoleKey.D1:
                    return Option.OptionExecuting();

                case ConsoleKey.D2:
                    return OptionsCategory.NormalOption;

                case ConsoleKey.D3:
                    var ExitFromProgram = new ExitOption();
                    return ExitFromProgram.OptionExecuting();

                default:
                    return UserDecidesWhatToDoNext();
            }
        } 
    }
}
