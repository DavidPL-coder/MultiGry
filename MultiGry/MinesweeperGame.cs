using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MultiGry
{
    class MinesweeperGame : IMenuOption
    {
        public string NameOption => "Saper";
        private bool IsThereFirstRound;
        private char[,] DisplayedBoard;
        private char[,] ActualBoardContent;
        private const int VerticalDimensionOfBoard = 8;
        private const int HorizontalDimensionOfBoard = 8;
        private List<Tuple<int, int>> CoordinatesOfMinesDrawn;
        private Random NumberGenerator;
        enum GameStatus { DuringGame, PlayerLost, PlayerWin, Break }
        GameStatus StatusOfGame;
        Stopwatch GameTime;
        private string[] SelectedIndexesInTextVersion;
        private Tuple<int, int> IndexesOfField;  
        struct Rect
        {
            public int Left, Top, Right, Bottom;
        }
        Rect SquareOfExposedFields;
        
        public OptionsCategory OptionExecuting()
        {
            ExecutingNewGame();

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }


        private void ExecutingNewGame()
        {
            SetDefaults();
            GameTime.Start();
            PlayingGame();

            DisplayBoard();
            GameTime.Stop();
            DisplayGameResult();

            if (StatusOfGame != GameStatus.Break)
                Console.ReadKey();
        }

        private void SetDefaults()
        {
            IsThereFirstRound = true;
            SetBoard();
            CoordinatesOfMinesDrawn = new List<Tuple<int, int>>();
            NumberGenerator = new Random();
            StatusOfGame = GameStatus.DuringGame;
            GameTime = new Stopwatch();
        }

        private void SetBoard()
        {
            DisplayedBoard = new char[VerticalDimensionOfBoard, HorizontalDimensionOfBoard];
            ActualBoardContent = new char[VerticalDimensionOfBoard, HorizontalDimensionOfBoard];

            for (int i = 0; i < VerticalDimensionOfBoard; ++i)
            {
                for (int j = 0; j < HorizontalDimensionOfBoard; ++j)
                {
                    DisplayedBoard[i, j] = '■';
                    ActualBoardContent[i, j] = 'O';
                }
            }
        }

        private void PlayingGame()
        {
            while (StatusOfGame == GameStatus.DuringGame)
            {
                DisplayBoard();
                if (IsThereFirstRound)
                    StartingGame();

                else
                    PlayingRound();
            }
        }

        private void DisplayBoard()
        {          
            Console.Clear();
            Console.WriteLine("  1 2 3 4 5 6 7 8");
            Console.WriteLine("  - - - - - - - -");
            DisplayVerticalBoardLines();
        }

        private void DisplayVerticalBoardLines()
        {
            for (int i = 0; i < VerticalDimensionOfBoard; ++i)
            {
                Console.Write(i + 1 + "|");
                for (int j = 0; j < HorizontalDimensionOfBoard; ++j)
                {
                    SetColorForCharacter(i, j);
                    Console.Write(DisplayedBoard[i, j] + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }

        private void SetColorForCharacter(int i, int j)
        {
            switch (DisplayedBoard[i, j])
            {
                case '*': Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case 'C': Console.ForegroundColor = ConsoleColor.Yellow; break;
                case '1': Console.ForegroundColor = ConsoleColor.Blue; break;
                case '2': Console.ForegroundColor = ConsoleColor.Green; break;
                case '3': Console.ForegroundColor = ConsoleColor.Red; break;
                case '4': Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                case '5':
                case '6':
                case '7':
                case '8':
                    Console.ForegroundColor = ConsoleColor.Magenta; break;
            }
        }

        private void StartingGame()
        {
            UserInputOfFieldIndexes();
            if (CheckSelectedIndexes())
                PreparingToPlayGame();

            else
                DisplayMessage("Wprowadzono nieprawidłowe wartości!");
        }

        private void UserInputOfFieldIndexes()
        {
            Console.Write("Wybierz pole (podaj pionowy indeks oraz po spacji poziomy indeks): ");
            SelectedIndexesInTextVersion = Console.ReadLine().Split();
        }

        private bool CheckSelectedIndexes()
        {
            if (SelectedIndexesInTextVersion.Length != 2)
                return false;

            else
                return CheckIfNumbersBetween1And8AreGiven();
        }

        private bool CheckIfNumbersBetween1And8AreGiven()
        {
            if (!int.TryParse(SelectedIndexesInTextVersion[0], out _) || !int.TryParse(SelectedIndexesInTextVersion[1], out _))
                return false;

            else
                return int.Parse(SelectedIndexesInTextVersion[0]) >= 1 && int.Parse(SelectedIndexesInTextVersion[0]) <= 8 &&
                       int.Parse(SelectedIndexesInTextVersion[1]) >= 1 && int.Parse(SelectedIndexesInTextVersion[1]) <= 8;
        }

        private void PreparingToPlayGame()
        {
            SetIndexesOfField();
            SetMinesOnBoard();

            SetTopAndBottomOfSquare();
            SetLeftAndRightOfSquare();

            LoadNumberOfMinesIntoDisplayedBoard();
            IsThereFirstRound = false;
        }

        private void SetIndexesOfField() =>
            IndexesOfField = Tuple.Create(int.Parse(SelectedIndexesInTextVersion[0]) - 1,
                                          int.Parse(SelectedIndexesInTextVersion[1]) - 1);

        private void SetMinesOnBoard()
        {
            for (int i = 0; i < 10; ++i)
            {
                int Vertical = NumberGenerator.Next(0, VerticalDimensionOfBoard);
                int Horizontal = NumberGenerator.Next(0, HorizontalDimensionOfBoard);
                var CoordinatePair = Tuple.Create(Vertical, Horizontal);

                if (CanMineBeInThisField(CoordinatePair))
                {
                    ActualBoardContent[Vertical, Horizontal] = '*';
                    CoordinatesOfMinesDrawn.Add(CoordinatePair);
                }

                else
                    --i;
            }
        }

        private bool CanMineBeInThisField(Tuple<int, int> CoordinatePair)
        {
            for (int j = 0; j < CoordinatesOfMinesDrawn.Count; ++j)
                if (Equals(CoordinatesOfMinesDrawn[j], CoordinatePair) ||
                    Equals(CoordinatesOfMinesDrawn[j], IndexesOfField))
                    return false;

            return true;
        }

        private void SetTopAndBottomOfSquare()
        {
            bool AreFieldsToBeUncoveredUpwards = NumberGenerator.Next(0, 2) == 1;
            if (AreFieldsToBeUncoveredUpwards)
            {
                SquareOfExposedFields.Top = IndexesOfField.Item1 - 2;
                SquareOfExposedFields.Bottom = IndexesOfField.Item1;
            }

            else
            {
                SquareOfExposedFields.Top = IndexesOfField.Item1;
                SquareOfExposedFields.Bottom = IndexesOfField.Item1 + 2;
            }
        }

        private void SetLeftAndRightOfSquare()
        {
            bool AreFieldsToBeUncoveredLeft = NumberGenerator.Next(0, 2) == 1;
            if (AreFieldsToBeUncoveredLeft)
            {
                SquareOfExposedFields.Left = IndexesOfField.Item2 - 2;
                SquareOfExposedFields.Right = IndexesOfField.Item2;
            }

            else
            {
                SquareOfExposedFields.Left = IndexesOfField.Item2;
                SquareOfExposedFields.Right = IndexesOfField.Item2 + 2;
            }
        }

        private void LoadNumberOfMinesIntoDisplayedBoard()
        {
            for (int i = SquareOfExposedFields.Top; i <= SquareOfExposedFields.Bottom; ++i)
            {
                for (int j = SquareOfExposedFields.Left; j <= SquareOfExposedFields.Right; ++j)
                {
                    if (IsThereFieldWithSuchIndex(i, j) && ActualBoardContent[i, j] != '*')
                        DisplayNumberOfMinesInField(i, j);
                }
            }
        }

        private bool IsThereFieldWithSuchIndex(int i, int j) =>
            i >= 0 &&
            i < VerticalDimensionOfBoard &&
            j >= 0 &&
            j < HorizontalDimensionOfBoard;

        private int DisplayNumberOfMinesInField(int VerticalIndex, int HorizontalIndex)
        {
            int NumberDisplayedInGivenField = CalculateHowManyMinesAreAroundField(VerticalIndex, HorizontalIndex);

            if (NumberDisplayedInGivenField != 0)
                DisplayedBoard[VerticalIndex, HorizontalIndex] = NumberDisplayedInGivenField.ToString()[0];

            else
                DisplayedBoard[VerticalIndex, HorizontalIndex] = 'O';

            return NumberDisplayedInGivenField;
        }

        private int CalculateHowManyMinesAreAroundField(int VerticalIndex, int HorizontalIndex)
        {
            int NumberDisplayedInGivenField = 0;
            for (int i = VerticalIndex - 1; i <= VerticalIndex + 1; ++i)
            {
                for (int j = HorizontalIndex - 1; j <= HorizontalIndex + 1; ++j)
                {
                    if (IsThereFieldWithSuchIndex(i, j) && ActualBoardContent[i, j] == '*')
                        ++NumberDisplayedInGivenField;
                }
            }
            return NumberDisplayedInGivenField;
        }

        private void DisplayMessage(string Message)
        {
            Console.WriteLine(Message);
            System.Threading.Thread.Sleep(1500);
        }

        private void PlayingRound()
        {
            DisplayOptionsToSelectFrom();
            var KeySelectedByUser = Console.ReadKey(true).Key;

            if (KeySelectedByUser >= ConsoleKey.D1 && KeySelectedByUser <= ConsoleKey.D3)                
                PerformOperationsForSelectedOption(KeySelectedByUser);

            else if (KeySelectedByUser == ConsoleKey.D4)
                TryingToStopGame();

            else if (KeySelectedByUser == ConsoleKey.D5)
                DisplayMessage("Czas: " + GetGameTimeInTextVersion());

            else
                DisplayMessage("Można wybrać tylko opcje z numerami 1-5!");
        }

        private void DisplayOptionsToSelectFrom()
        {
            Console.WriteLine("\n" + "Wybierz opcje: ");
            Console.WriteLine("1. Odsłoń pole");
            Console.WriteLine("2. Ustaw chorągiewkę");
            Console.WriteLine("3. Usuń chorągiewkę");
            Console.WriteLine("4. Zakończ rozgrywkę");
            Console.WriteLine("5. Pokaż upłynięty czas");
        }

        private void PerformOperationsForSelectedOption(ConsoleKey KeySelectedByUser)
        {
            UserInputOfFieldIndexes();
            if (CheckSelectedIndexes())
            {
                SetIndexesOfField();
                switch (KeySelectedByUser)
                {
                    case ConsoleKey.D1: PlayerRevealsField(); break;
                    case ConsoleKey.D2: PlayerSetsFlagOnField(); break;
                    case ConsoleKey.D3: PlayerRemovesFlagOnField(); break;
                }
            }

            else
                DisplayMessage("Wprowadzono nieprawidłowe wartości!");   
        }

        private void PlayerRevealsField()
        {
            if (IsntSelectedFieldMined())
            {
                UnveilingFieldOrSeveralFields();
                StatusOfGame = DidPlayerRevealAllEmptyFields() ? GameStatus.PlayerWin : GameStatus.DuringGame;
            }

            else
            {
                UnveilingAllMines();
                StatusOfGame = GameStatus.PlayerLost;
            }
        }

        private bool IsntSelectedFieldMined() =>
            ActualBoardContent[IndexesOfField.Item1, IndexesOfField.Item2] != '*';

        private void UnveilingFieldOrSeveralFields()
        {
            if (DisplayNumberOfMinesInField(IndexesOfField.Item1, IndexesOfField.Item2) == 0)
            {
                SquareOfExposedFields.Left = IndexesOfField.Item2 - 1;
                SquareOfExposedFields.Top = IndexesOfField.Item1 - 1;
                SquareOfExposedFields.Right = IndexesOfField.Item2 + 1;
                SquareOfExposedFields.Bottom = IndexesOfField.Item1 + 1;
                LoadNumberOfMinesIntoDisplayedBoard();
            }
        }

        private bool DidPlayerRevealAllEmptyFields()
        {
            for (int i = 0; i < VerticalDimensionOfBoard; ++i)
                for (int j = 0; j < HorizontalDimensionOfBoard; ++j)
                    if (ActualBoardContent[i, j] == 'O' && DisplayedBoard[i, j] == '■')
                        return false;

            return true;
        }

        private void UnveilingAllMines()
        {
            for (int i = 0; i < VerticalDimensionOfBoard; ++i)
                for (int j = 0; j < HorizontalDimensionOfBoard; ++j)
                    if (ActualBoardContent[i, j] == '*')
                        DisplayedBoard[i, j] = '*';
        }       

        private void PlayerSetsFlagOnField()
        {
            if (DisplayedBoard[IndexesOfField.Item1, IndexesOfField.Item2] == '■')
                DisplayedBoard[IndexesOfField.Item1, IndexesOfField.Item2] = 'C';                

            else
                DisplayMessage("Tutaj nie można wstawić flagi!");
        }

        private void PlayerRemovesFlagOnField()
        {
            if (DisplayedBoard[IndexesOfField.Item1, IndexesOfField.Item2] == 'C')
                DisplayedBoard[IndexesOfField.Item1, IndexesOfField.Item2] = '■';

            else
                DisplayMessage("Na tym polu nie ma flagi!");
        }

        private void TryingToStopGame()
        {
            Console.WriteLine("Czy napewno chcesz zakończyć rozgrywkę?");
            Console.WriteLine("(naciśnij enter aby potwierdzić, bądź inny klawisz aby anulować)");

            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                StatusOfGame = GameStatus.Break;
        }

        private string GetGameTimeInTextVersion()
        {
            if (GameTime.ElapsedMilliseconds < 60000)
                return (GameTime.ElapsedMilliseconds / 1000).ToString() + " s";

            else
                return (GameTime.ElapsedMilliseconds / 60000).ToString() + " m " + (GameTime.ElapsedMilliseconds % 60000 / 1000).ToString() + " s";
        }

        private void DisplayGameResult()
        {
            if (StatusOfGame == GameStatus.PlayerWin)
                Console.WriteLine("Brawo! Wygrałeś!");

            if (StatusOfGame == GameStatus.PlayerLost)
                Console.WriteLine("Niestety! Nie udało ci się!");

            Console.WriteLine("Twój czas: " + GetGameTimeInTextVersion());
        }   
    }
}
