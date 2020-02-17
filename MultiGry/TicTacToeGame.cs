using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class TicTacToeGame : IMenuOption
    {
        public string NameOption => "Kółko i krzyżyk";
        private enum GamerTurn { Circle = 1, Sharp }
        private GamerTurn PlayerTurn;
        private char[] Board =
        {
            '1', '2', '3',
            '4', '5', '6',
            '7', '8', '9'
        };
        private int TurnNumber;

        public OptionsCategory OptionExecuting()
        {
            TurnNumber = 1;
            DrawPlayer();

            while (TurnNumber <= 9) // for tests, the loop is done 9 times!
            {
                DisplayPlayersTurn();
                DisplayBoard();
                PlayerIsSelectingField();
                ++TurnNumber;
                Console.Clear();
            }

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void PlayerIsSelectingField()
        {
            char FieldNumberSelected = Console.ReadKey(true).KeyChar;

            if (char.IsDigit(FieldNumberSelected) && FieldNumberSelected != '0')
            {
                int Index = (FieldNumberSelected - '0') - 1;
                Board[Index] = (PlayerTurn == GamerTurn.Circle ? 'o' : 'x');
                PlayerTurn = (PlayerTurn == GamerTurn.Circle ? GamerTurn.Sharp : GamerTurn.Circle);
            }

            else
            {
                Console.WriteLine("Numerki pola są od 1 do 9!");
                System.Threading.Thread.Sleep(1500);
            }
        }

        private void DrawPlayer()
        {
            var Random = new Random();
            PlayerTurn = (GamerTurn) Random.Next(1, 3);
        }

        private void DisplayPlayersTurn()
        {
            string Gamer = (PlayerTurn == GamerTurn.Circle ? "kółko" : "krzyżyk");
            Console.WriteLine("Tura gracza: " + Gamer);
        }

        private void DisplayBoard()
        {
            Console.WriteLine("+---+---+---+");
            Console.WriteLine("| {0} | {1} | {2} |", Board[0], Board[1], Board[2]);
            Console.WriteLine("+---+---+---+");
            Console.WriteLine("| {0} | {1} | {2} |", Board[3], Board[4], Board[5]);
            Console.WriteLine("+---+---+---+");
            Console.WriteLine("| {0} | {1} | {2} |", Board[6], Board[7], Board[8]);
            Console.WriteLine("+---+---+---+");
        }
    }
}
