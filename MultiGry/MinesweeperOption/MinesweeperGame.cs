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
            DisplayGameResult();

            if (StatusOfGame != MinesweeperGameStatus.Break)
                Console.ReadKey();

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
            var BoardSetter = new BoardSetter(this);
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
            var KeySelectedByUser = Console.ReadKey(true).Key;

            if (KeySelectedByUser >= ConsoleKey.D1 && KeySelectedByUser <= ConsoleKey.D5)
                PerformerRoundPlayed.PerformOperationsForSelectedOption(KeySelectedByUser);

            else
                PerformerRoundPlayed.DisplayWrongOptionNumberMessage();

            StatusOfGame = PerformerRoundPlayed.StatusOfGame;
        }

        private void DisplayGameResult()
        {
            DisplayBoardContent();

            if (StatusOfGame == MinesweeperGameStatus.PlayerWin)
                Console.WriteLine("Brawo! Wygrałeś!");

            if (StatusOfGame == MinesweeperGameStatus.PlayerLost)
                Console.WriteLine("Niestety! Nie udało ci się!");

            Console.WriteLine("Twój czas: " + GameTime.GetTimeInTextVersion());
        }   
    }
}
