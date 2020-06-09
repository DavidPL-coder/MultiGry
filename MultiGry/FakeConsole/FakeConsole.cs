using System;

namespace MultiGry
{
    // FakeConsole is used instead of System.Console when needed
    // Console.ReadKey, ReadLine, Clear methods or Console.KeyAvailable field  
    // and you must test the code at the same time.
    public class FakeConsole : IFakeConsole
    {
        public bool KeyAvailable() => 
            Console.KeyAvailable;

        public ConsoleKeyInfo ReadKey() => 
            Console.ReadKey();

        public ConsoleKeyInfo ReadKey(bool intercept) => 
            Console.ReadKey(intercept);

        public string ReadLine() => 
            Console.ReadLine();

        public void Clear() =>
            Console.Clear();       
    }
}
