using System;

namespace MultiGry
{
    public class DecisionOnFurtherCourseOfProgram : IDecisionOnFurtherCourseOfProgram
    {
        private IMenuOption Option;
        private IFakeConsole DummyConsole;
        private IMenuOption ExitFromProgram;
        private ConsoleKey OptionChosenByUser;

        public DecisionOnFurtherCourseOfProgram(IMenuOption Option)
        {
            this.Option = Option;
            DummyConsole = new FakeConsole();
            ExitFromProgram = new Exit.ExitOption(DummyConsole);
        }

        public DecisionOnFurtherCourseOfProgram(IMenuOption Option, 
                                                IFakeConsole DummyConsole, 
                                                IMenuOption Exit)
        {
            this.Option = Option;
            this.DummyConsole = DummyConsole;
            ExitFromProgram = Exit;
        }

        public OptionsCategory UserDecidesWhatToDoNext()
        {
            DisplayOnlyOptions();
            OptionChosenByUser = DummyConsole.ReadKey().Key;
            DummyConsole.Clear();

            return ExecutingSpecificOperation();
        }

        private void DisplayOnlyOptions()
        {
            DummyConsole.Clear();
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
                    return ExitFromProgram.OptionExecuting();

                default:
                    return UserDecidesWhatToDoNext();
            }
        } 
    }
}
