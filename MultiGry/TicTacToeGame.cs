﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class TicTacToeGame : IMenuOption
    {
        public string NameOption => "Kółko i krzyżyk";
        private enum PlayerType { Nobody, Circle, Sharp }
        private PlayerType PlayerTurn;
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

            /// TODO: the program displays a "Remis!" before starting the game. You have to fix it!

            PlayerType Winner = 0;
            while (TurnNumber <= 9 && Winner != PlayerType.Nobody) // for tests, the loop is done 9 times!
            {
                DisplayPlayersTurn();
                DisplayBoard();
                PlayerIsSelectingField();
                Winner = CheckingWhoWon();
                ++TurnNumber;
                Console.Clear();
            }

            if (Winner == PlayerType.Circle)
                Console.WriteLine("Wygrało kółko!");

            if (Winner == PlayerType.Sharp)
                Console.WriteLine("Wygrał krzyżyk!");

            if (Winner == 0)
                Console.WriteLine("Remis!");

            Console.ReadKey();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

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

        private void DrawPlayer()
        {
            var Random = new Random();
            PlayerTurn = (PlayerType) Random.Next(1, 3);
        }

        private void DisplayPlayersTurn()
        {
            string Gamer = ( PlayerTurn == PlayerType.Circle ? "kółko" : "krzyżyk" );
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
            char FieldNumberSelected = Console.ReadKey(true).KeyChar;

            if (IsGivenLetterNumberBetween1And9(FieldNumberSelected))
            {
                int FieldIndex = ( FieldNumberSelected - '0' ) - 1;
                if (IsntFieldBlank(FieldIndex))
                    throw new InvalidOperationException("To pole było już wcześniej wybrane!");

                Board[FieldIndex] = ( PlayerTurn == PlayerType.Circle ? 'o' : 'x' );
                PlayerTurn = ( PlayerTurn == PlayerType.Circle ? PlayerType.Sharp : PlayerType.Circle );
            }

            else
                throw new InvalidOperationException("Numerki pola są od 1 do 9!");
        }

        private bool IsGivenLetterNumberBetween1And9(char FieldNumberSelected) =>
            char.IsDigit(FieldNumberSelected) && FieldNumberSelected != '0';

        private bool IsntFieldBlank(int FieldIndex) =>
            Board[FieldIndex] == 'x' || Board[FieldIndex] == 'o';
    }
}