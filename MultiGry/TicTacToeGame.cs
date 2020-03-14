using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class TicTacToeGame : IMenuOption
    {
        public string NameOption => "Kółko i krzyżyk dla dwóch osób";
        private int TurnNumber;
        private enum PlayerType { Nobody, Circle, Sharp }
        private PlayerType PlayerTurn;
        private PlayerType Winner;
        private char[] Board;
        private char FieldNumberSelected;

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            while (IsGameStillGoingOn())
                PlayingOneTurn();

            DisplayResult();
            Console.ReadKey();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void SetDefaults()
        {
            TurnNumber = 1;
            DrawPlayer();
            Winner = PlayerType.Nobody;
            Board = new char[]
            {
                '1', '2', '3',
                '4', '5', '6',
                '7', '8', '9'
            };
        }

        private void DrawPlayer()
        {
            var Random = new Random();
            PlayerTurn = (PlayerType) Random.Next(1, 3);    // draws "Circle" or "Sharp"
        }

        private bool IsGameStillGoingOn() =>
            TurnNumber <= 9 && Winner == PlayerType.Nobody;

        private void PlayingOneTurn()
        {
            DisplayPlayersTurn();
            DisplayBoard();
            PlayerIsSelectingField();
            Winner = CheckingWhoWon();
            ++TurnNumber;
            Console.Clear();
        }

        private void DisplayPlayersTurn()
        {
            string Gamer = (PlayerTurn == PlayerType.Circle ? "kółko" : "krzyżyk");
            Console.WriteLine("Tura gracza: " + Gamer);
        }

        private void DisplayBoard()
        {
            Console.WriteLine("+---+---+---+");
            DisplayBoardFieldsInThisRange(0, 2);
            Console.WriteLine("+---+---+---+");
            DisplayBoardFieldsInThisRange(3, 5);
            Console.WriteLine("+---+---+---+");
            DisplayBoardFieldsInThisRange(6, 8);
            Console.WriteLine("+---+---+---+");
        }

        private void DisplayBoardFieldsInThisRange(int Begin, int End)
        {
            for (int i = Begin; i <= End; ++i)
            {
                Console.Write("| ");
                if (Board[i] == 'x')
                    Console.ForegroundColor = ConsoleColor.Blue;

                if (Board[i] == 'o')
                    Console.ForegroundColor = ConsoleColor.Green;

                Console.Write(Board[i] + " ");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("|");
        }

        private void PlayerIsSelectingField()
        {
            try
            {
                TryPlayerIsSelectingField();
            }
            catch (InvalidOperationException InvalidFieldNumberException)
            {
                Console.WriteLine(InvalidFieldNumberException.Message);
                System.Threading.Thread.Sleep(1500);
                --TurnNumber;
            }
        }

        private void TryPlayerIsSelectingField()
        {
            FieldNumberSelected = Console.ReadKey(true).KeyChar;

            if (IsGivenLetterNumberBetween1And9())
                FinalFieldSelectionProcessing();

            else
                throw new InvalidOperationException("Numerki pola są od 1 do 9!");
        }

        private bool IsGivenLetterNumberBetween1And9() =>
            char.IsDigit(FieldNumberSelected) && FieldNumberSelected != '0';

        private void FinalFieldSelectionProcessing()
        {
            int FieldIndex = (FieldNumberSelected - '0') - 1;

            if (IsntFieldBlank(FieldIndex))
                throw new InvalidOperationException("To pole było już wcześniej wybrane!");

            Board[FieldIndex] = (PlayerTurn == PlayerType.Circle ? 'o' : 'x');
            PlayerTurn = (PlayerTurn == PlayerType.Circle ? PlayerType.Sharp : PlayerType.Circle);
        }

        private bool IsntFieldBlank(int FieldIndex) =>
            Board[FieldIndex] == 'x' || Board[FieldIndex] == 'o';

        private PlayerType CheckingWhoWon()
        {
            PlayerType Horizontal = CheckingWhoWonHorizontally();
            PlayerType Vertical = CCheckingWhoWonVertically();
            PlayerType Diagonal = CheckingWhoWonDiagonally();

            if (Horizontal != 0)
                return Horizontal;

            if (Vertical != 0)
                return Vertical;

            if (Diagonal != 0)
                return Diagonal;

            return PlayerType.Nobody;
        }

        private PlayerType CheckingWhoWonHorizontally()
        {
            for (int i = 0; i <= 6; i += 3)
            {
                if (Board[i] == 'x' && Board[i + 1] == 'x' && Board[i + 2] == 'x')
                    return PlayerType.Sharp;

                if (Board[i] == 'o' && Board[i + 1] == 'o' && Board[i + 2] == 'o')
                    return PlayerType.Circle;
            }

            return PlayerType.Nobody;
        }

        private PlayerType CCheckingWhoWonVertically()
        {
            for (int i = 0; i < 3; ++i)
            {
                if (Board[i] == 'x' && Board[i + 3] == 'x' && Board[i + 6] == 'x')
                    return PlayerType.Sharp;

                if (Board[i] == 'o' && Board[i + 3] == 'o' && Board[i + 6] == 'o')
                    return PlayerType.Circle;
            }

            return PlayerType.Nobody;
        }

        private PlayerType CheckingWhoWonDiagonally()
        {
            if (( Board[0] == 'x' && Board[4] == 'x' && Board[8] == 'x' ) ||
                ( Board[2] == 'x' && Board[4] == 'x' && Board[6] == 'x' ))
                return PlayerType.Sharp;

            if (( Board[0] == 'o' && Board[4] == 'o' && Board[8] == 'o' ) ||
                ( Board[2] == 'o' && Board[4] == 'o' && Board[6] == 'o' ))
                return PlayerType.Circle;

            return PlayerType.Nobody;
        }

        private void DisplayResult()
        {
            if (Winner == PlayerType.Circle)
                Console.WriteLine("Wygrało kółko!");

            else if (Winner == PlayerType.Sharp)
                Console.WriteLine("Wygrał krzyżyk!");

            else
                Console.WriteLine("Remis!");

            DisplayBoard();
        }  
    }
}
