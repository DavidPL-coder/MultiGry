namespace MultiGry.TicTacToe
{
    class Judge
    {
        private readonly char[] Board;

        public Judge(char[] Board) => 
            this.Board = Board;

        public PlayerType CheckWhoWon()
        {
            PlayerType Horizontal = CheckingWhoWonHorizontally();
            PlayerType Vertical = CheckingWhoWonVertically();
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
                if (Board[i] == TicTacToeGame.SharpSign && 
                    Board[i + 1] == TicTacToeGame.SharpSign && 
                    Board[i + 2] == TicTacToeGame.SharpSign)
                    return PlayerType.Sharp;

                if (Board[i] == TicTacToeGame.CircleSign &&
                    Board[i + 1] == TicTacToeGame.CircleSign &&
                    Board[i + 2] == TicTacToeGame.CircleSign)
                    return PlayerType.Circle;
            }

            return PlayerType.Nobody;
        }

        private PlayerType CheckingWhoWonVertically()
        {
            for (int i = 0; i < 3; ++i)
            {
                if (Board[i] == TicTacToeGame.SharpSign &&
                    Board[i + 3] == TicTacToeGame.SharpSign &&
                    Board[i + 6] == TicTacToeGame.SharpSign)
                    return PlayerType.Sharp;

                if (Board[i] == TicTacToeGame.CircleSign &&
                    Board[i + 3] == TicTacToeGame.CircleSign &&
                    Board[i + 6] == TicTacToeGame.CircleSign)
                    return PlayerType.Circle;
            }

            return PlayerType.Nobody;
        }

        private PlayerType CheckingWhoWonDiagonally()
        {
            if (Board[0] == TicTacToeGame.SharpSign && 
                Board[4] == TicTacToeGame.SharpSign && 
                Board[8] == TicTacToeGame.SharpSign)
                return PlayerType.Sharp;

            if (Board[2] == TicTacToeGame.SharpSign && 
                Board[4] == TicTacToeGame.SharpSign && 
                Board[6] == TicTacToeGame.SharpSign)
                return PlayerType.Sharp;

            if (Board[0] == TicTacToeGame.CircleSign && 
                Board[4] == TicTacToeGame.CircleSign && 
                Board[8] == TicTacToeGame.CircleSign)
                return PlayerType.Circle;

            if (Board[2] == TicTacToeGame.CircleSign && 
                Board[4] == TicTacToeGame.CircleSign && 
                Board[6] == TicTacToeGame.CircleSign)
                return PlayerType.Circle;

            return PlayerType.Nobody;
        }
    }
}
