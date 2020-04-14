using System;
using System.Collections.Generic;

namespace MultiGry
{
    class Program
    {
        static List<IMenuOption> GetListOfMenuOption()
        {
            var options = new List<IMenuOption>
            {
                new PaperRockScissors.PaperRockScissorsGame(),
                new GuessingNumbers.GuessingNumbersGame(),
                new GuessingPIN.GuessingPIN_Game(),
                new BinaryClock.BinaryClockOption(),
                new Hangman.HangmanGame(),
                new TicTacToe.TicTacToeGame(),
                new FilesEncryptor.FilesEncryptorOption(),
                new Minesweeper.MinesweeperGame(),
                new Exit.ExitOption()
            };

            return options;
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            var MenuOptions = GetListOfMenuOption();
            var Menu = new MainMenu(MenuOptions);
            Menu.ExecutingMainMenuOperation();
        }
    }
}
