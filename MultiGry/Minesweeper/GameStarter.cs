using System;

namespace MultiGry.Minesweeper
{
    class GameStarter
    {
        private ManagerOfFieldIndexes selectedFieldIndexes;
        private MinesweeperGame game;

        public GameStarter(MinesweeperGame game) => 
            this.game = game;

        /// <returns> Returns "true" when the mines were drawn randomly 
        /// (i.e. prepare the board for the game)
        /// Returns "false" when drawing the mines failed
        /// (user entered incorrect field coordinates) </returns>
        public bool PreparingToPlayGame()
        {
            selectedFieldIndexes = new ManagerOfFieldIndexes();
            selectedFieldIndexes.UserInputOfFieldIndexesInTextVersion();

            if (selectedFieldIndexes.CheckIndexesInTextVersion())
            {
                DrawMines();
                return true;
            }

            else
            {
                DisplayMessage("Wprowadzono nieprawidłowe wartości!");
                return false;
            }
        }

        private void DrawMines()
        {
            selectedFieldIndexes.SetTupleOfIndexes();
            SetMinesOnBoard();
            LoadNumberOfMinesIntoDisplayedBoard();
        }

        private void SetMinesOnBoard()
        {
            var minesSetter = new MinesSetter(selectedFieldIndexes.TupleOfIndexes, new NumberGenerator());
            minesSetter.SetMinesOnBoard(game.ActualBoardContent, MinesweeperGame.NumberOfMines);
        }

        private void LoadNumberOfMinesIntoDisplayedBoard()
        {
            Rect squareOfExposedFields = SetRandomSquareOfExposedFields();
            var minesCounter = new MinesCounter(game.DisplayedBoard, game.ActualBoardContent, squareOfExposedFields);
            minesCounter.LoadNumberOfMinesIntoDisplayedBoard();
        }

        private Rect SetRandomSquareOfExposedFields()
        {
            var getterSquare = new GetterSquareOfExposedFields(selectedFieldIndexes.TupleOfIndexes);
            return getterSquare.GetRandomSquare();
        }

        private void DisplayMessage(string message)
        {
            Console.WriteLine(message);
            System.Threading.Thread.Sleep(1500);
        }
    }
}
