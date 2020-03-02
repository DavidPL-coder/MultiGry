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

        public OptionsCategory OptionExecuting()
        {
            SetDefaults();
            while (IsGameStillGoingOn())
            {
                DisplayBoard();
                if (IsThereFirstRound)
                    StartingGame();

                else
                    PlayingRound();

                Console.ReadKey();
            }

            var ProgramExecution = new DecisionOnFurtherCourseOfProgram(this);
            return ProgramExecution.UserDecidesWhatToDoNext();
        }

        private void SetDefaults()
        {
            RightTextColor = Console.ForegroundColor;
            IsThereFirstRound = true;
            SetBoard();
            CoordinatesOfMinesDrawn = new List<Tuple<int, int>>();
            NumberGenerator = new Random();
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

        private bool IsGameStillGoingOn()
        {
            return true;
        }

        private void DisplayBoard()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();
            Console.WriteLine("  1 2 3 4 5 6 7 8");
            Console.WriteLine("  - - - - - - - -");
            DisplayVerticalBoardLines();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(ActualBoardContent[i, j] + " ");
                }

                Console.WriteLine();
            }

            Console.ForegroundColor = RightTextColor;
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

        private void StartingGame()
        {
            DisplayFieldIndexQuery();
            SelectedIndexesInTextVersion = Console.ReadLine().Split();

            if (CheckSelectedIndexes())
                PreparingToPlayGame();

            else
                DisplayMessageAboutWrongValues();
        }

        private void DisplayMessageAboutWrongValues()
        {
            Console.WriteLine("Wprowadzono nieprawidłowe wartości!");
            System.Threading.Thread.Sleep(1500);
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
            SetIndexesOfSelectedField();
            SetMinesOnBoard();

            SetTopAndBottomOfSquare(AreFieldsToBeUncoveredUpwards: NumberGenerator.Next(0, 2) == 1);
            SetLeftAndRightOfSquare(AreFieldsToBeUncoveredLeft: NumberGenerator.Next(0, 2) == 1);

            LoadNumberOfMinesIntoDisplayedBoard();

            IsThereFirstRound = false;
        }

        private void SetIndexesOfSelectedField()
        {
            IndexesOfSelectedField = Tuple.Create(int.Parse(SelectedIndexesInTextVersion[0]) - 1,
                                                  int.Parse(SelectedIndexesInTextVersion[1]) - 1);
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

        private bool CanMineBeInThisField(Tuple<int, int> CoordinatePair)
        {
            for (int j = 0; j < CoordinatesOfMinesDrawn.Count; ++j)
                if (Equals(CoordinatesOfMinesDrawn[j], CoordinatePair) || Equals(CoordinatesOfMinesDrawn[j], IndexesOfSelectedField))
                    return false;

            return true;
        }

        private void SetTopAndBottomOfSquare(bool AreFieldsToBeUncoveredUpwards)
        {
            if (AreFieldsToBeUncoveredUpwards)
            {
                StartSquareOfExposedFields.Top = (IndexesOfSelectedField.Item1 - 2) < 0 ? 0 : (IndexesOfSelectedField.Item1 - 2);
                StartSquareOfExposedFields.Bottom = IndexesOfSelectedField.Item1;
            }

            else
            {
                StartSquareOfExposedFields.Top = IndexesOfSelectedField.Item1;
                StartSquareOfExposedFields.Bottom = (IndexesOfSelectedField.Item1 + 2) > VerticalDimensionOfBoard - 1 ?
                                                     VerticalDimensionOfBoard - 1 : (IndexesOfSelectedField.Item1 + 2);
            }
        }

        private void SetLeftAndRightOfSquare(bool AreFieldsToBeUncoveredLeft)
        {
            if (AreFieldsToBeUncoveredLeft)
            {
                StartSquareOfExposedFields.Left = (IndexesOfSelectedField.Item2 - 2) < 0 ? 0 : (IndexesOfSelectedField.Item2 - 2);
                StartSquareOfExposedFields.Right = IndexesOfSelectedField.Item2;
            }

            else
            {
                StartSquareOfExposedFields.Left = IndexesOfSelectedField.Item2;
                StartSquareOfExposedFields.Right = (IndexesOfSelectedField.Item2 + 2) > HorizontalDimensionOfBoard - 1 ?
                                                    HorizontalDimensionOfBoard - 1 : (IndexesOfSelectedField.Item2 + 2);
            }
        }

        private void LoadNumberOfMinesIntoDisplayedBoard()
        {
            for (int VerticalIndex = StartSquareOfExposedFields.Top; VerticalIndex <= StartSquareOfExposedFields.Bottom; ++VerticalIndex)
                for (int HorizontalIndex = StartSquareOfExposedFields.Left; HorizontalIndex <= StartSquareOfExposedFields.Right; ++HorizontalIndex)
                {
                    int NumberDisplayedInGivenField = 0;
                    if (ActualBoardContent[VerticalIndex, HorizontalIndex] != '*')
                    {
                        NumberDisplayedInGivenField = CalculateHowManyMinesAreAroundField(VerticalIndex, HorizontalIndex);

                        if (NumberDisplayedInGivenField != 0)
                            DisplayedBoard[VerticalIndex, HorizontalIndex] = NumberDisplayedInGivenField.ToString()[0];

                        else
                            DisplayedBoard[VerticalIndex, HorizontalIndex] = 'O';
                    }
                }
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

        private bool IsThereFieldWithSuchIndex(int i, int j)
        {
            return i >= 0 &&
                   i < VerticalDimensionOfBoard &&
                   j >= 0 &&
                   j < HorizontalDimensionOfBoard;
        }

        private void DisplayFieldIndexQuery()
        {
            Console.ForegroundColor = RightTextColor;
            Console.Write("Wybierz pole (podaj pionowy indeks oraz po spacji poziomy indeks): ");
        }

        private void PlayingRound()
        {
            Console.WriteLine("\n" + "Wybierz opcje: ");
            Console.WriteLine("1. Odkryj pole");
            Console.WriteLine("2. Ustaw chorągiewkę");
            Console.WriteLine("3. Usuń chorągiewkę");
        }
    }
}
