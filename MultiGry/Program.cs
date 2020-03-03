using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class Program
    {
        static List<IMenuOption> GetListOfMenuOption()
        {
            var options = new List<IMenuOption>();

            options.Add(new PaperRockScissorsGame());
            options.Add(new GuessingNumbersGame());
            options.Add(new GuessingPIN_Game());
            options.Add(new BinaryClockOption());
            options.Add(new HangmanGame());
            options.Add(new TicTacToeGame());
            options.Add(new FilesEncryptionOption());
            options.Add(new MinesweeperGame());
            options.Add(new ExitOption());

            return options;
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            MainMenu Menu = new MainMenu(GetListOfMenuOption());
            Menu.ExecutingMainMenuOperation();
        }
    }
}
