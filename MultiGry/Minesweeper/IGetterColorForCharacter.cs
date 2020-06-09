using System;

namespace MultiGry.Minesweeper
{
    public interface IGetterColorForCharacter
    {
        ConsoleColor GetColorForCharacter(int i, int j);
    }
}
