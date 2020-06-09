using System;

namespace MultiGry
{
    public interface IFakeConsole
    {
        bool KeyAvailable();
        ConsoleKeyInfo ReadKey();
        ConsoleKeyInfo ReadKey(bool intercept);
        string ReadLine();
        void Clear();
    }
}
