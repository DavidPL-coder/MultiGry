using System;

namespace MultiGry.Minesweeper
{
    // This game displays characters on the screen that may be displayed 
    // differently in other character encoding systems. Characters are displayed 
    // correctly for Windows-1250 encoding

    public class MinesweeperGame : IMenuOption
    {
        public string NameOption => "Saper";
        public const int VerticalDimensionOfBoard = 8;
        public const int HorizontalDimensionOfBoard = 8;
        public const char SquareSign = '■';
        public const char EmptyFieldSign = 'O';
        public const char FlagSign = 'C';
        public const char BombSign = '*';
        public const int NumberOfMines = 10;
        public char[,] DisplayedBoard { private set; get; }
        public char[,] ActualBoardContent { private set; get; }
        public GameDuration GameTime { private set; get; }
        public GameStatus StatusOfGame { private set; get; }
        private GameStarter gameStarter;
        //public ManagerOfFieldIndexes SelectedFieldIndexes { private set; get; }

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
            SetBoard();
            StatusOfGame = GameStatus.DuringGame;
            GameTime = new GameDuration();
            gameStarter = new GameStarter(this);
        }

        private void SetBoard()
        {
            DisplayedBoard = new char[VerticalDimensionOfBoard, HorizontalDimensionOfBoard];
            ActualBoardContent = new char[VerticalDimensionOfBoard, HorizontalDimensionOfBoard];
            BoardSetter.CreateBoard(DisplayedBoard, ActualBoardContent);
        }

        private void PlayingGame()
        {
            GameTime.Start();
            bool areGameBoardsReady = false;
            while (StatusOfGame == GameStatus.DuringGame)
            {
                DisplayBoardContent();
                if (!areGameBoardsReady)
                    areGameBoardsReady = gameStarter.PreparingToPlayGame();

                else
                    PlayingRound();
            }
            GameTime.Stop();
        }

        private void DisplayBoardContent()
        {
            var boardDisplay = new BoardDisplay(DisplayedBoard);
            boardDisplay.DisplayContent();
        }

        /// <returns> Returns "true" when the mines were drawn randomly 
        /// (i.e. prepare the board for the game)
        /// Returns "false" when drawing the mines failed
        /// (user entered incorrect field coordinates) </returns>
        //private bool PreparingToPlayGame()
        //{
        //    SelectedFieldIndexes = new ManagerOfFieldIndexes();
        //    SelectedFieldIndexes.UserInputOfFieldIndexesInTextVersion();

        //    if (SelectedFieldIndexes.CheckIndexesInTextVersion())
        //    {
        //        DrawMines(); 
        //        return true;
        //    }

        //    else
        //    {
        //        DisplayMessage("Wprowadzono nieprawidłowe wartości!");
        //        return false;
        //    }
        //}

        //private void DrawMines()
        //{
        //    SelectedFieldIndexes.SetTupleOfIndexes();
        //    SetMinesOnBoard();
        //    LoadNumberOfMinesIntoDisplayedBoard();
        //}

        //private void SetMinesOnBoard()
        //{
        //    //var MinesSetter = new MinesSetter(this);
        //    //MinesSetter.SetMinesOnBoard();
        //}

        //private void LoadNumberOfMinesIntoDisplayedBoard()
        //{
        //    Rect SquareOfExposedFields = SetRandomSquareOfExposedFields();
        //    var MinesCounter = new MinesCounter(this, SquareOfExposedFields);
        //    MinesCounter.LoadNumberOfMinesIntoDisplayedBoard();
        //}

        //private Rect SetRandomSquareOfExposedFields()
        //{
        //    var GetterSquare = new GetterSquareOfExposedFields(SelectedFieldIndexes
        //                                                      .TupleOfIndexes);
        //    return GetterSquare.GetRandomSquare();
        //}

        //private void DisplayMessage(string Message)
        //{
        //    Console.WriteLine(Message);
        //    System.Threading.Thread.Sleep(1500);
        //}

        private void PlayingRound()
        {
            var performerRoundPlayed = new PerformerRoundPlayed(this);
            performerRoundPlayed.DisplayOptionsToSelectFrom();
            var key = Console.ReadKey(true).Key;

            if (key >= ConsoleKey.D1 && key <= ConsoleKey.D5)
                performerRoundPlayed.PerformOperationsForSelectedOption(key);

            else
                performerRoundPlayed.DisplayWrongOptionNumberMessage();

            StatusOfGame = performerRoundPlayed.StatusOfGame;
        }

        // method should be called only for StatusOfGame 
        // equal to PlayerWin or PlayerLost
        private void DisplayGameResult()
        {
            DisplayBoardContent();
            string message = StatusOfGame == GameStatus.PlayerWin 
                ? "Brawo! Wygrałeś!" 
                : "Niestety! Nie udało ci się!";

            Console.WriteLine(message);
            Console.WriteLine("Twój czas: " + GameTime.GetTimeInTextVersion());
            Console.ReadKey();
        }   
    }
}
