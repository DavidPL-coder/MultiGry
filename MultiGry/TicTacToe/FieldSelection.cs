using System;

namespace MultiGry.TicTacToe
{
    class FieldSelection
    {
        private char FieldNumberSelected;
        private char[] Board;
        private readonly PlayerType PlayerTurn;
        
        public FieldSelection(char[] Board, PlayerType PlayerTurn)
        {
            this.Board = Board;
            this.PlayerTurn = PlayerTurn;
        }

        /// <exception cref = "InvalidOperationException"> 
        /// if the method throws this exception, it means that the user has 
        /// entered the wrong field number or selected the field 
        /// that was selected earlier
        /// </exception>
        public void TryPlayerIsSelectingField()
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
            // replacement of field number to array index
            int FieldIndex = (FieldNumberSelected - '0') - 1; 

            if (IsNotFieldBlank(FieldIndex))
                throw new InvalidOperationException("To pole było już " +
                                                    "wcześniej wybrane!");

            Board[FieldIndex] = PlayerTurn == PlayerType.Circle 
                                ? TicTacToeGame.CircleSign 
                                : TicTacToeGame.SharpSign;
        }

        private bool IsNotFieldBlank(int FieldIndex) =>
            Board[FieldIndex] == TicTacToeGame.SharpSign || 
            Board[FieldIndex] == TicTacToeGame.CircleSign;
    }
}
