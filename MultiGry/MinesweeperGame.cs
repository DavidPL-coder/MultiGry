using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class MinesweeperGame : IMenuOption
    {
        public string NameOption => "Saper";
        private char[,] DisplayedBoard;
        private char[,] ActualBoardContent;
        private const int VerticalDimensionOfBoard = 8;
        private const int HorizontalDimensionOfBoard = 8;
        private ConsoleColor RightTextColor;
        private bool IsThereFirstRound;
        private List<Tuple<int, int>> CoordinatesOfMinesDrawn;
        private Random NumberGenerator;
        private Tuple<int, int> IndexesOfSelectedField;
        private string[] SelectedIndexesInTextVersion;
        struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        Rect StartSquareOfExposedFields;

        //private int Top;
        //private int Left;
        //private int Bottom;
        //private int Right;

        public OptionsCategory OptionExecuting()
        {
            RightTextColor = Console.ForegroundColor;
            IsThereFirstRound = true;
            SetBoard();
            CoordinatesOfMinesDrawn = new List<Tuple<int, int>>();
            NumberGenerator = new Random();

            while (IsGameStillGoingOn())
            {
                DisplayBoard();
                try
                {
                    if (IsThereFirstRound)
                        StartingGame();

                    else
                        PlayingRound();
                }
                catch 
                {
                    Console.WriteLine("sdhdshhdfshdshf");
                }
                

                Console.ReadKey();
            }

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private bool IsGameStillGoingOn()
        {
            return true;
        }

        private void PlayingRound()
        {
            Console.WriteLine();
        }

        private void StartingGame()
        {
            DisplayFieldIndexQuery();
            SelectedIndexesInTextVersion = Console.ReadLine().Split();

            if (CheckSelectedIndexes())
            {
                SetIndexesOfSelectedField();
                SetMinesOnBoard();

                SetTopAndBottomOfSquare(AreFieldsToBeUncoveredUpwards: NumberGenerator.Next(0, 2) == 1);
                SetLeftAndRightOfSquare(AreFieldsToBeUncoveredLeft: NumberGenerator.Next(0, 2) == 1);

                //LoadEmptyFieldsIntoDisplayedBoard();

                LoadNumberOfMinesIntoDisplayedBoard();

                DisplayBoard();
                IsThereFirstRound = false;
            }

            else
            {
                Console.WriteLine("Wprowadzono nieprawidłowe wartości!");
                System.Threading.Thread.Sleep(1500);
            }

        }

        private void LoadNumberOfMinesIntoDisplayedBoard()
        {
            for (int i = StartSquareOfExposedFields.Top; i <= StartSquareOfExposedFields.Bottom; ++i)
            {
                for (int j = StartSquareOfExposedFields.Left; j <= StartSquareOfExposedFields.Right; ++j)
                {
                    int NumberDisplayedInGivenField = 0;
                    if (ActualBoardContent[i, j] != '*')
                    {
                        if (i - 1 >= 0 && j - 1 >= 0 && ActualBoardContent[i - 1, j - 1] == '*')
                            ++NumberDisplayedInGivenField;

                        if (i - 1 >= 0 && ActualBoardContent[i - 1, j] == '*')
                            ++NumberDisplayedInGivenField;

                        if (i - 1 >= 0 && j + 1 <= 7 && ActualBoardContent[i - 1, j + 1] == '*')
                            ++NumberDisplayedInGivenField;

                        if (j - 1 >= 0 && ActualBoardContent[i, j - 1] == '*')
                            ++NumberDisplayedInGivenField;

                        if (j + 1 <= 7 && ActualBoardContent[i, j + 1] == '*')
                            ++NumberDisplayedInGivenField;

                        if (i + 1 <= 7 && j - 1 >= 0 && ActualBoardContent[i + 1, j - 1] == '*')
                            ++NumberDisplayedInGivenField;

                        if (i + 1 <= 7 && ActualBoardContent[i + 1, j] == '*')
                            ++NumberDisplayedInGivenField;

                        if (i + 1 <= 7 && j + 1 <= 7 && ActualBoardContent[i + 1, j + 1] == '*')
                            ++NumberDisplayedInGivenField;
                    }

                    if (NumberDisplayedInGivenField != 0)
                        DisplayedBoard[i, j] = NumberDisplayedInGivenField.ToString()[0];

                    else
                        DisplayedBoard[i, j] = 'O';
                }
            }
        }

        //private void LoadEmptyFieldsIntoDisplayedBoard()
        //{
        //    for (int i = StartSquareOfExposedFields.Top; i <= StartSquareOfExposedFields.Bottom; ++i)
        //    {
        //        for (int j = StartSquareOfExposedFields.Left; j <= StartSquareOfExposedFields.Right; ++j)
        //        {
        //            if (ActualBoardContent[i, j] != '*')
        //                DisplayedBoard[i, j] = ActualBoardContent[i, j];
        //        }
        //    }
        //}

        private void SetLeftAndRightOfSquare(bool AreFieldsToBeUncoveredLeft)
        {
            if (AreFieldsToBeUncoveredLeft)
            {
                StartSquareOfExposedFields.Left = ( IndexesOfSelectedField.Item2 - 2 ) < 0 ? 0 : ( IndexesOfSelectedField.Item2 - 2 );
                StartSquareOfExposedFields.Right = IndexesOfSelectedField.Item2;
            }

            else
            {
                StartSquareOfExposedFields.Left = IndexesOfSelectedField.Item2;
                StartSquareOfExposedFields.Right = ( IndexesOfSelectedField.Item2 + 2 ) > 7 ? 7 : ( IndexesOfSelectedField.Item2 + 2 );
            }
        }

        private void SetTopAndBottomOfSquare(bool AreFieldsToBeUncoveredUpwards)
        {
            if (AreFieldsToBeUncoveredUpwards)
            {
                StartSquareOfExposedFields.Top = ( IndexesOfSelectedField.Item1 - 2 ) < 0 ? 0 : ( IndexesOfSelectedField.Item1 - 2 );
                StartSquareOfExposedFields.Bottom = IndexesOfSelectedField.Item1;
            }

            else
            {
                StartSquareOfExposedFields.Top = IndexesOfSelectedField.Item1;
                StartSquareOfExposedFields.Bottom = ( IndexesOfSelectedField.Item1 + 2 ) > 7 ? 7 : ( IndexesOfSelectedField.Item1 + 2 );
            }
        }

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

        private void SetIndexesOfSelectedField()
        {
            IndexesOfSelectedField = Tuple.Create(int.Parse(SelectedIndexesInTextVersion[0]) - 1,
                                                  int.Parse(SelectedIndexesInTextVersion[1]) - 1);
        }

        private bool CanMineBeInThisField(Tuple<int, int> CoordinatePair)
        {
            for (int j = 0; j < CoordinatesOfMinesDrawn.Count; ++j)
                if (Equals(CoordinatesOfMinesDrawn[j], CoordinatePair) || Equals(CoordinatesOfMinesDrawn[j], IndexesOfSelectedField))
                    return false;

            return true;
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

        private void DisplayBoard()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            DisplayVerticalBoardLines();

            Console.ForegroundColor = RightTextColor;
        }

        private void DisplayVerticalBoardLines()
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8");
            Console.WriteLine("  - - - - - - - -");

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
                case '1': Console.ForegroundColor = ConsoleColor.Blue; break;
                case '2': Console.ForegroundColor = ConsoleColor.Green; break;
                case '3': Console.ForegroundColor = ConsoleColor.Red; break;
                case '4': Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case '5':
                case '6':
                case '7':
                case '8':
                    Console.ForegroundColor = ConsoleColor.Magenta; break;
            }
        }

        private void DisplayFieldIndexQuery()
        {
            Console.ForegroundColor = RightTextColor;
            Console.Write("Wybierz pole (podaj pionowy indeks oraz po spacji poziomy indeks): ");
        }
    }
}
