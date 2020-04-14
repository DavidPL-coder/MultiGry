using System;

namespace MultiGry.Minesweeper
{
    class PerformerRoundPlayed
    {
        private ManagerOfFieldIndexes SelectedFieldIndexes;
        public GameStatus StatusOfGame { private set; get; }
        private char[,] DisplayedBoard;
        private readonly char[,] ActualBoardContent;
        private readonly MinesweeperGame Game;

        public PerformerRoundPlayed(MinesweeperGame Game)
        {
            StatusOfGame = Game.StatusOfGame;
            DisplayedBoard = Game.DisplayedBoard;
            ActualBoardContent = Game.ActualBoardContent;

            this.Game = Game;
        }

        public void DisplayOptionsToSelectFrom()
        {
            Console.WriteLine("\n" + "Wybierz opcje: ");
            Console.WriteLine("1. Odsłoń pole");
            Console.WriteLine("2. Ustaw chorągiewkę");
            Console.WriteLine("3. Usuń chorągiewkę");
            Console.WriteLine("4. Zakończ rozgrywkę");
            Console.WriteLine("5. Pokaż upłynięty czas");
        }

        public void PerformOperationsForSelectedOption(ConsoleKey KeySelectedByUser)
        {
            if (KeySelectedByUser == ConsoleKey.D4)
                TryingToStopGame();

            else if (KeySelectedByUser == ConsoleKey.D5)
                DisplayElapsedGameTime();

            else
            {
                UserInputOfFieldIndexes();
                if (SelectedFieldIndexes.CheckIndexesInTextVersion())
                    PerformingPlayerActionsOnBoard(KeySelectedByUser);

                else
                    DisplayMessage("Wprowadzono nieprawidłowe wartości!");
            }
        }

        private void TryingToStopGame()
        {
            Console.WriteLine("Czy napewno chcesz zakończyć rozgrywkę?");
            Console.WriteLine("(naciśnij enter aby potwierdzić, bądź inny " +
                              "klawisz aby anulować)");

            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                StatusOfGame = GameStatus.Break;
        }

        private void DisplayElapsedGameTime() =>
            DisplayMessage("Czas: " + Game.GameTime.GetTimeInTextVersion());

        private void UserInputOfFieldIndexes()
        {
            SelectedFieldIndexes = new ManagerOfFieldIndexes();
            SelectedFieldIndexes.UserInputOfFieldIndexesInTextVersion();
        }

        private void PerformingPlayerActionsOnBoard(ConsoleKey KeySelectedByUser)
        {
            SelectedFieldIndexes.SetTupleOfIndexes();
            switch (KeySelectedByUser)
            {
                case ConsoleKey.D1: PlayerRevealsField(); break;
                case ConsoleKey.D2: PlayerSetsFlagOnField(); break;
                case ConsoleKey.D3: PlayerRemovesFlagOnField(); break;
            }
        }

        private void PlayerRevealsField()
        {
            if (DidUserSelectFlaggedField())
                DisplayMessage("Na tym pole jest chorągiewka, więc nie można " +
                               "odsłonić pola!");

            else if (IsNotSelectedFieldMined())
            {
                UnveilingFieldOrSeveralFields();
                StatusOfGame = DidPlayerRevealAllEmptyFields() ? GameStatus.PlayerWin 
                                                               : GameStatus.DuringGame;
            }

            else
            {
                UnveilingAllMines();
                StatusOfGame = GameStatus.PlayerLost;
            }
        }

        private bool DidUserSelectFlaggedField() =>
            DisplayedBoard[SelectedFieldIndexes.Vertical, 
                          SelectedFieldIndexes.Horizontal] == 
                          MinesweeperGame.FlagSign;

        private bool IsNotSelectedFieldMined() =>
            ActualBoardContent[SelectedFieldIndexes.Vertical,
                               SelectedFieldIndexes.Horizontal] != 
                               MinesweeperGame.BombSign;

        private void UnveilingFieldOrSeveralFields()
        {       
            int NumbersMines = DisplayNumberOfMinesInField();

            if (NumbersMines == 0)
                RevealEmptyFieldsAroundSelectedField();
        }

        private int DisplayNumberOfMinesInField()
        {
            var MinesCounter = new MinesCounter(Game, new Rect());

            return MinesCounter.DisplayNumberOfMinesInField(
                SelectedFieldIndexes.Vertical, SelectedFieldIndexes.Horizontal);
        }

        private void RevealEmptyFieldsAroundSelectedField()
        {
            var GetterSquare = new GetterSquareOfExposedFields(SelectedFieldIndexes
                                                               .TupleOfIndexes);

            Rect ExposedFields = GetterSquare.GetSquareAroundSelectedField();

            var MinesCounter = new MinesCounter(Game, ExposedFields);
            MinesCounter.LoadNumberOfMinesIntoDisplayedBoard();
        }

        private bool DidPlayerRevealAllEmptyFields()
        {
            for (int i = 0; i < MinesweeperGame.VerticalDimensionOfBoard; ++i)
                for (int j = 0; j < MinesweeperGame.HorizontalDimensionOfBoard; ++j)
                    if (IsTheFieldEmptyAndUnrevealed(i, j))
                        return false;

            return true;
        }

        private bool IsTheFieldEmptyAndUnrevealed(int i, int j) => 
            ActualBoardContent[i, j] == MinesweeperGame.EmptyFieldSign && 
            DisplayedBoard[i, j] == MinesweeperGame.SquareSign;

        private void UnveilingAllMines()
        {
            for (int i = 0; i < MinesweeperGame.VerticalDimensionOfBoard; ++i)
                for (int j = 0; j < MinesweeperGame.HorizontalDimensionOfBoard; ++j)
                    if (ActualBoardContent[i, j] == MinesweeperGame.BombSign)
                        DisplayedBoard[i, j] = MinesweeperGame.BombSign;
        }

        private void PlayerSetsFlagOnField()
        {
            ref char CurrentBoardField = ref DisplayedBoard[
                SelectedFieldIndexes.Vertical, SelectedFieldIndexes.Horizontal];

            if (CurrentBoardField == MinesweeperGame.SquareSign)
                CurrentBoardField = MinesweeperGame.FlagSign;

            else
                DisplayMessage("Tutaj nie można wstawić flagi!");
        }

        private void PlayerRemovesFlagOnField()
        {
            ref char CurrentBoardField = ref DisplayedBoard[
                SelectedFieldIndexes.Vertical, SelectedFieldIndexes.Horizontal];

            if (CurrentBoardField == MinesweeperGame.FlagSign)
                CurrentBoardField = MinesweeperGame.SquareSign;

            else
                DisplayMessage("Na tym polu nie ma flagi!");
        }

        private void DisplayMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        public void DisplayWrongOptionNumberMessage() =>
            DisplayMessage("Można wybrać tylko opcje z numerami 1-5!");
    }
}
