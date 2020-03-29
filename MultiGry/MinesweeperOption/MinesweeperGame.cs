using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MultiGry
{
    class MinesweeperGame : IMenuOption
    {
        public string NameOption => "Saper";
        public const int VerticalDimensionOfBoard = 8;
        public const int HorizontalDimensionOfBoard = 8;
        private bool IsThereFirstRound;
        public char[,] DisplayedBoard { private set; get; }
        public char[,] ActualBoardContent { private set; get; }
        public GameDuration GameTime { private set; get; }
        public MinesweeperGameStatus StatusOfGame { private set; get; }
        public ManagerOfSelectedFieldIndexes SelectedFieldIndexes { private set; get; }

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();           
            PlayingGame();

            if (StatusOfGame != MinesweeperGameStatus.Break)
                DisplayGameResult();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void SetDefaults()
        {
            IsThereFirstRound = true;
            SetBoard();
            StatusOfGame = MinesweeperGameStatus.DuringGame;
            GameTime = new GameDuration();
        }

        private void SetBoard()
        {
            var BoardSetter = new BoardSetter();
            BoardSetter.CreateBoard();
            DisplayedBoard = BoardSetter.DisplayedBoard;
            ActualBoardContent = BoardSetter.ActualBoardContent;
        }

        private void PlayingGame()
        {
            GameTime.Start();

            while (StatusOfGame == MinesweeperGameStatus.DuringGame)
            {
                DisplayBoardContent();
                if (IsThereFirstRound)
                    StartingGame();

                else
                    PlayingRound();
            }

            GameTime.Stop();
        }

        private void DisplayBoardContent()
        {
            var BoardDisplay = new BoardDisplay(DisplayedBoard);
            BoardDisplay.DisplayContent();
        }

        private void StartingGame()
        {
            SelectedFieldIndexes = new ManagerOfSelectedFieldIndexes();
            SelectedFieldIndexes.UserInputOfFieldIndexesInTextVersion();

            if (SelectedFieldIndexes.CheckIndexesInTextVersion())
                PreparingToPlayGame();

            else
                DisplayMessage("Wprowadzono nieprawidłowe wartości!");
        }

        private void PreparingToPlayGame()
        {
            SelectedFieldIndexes.SetTupleOfIndexes();
            SetMinesOnBoard();
            LoadNumberOfMinesIntoDisplayedBoard();
            IsThereFirstRound = false;
        }

        private void SetMinesOnBoard()
        {
            var MinesSetter = new MinesSetter(this);
            MinesSetter.SetMinesOnBoard(10);
        }

        private void LoadNumberOfMinesIntoDisplayedBoard()
        {
            Rect SquareOfExposedFields = SetRandomSquareOfExposedFields();
            var MinesCounter = new MinesCounterOnBoard(this, SquareOfExposedFields);
            MinesCounter.LoadNumberOfMinesIntoDisplayedBoard();
        }

        private Rect SetRandomSquareOfExposedFields()
        {
            var SetterSquareOfExposedFields = new SetterSquareOfExposedFields(SelectedFieldIndexes.TupleOfIndexes);
            return SetterSquareOfExposedFields.GetRandomSquare();
        }

        private void DisplayMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        private void PlayingRound()
        {
            var PerformerRoundPlayed = new PerformerRoundPlayed(this);
            PerformerRoundPlayed.DisplayOptionsToSelectFrom();
            var Key = Console.ReadKey(true).Key;

            if (Key >= ConsoleKey.D1 && Key <= ConsoleKey.D5)
                PerformerRoundPlayed.PerformOperationsForSelectedOption(Key);

            else
                PerformerRoundPlayed.DisplayWrongOptionNumberMessage();

            StatusOfGame = PerformerRoundPlayed.StatusOfGame;
        }

        // method should be called only for StatusOfGame equal to PlayerWin or PlayerLost
        private void DisplayGameResult()
        {
            DisplayBoardContent();

            string Message = StatusOfGame == MinesweeperGameStatus.PlayerWin ? "Brawo! Wygrałeś!" : "Niestety! Nie udało ci się!";
            Console.WriteLine(Message);

            Console.WriteLine("Twój czas: " + GameTime.GetTimeInTextVersion());
            Console.ReadKey();
        }   
    }
}
