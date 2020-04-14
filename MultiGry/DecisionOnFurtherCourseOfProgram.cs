using System;

namespace MultiGry
{
    class DecisionOnFurtherCourseOfProgram
    {
        private IMenuOption Option;
        private ConsoleKey OptionChosenByUser;

        public DecisionOnFurtherCourseOfProgram(IMenuOption option) => 
            Option = option;

        public OptionsCategory UserDecidesWhatToDoNext()
        {
            DisplayOnlyOptions();
            OptionChosenByUser = Console.ReadKey().Key;
            Console.Clear();

            return ExecutingSpecificOperation();
        }

        private void DisplayOnlyOptions()
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
                // if the user selected the 1 key then the option is re-executed
                case ConsoleKey.D1:
                    return Option.OptionExecuting();

               // if the user selected the 2 key, the program exits the option 
               // and it will be redirected to the main menu
                case ConsoleKey.D2:
                    return OptionsCategory.NormalOption;

                case ConsoleKey.D3:
                    var ExitFromProgram = new Exit.ExitOption();
                    return ExitFromProgram.OptionExecuting();

                default:
                    return UserDecidesWhatToDoNext();
            }
        } 
    }
}
