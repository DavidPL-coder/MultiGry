using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class PerformerRoundPlayed
    {
        private ManagerOfSelectedFieldIndexes SelectedFieldIndexes;
        public MinesweeperGameStatus StatusOfGame { private set; get; }
        private char[,] DisplayedBoard;
        private readonly char[,] ActualBoardContent;
        private readonly MinesweeperGame minesweeperGame;

        public PerformerRoundPlayed(MinesweeperGame Game)
        {
            StatusOfGame = Game.StatusOfGame;
            DisplayedBoard = Game.DisplayedBoard;
            ActualBoardContent = Game.ActualBoardContent;

            minesweeperGame = Game;
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
            Console.WriteLine("(naciśnij enter aby potwierdzić, bądź inny klawisz aby anulować)");

            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                StatusOfGame = MinesweeperGameStatus.Break;
        }

        private void DisplayElapsedGameTime() =>
            DisplayMessage("Czas: " + minesweeperGame.GameTime.GetTimeInTextVersion());

        private void UserInputOfFieldIndexes()
        {
            SelectedFieldIndexes = new ManagerOfSelectedFieldIndexes();
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
                DisplayMessage("Na tym pole jest chorągiewka, więc nie można odsłonić pola!");

            else if (IsntSelectedFieldMined())
            {
                UnveilingFieldOrSeveralFields();
                StatusOfGame = DidPlayerRevealAllEmptyFields() ? MinesweeperGameStatus.PlayerWin : MinesweeperGameStatus.DuringGame;
            }

            else
            {
                UnveilingAllMines();
                StatusOfGame = MinesweeperGameStatus.PlayerLost;
            }
        }

        private bool DidUserSelectFlaggedField() =>
            DisplayedBoard[SelectedFieldIndexes.Vertical, 
                           SelectedFieldIndexes.Horizontal] == 'C';

        private bool IsntSelectedFieldMined() =>
            ActualBoardContent[SelectedFieldIndexes.Vertical,
                               SelectedFieldIndexes.Horizontal] != '*';

        private void UnveilingFieldOrSeveralFields()
        {       
            int NumbersMines = DisplayNumberOfMinesInField();

            if (NumbersMines == 0)
                RevealEmptyFieldsAroundSelectedField();
        }

        private int DisplayNumberOfMinesInField()
        {
            var MinesCounter = new MinesCounterOnBoard(minesweeperGame, new Rect());

            return MinesCounter.DisplayNumberOfMinesInField(SelectedFieldIndexes.Vertical, 
                                                            SelectedFieldIndexes.Horizontal);
        }

        private void RevealEmptyFieldsAroundSelectedField()
        {
            var SetterSquareOfExposedFields = new SetterSquareOfExposedFields(SelectedFieldIndexes.TupleOfIndexes);
            Rect SquareOfExposedFields = SetterSquareOfExposedFields.GetSquareAroundSelectedField();

            var MinesCounter = new MinesCounterOnBoard(minesweeperGame, SquareOfExposedFields);
            MinesCounter.LoadNumberOfMinesIntoDisplayedBoard();
        }

        private bool DidPlayerRevealAllEmptyFields()
        {
            for (int i = 0; i < MinesweeperGame.VerticalDimensionOfBoard; ++i)
                for (int j = 0; j < MinesweeperGame.HorizontalDimensionOfBoard; ++j)
                    if (ActualBoardContent[i, j] == 'O' && DisplayedBoard[i, j] == '■')
                        return false;

            return true;
        }

        private void UnveilingAllMines()
        {
            for (int i = 0; i < MinesweeperGame.VerticalDimensionOfBoard; ++i)
                for (int j = 0; j < MinesweeperGame.HorizontalDimensionOfBoard; ++j)
                    if (ActualBoardContent[i, j] == '*')
                        DisplayedBoard[i, j] = '*';
        }

        private void PlayerSetsFlagOnField()
        {
            ref char CurrentBoardField = ref DisplayedBoard[SelectedFieldIndexes.Vertical, SelectedFieldIndexes.Horizontal];
            if (CurrentBoardField == '■')
                CurrentBoardField = 'C';

            else
                DisplayMessage("Tutaj nie można wstawić flagi!");
        }

        private void PlayerRemovesFlagOnField()
        {
            ref char CurrentBoardField = ref DisplayedBoard[SelectedFieldIndexes.Vertical, SelectedFieldIndexes.Horizontal];
            if (CurrentBoardField == 'C')
                CurrentBoardField = '■';

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
