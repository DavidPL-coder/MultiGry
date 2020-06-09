using System;

namespace MultiGry.Exit
{
    // using an artificial console makes it easier to test the class
    public class ExitOption : IMenuOption
    {
        public string NameOption => "Wyjście z programu";
        private IFakeConsole DummyConsole;

        public ExitOption() => 
            DummyConsole = new FakeConsole();

        public ExitOption(IFakeConsole DummyConsole) => 
            this.DummyConsole = DummyConsole;

        public OptionsCategory OptionExecuting()
        {
            Console.WriteLine("Czy napewno chcesz wyjść z programu?");
            Console.WriteLine("(naciśnij enter aby wyjść, bądź inny klawisz" +
                              " aby anulować)");

            if (DummyConsole.ReadKey().Key == ConsoleKey.Enter)
                return OptionsCategory.ExitTheProgram;

            else
                return OptionsCategory.CanceledExit;
        }
    }
}
