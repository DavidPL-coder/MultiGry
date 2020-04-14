using System;

namespace MultiGry.Exit
{
    class ExitOption : IMenuOption
    {
        public string NameOption => "Wyjście z programu";

        public OptionsCategory OptionExecuting()
        {
            Console.WriteLine("Czy napewno chcesz wyjść z programu?");
            Console.WriteLine("(naciśnij enter aby wyjść, bądź inny klawisz" +
                              " aby anulować)");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
                return OptionsCategory.ExitTheProgram;

            else
                return OptionsCategory.CanceledExit;
        }
    }
}
