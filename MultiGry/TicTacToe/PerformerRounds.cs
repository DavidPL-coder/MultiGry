using System;

namespace MultiGry.TicTacToe
{
    class PerformerRounds
    {
        public PlayerType PlayerTurn { private set; get; }
        public char[] Board { private set; get; }
        private int TurnNumber;
        public PlayerType Winner { private set; get; }

        public PerformerRounds()
        {
            TurnNumber = 1;
            Winner = PlayerType.Nobody;
            Board = new char[]
            {
                '1', '2', '3',
                '4', '5', '6',
                '7', '8', '9'
            };
        }

        public void DrawPlayer()
        {
            var Random = new Random();
            // draws "Circle" or "Sharp"
            PlayerTurn = (PlayerType) Random.Next(1, 3);    
        }

        public bool IsGameStillGoingOn() =>
            TurnNumber <= 9 && Winner == PlayerType.Nobody;

        public void PlayingOneTurn()
        {
            DisplayGameInterface();

            PlayerIsSelectingField();
            ++TurnNumber;

            SetWinner();
            Console.Clear();
        }

        private void DisplayGameInterface()
        {
            var InterfaceDisplay = new GameInterfaceDisplay(this);
            InterfaceDisplay.DisplayInterface();
        }

        private void PlayerIsSelectingField()
        {
            var FieldSelection = new FieldSelection(Board, PlayerTurn);
            try
            {
                FieldSelection.TryPlayerIsSelectingField();
                PlayerSwap();
            }
            catch (InvalidOperationException InvalidFieldNumberException)
            {
                Console.WriteLine(InvalidFieldNumberException.Message);
                System.Threading.Thread.Sleep(1500);
                --TurnNumber;
            }
        }

        private void PlayerSwap() => 
            PlayerTurn = (PlayerTurn == PlayerType.Circle ? PlayerType.Sharp 
                                                          : PlayerType.Circle);

        private void SetWinner()
        {
            var Judge = new Judge(Board);
            Winner = Judge.CheckWhoWon();
        }
    }
}
