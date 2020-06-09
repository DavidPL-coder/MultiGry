using System;

namespace MultiGry.Minesweeper
{
    class PerformerRoundPlayed
    {
        private ManagerOfFieldIndexes selectedFieldIndexes;
        public GameStatus StatusOfGame { private set; get; }
        private char[,] displayedBoard;
        private readonly char[,] actualBoardContent;
        private readonly MinesweeperGame game;

        public PerformerRoundPlayed(MinesweeperGame game)
        {
            StatusOfGame = game.StatusOfGame;
            displayedBoard = game.DisplayedBoard;
            actualBoardContent = game.ActualBoardContent;
            this.game = game;
        }

        public void DisplayOptionsToSelectFrom()
        {
            Console.WriteLine("\nWybierz opcje: ");
            Console.WriteLine("1. Odsłoń pole");
            Console.WriteLine("2. Ustaw chorągiewkę");
            Console.WriteLine("3. Usuń chorągiewkę");
            Console.WriteLine("4. Zakończ rozgrywkę");
            Console.WriteLine("5. Pokaż upłynięty czas");
        }

        public void PerformOperationsForSelectedOption(ConsoleKey keySelectedByUser)
        {
            if (keySelectedByUser == ConsoleKey.D4)
                TryingToStopGame();

            else if (keySelectedByUser == ConsoleKey.D5)
                DisplayElapsedGameTime();

            else
            {
                UserInputOfFieldIndexes();
                if (selectedFieldIndexes.CheckIndexesInTextVersion())
                    PerformingPlayerActionsOnBoard(keySelectedByUser);

                else
                    DisplayMessage("Wprowadzono nieprawidłowe wartości!");
            }
        }

        private void TryingToStopGame()
        {
            Console.WriteLine("Czy napewno chcesz zakończyć rozgrywkę?");
            Console.WriteLine("(naciśnij enter aby potwierdzić, bądź inny klawisz aby anulować)");

            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                StatusOfGame = GameStatus.Break;
        }

        private void DisplayElapsedGameTime() =>
            DisplayMessage("Czas: " + game.GameTime.GetTimeInTextVersion());

        private void UserInputOfFieldIndexes()
        {
            selectedFieldIndexes = new ManagerOfFieldIndexes();
            selectedFieldIndexes.UserInputOfFieldIndexesInTextVersion();
        }

        private void PerformingPlayerActionsOnBoard(ConsoleKey keySelectedByUser)
        {
            selectedFieldIndexes.SetTupleOfIndexes();
            switch (keySelectedByUser)
            {
                case ConsoleKey.D1: PlayerRevealsField(); break;
                case ConsoleKey.D2: PlayerSetsFlagOnField(); break;
                case ConsoleKey.D3: PlayerRemovesFlagOnField(); break;
            }
        }

        private void PlayerRevealsField()
        {
            if (DidUserSelectFlaggedField())
                DisplayMessage("Na tym pole jest chorągiewka, więc nie można odsłonić pola!");

            else if (IsNotSelectedFieldMined())
            {
                UnveilingFieldOrSeveralFields();
                StatusOfGame = DidPlayerRevealAllEmptyFields() 
                    ? GameStatus.PlayerWin 
                    : GameStatus.DuringGame;
            }

            else
            {
                UnveilingAllMines();
                StatusOfGame = GameStatus.PlayerLost;
            }
        }

        private bool DidUserSelectFlaggedField() =>
            displayedBoard[selectedFieldIndexes.VerticalIndex, selectedFieldIndexes.HorizontalIndex] == MinesweeperGame.FlagSign;

        private bool IsNotSelectedFieldMined() =>
            actualBoardContent[selectedFieldIndexes.VerticalIndex, selectedFieldIndexes.HorizontalIndex] != MinesweeperGame.BombSign;

        private void UnveilingFieldOrSeveralFields()
        {       
            int numbersMines = DisplayNumberOfMinesInField();

            if (numbersMines == 0)
                RevealEmptyFieldsAroundSelectedField();
        }

        private int DisplayNumberOfMinesInField()
        {
            var minesCounter = new MinesCounter(displayedBoard, actualBoardContent, new Rect());
            return minesCounter.DisplayNumberOfMinesInField(selectedFieldIndexes.VerticalIndex, selectedFieldIndexes.HorizontalIndex);
        }

        private void RevealEmptyFieldsAroundSelectedField()
        {
            var getterSquare = new GetterSquareOfExposedFields(selectedFieldIndexes.TupleOfIndexes);
            Rect exposedFields = getterSquare.GetSquareAroundSelectedField();

            var minesCounter = new MinesCounter(displayedBoard, actualBoardContent, exposedFields);
            minesCounter.LoadNumberOfMinesIntoDisplayedBoard();
        }

        private bool DidPlayerRevealAllEmptyFields()
        {
            for (int i = 0; i < MinesweeperGame.VerticalDimensionOfBoard; ++i)
            {
                for (int j = 0; j < MinesweeperGame.HorizontalDimensionOfBoard; ++j)
                {
                    if (IsTheFieldEmptyAndUnrevealed(i, j))
                        return false;
                }
            }
            return true;
        }

        private bool IsTheFieldEmptyAndUnrevealed(int i, int j) => 
            actualBoardContent[i, j] == MinesweeperGame.EmptyFieldSign && displayedBoard[i, j] == MinesweeperGame.SquareSign;

        private void UnveilingAllMines()
        {
            for (int i = 0; i < MinesweeperGame.VerticalDimensionOfBoard; ++i)
            {
                for (int j = 0; j < MinesweeperGame.HorizontalDimensionOfBoard; ++j)
                {
                    if (actualBoardContent[i, j] == MinesweeperGame.BombSign)
                        displayedBoard[i, j] = MinesweeperGame.BombSign;
                }
            }
        }

        private void PlayerSetsFlagOnField()
        {
            ref char currentBoardField = ref displayedBoard[selectedFieldIndexes.VerticalIndex, selectedFieldIndexes.HorizontalIndex];

            if (currentBoardField == MinesweeperGame.SquareSign)
                currentBoardField = MinesweeperGame.FlagSign;

            else
                DisplayMessage("Tutaj nie można wstawić flagi!");
        }

        private void PlayerRemovesFlagOnField()
        {
            ref char currentBoardField = ref displayedBoard[selectedFieldIndexes.VerticalIndex, selectedFieldIndexes.HorizontalIndex];

            if (currentBoardField == MinesweeperGame.FlagSign)
                currentBoardField = MinesweeperGame.SquareSign;

            else
                DisplayMessage("Na tym polu nie ma flagi!");
        }

        private void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(1500);
        }

        public void DisplayWrongOptionNumberMessage() =>
            DisplayMessage("Można wybrać tylko opcje z numerami 1-5!");
    }
}
