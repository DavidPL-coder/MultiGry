using System;

namespace MultiGry.TicTacToe
{
    class GameInterfaceDisplay
    {
        private const string HorizontalLine = "+---+---+---+";
        private const char VerticalLine = '|';
        private PlayerType PlayerTurn;
        private char[] Board;

        public GameInterfaceDisplay(PerformerRounds PerformerRounds)
        {
            PlayerTurn = PerformerRounds.PlayerTurn;
            Board = PerformerRounds.Board;
        }

        public void DisplayInterface()
        {
            DisplayPlayersTurn();
            DisplayBoard();
        }

        private void DisplayPlayersTurn()
        {
            string Gamer = (PlayerTurn == PlayerType.Circle ? "kółko" : "krzyżyk");
            Console.WriteLine("Tura gracza: " + Gamer);
        }

        public void DisplayBoard()
        {
            Console.WriteLine(HorizontalLine);
            DisplayBoardFieldsInThisRange(0, 2);

            Console.WriteLine(HorizontalLine);
            DisplayBoardFieldsInThisRange(3, 5);

            Console.WriteLine(HorizontalLine);
            DisplayBoardFieldsInThisRange(6, 8);

            Console.WriteLine(HorizontalLine);
        }

        private void DisplayBoardFieldsInThisRange(int Begin, int End)
        {
            for (int i = Begin; i <= End; ++i)
            {
                Console.Write(VerticalLine + " ");
                ChangeTextColorForGivenBoardElement(i);
                Console.Write(Board[i] + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine(VerticalLine);
        }

        private void ChangeTextColorForGivenBoardElement(int i)
        {
            if (Board[i] == TicTacToeGame.SharpSign)
                Console.ForegroundColor = ConsoleColor.Blue;

            if (Board[i] == TicTacToeGame.CircleSign)
                Console.ForegroundColor = ConsoleColor.Green;
        }
    }
}
