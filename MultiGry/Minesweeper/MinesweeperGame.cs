using System;

namespace MultiGry.Minesweeper
{
    // This game displays characters on the screen that may be displayed 
    // differently in other character encoding systems. Characters are displayed 
    // correctly for Windows-1250 encoding

    class MinesweeperGame : IMenuOption
    {
        public string NameOption => "Saper";
        public const int VerticalDimensionOfBoard = 8;
        public const int HorizontalDimensionOfBoard = 8;
        public const char SquareSign = '■';
        public const char EmptyFieldSign = 'O';
        public const char FlagSign = 'C';
        public const char BombSign = '*';
        public const int NumberOfMines = 10;
        private bool IsThereFirstRound;
        public char[,] DisplayedBoard { private set; get; }
        public char[,] ActualBoardContent { private set; get; }
        public GameDuration GameTime { private set; get; }
        public GameStatus StatusOfGame { private set; get; }
        public ManagerOfFieldIndexes SelectedFieldIndexes { private set; get; }

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();           
            PlayingGame();

            if (StatusOfGame != GameStatus.Break)
                DisplayGameResult();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void SetDefaults()
        {
            IsThereFirstRound = true;
            SetBoard();
            StatusOfGame = GameStatus.DuringGame;
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

            while (StatusOfGame == GameStatus.DuringGame)
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
            SelectedFieldIndexes = new ManagerOfFieldIndexes();
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
            MinesSetter.SetMinesOnBoard();
        }

        private void LoadNumberOfMinesIntoDisplayedBoard()
        {
            Rect SquareOfExposedFields = SetRandomSquareOfExposedFields();
            var MinesCounter = new MinesCounter(this, SquareOfExposedFields);
            MinesCounter.LoadNumberOfMinesIntoDisplayedBoard();
        }

        private Rect SetRandomSquareOfExposedFields()
        {
            var GetterSquare = new GetterSquareOfExposedFields(SelectedFieldIndexes
                                                              .TupleOfIndexes);
            return GetterSquare.GetRandomSquare();
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

        // method should be called only for StatusOfGame 
        // equal to PlayerWin or PlayerLost
        private void DisplayGameResult()
        {
            DisplayBoardContent();
            string Message = StatusOfGame == GameStatus.PlayerWin ? 
                             "Brawo! Wygrałeś!" : "Niestety! Nie udało ci się!";

            Console.WriteLine(Message);
            Console.WriteLine("Twój czas: " + GameTime.GetTimeInTextVersion());
            Console.ReadKey();
        }   
    }
}
