using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class MinesSetter
    {
        private char[,] ActualBoardContent;
        private List<Tuple<int, int>> CoordinatesOfMinesDrawn;
        private readonly Tuple<int, int> IndexesOfField;
        private readonly int VerticalDimensionOfBoard;
        private readonly int HorizontalDimensionOfBoard;
        private Tuple<int, int> CoordinatePair;

        public MinesSetter(MinesweeperGame Game)
        {
            ActualBoardContent = Game.ActualBoardContent;
            IndexesOfField = Game.SelectedFieldIndexes.TupleOfIndexes;

            VerticalDimensionOfBoard = MinesweeperGame.VerticalDimensionOfBoard;
            HorizontalDimensionOfBoard = MinesweeperGame.HorizontalDimensionOfBoard;

            CoordinatesOfMinesDrawn = new List<Tuple<int, int>>();
        }

        public void SetMinesOnBoard(int NumberOfMines)
        {
            for (int i = 0; i < NumberOfMines; ++i)
            {
                SetCoordinatePair();
                if (CanMineBeInThisField())
                {
                    ActualBoardContent[CoordinatePair.Item1, CoordinatePair.Item2] = '*';
                    CoordinatesOfMinesDrawn.Add(CoordinatePair);
                }

                else
                    --i;
            }
        }

        private void SetCoordinatePair()
        {
            var NumberGenerator = new Random();
            int Vertical = NumberGenerator.Next(0, VerticalDimensionOfBoard);
            int Horizontal = NumberGenerator.Next(0, HorizontalDimensionOfBoard);
            CoordinatePair = Tuple.Create(Vertical, Horizontal);
        }

        private bool CanMineBeInThisField() => 
            !CoordinatesOfMinesDrawn.Contains(CoordinatePair) &&
            !CoordinatesOfMinesDrawn.Contains(IndexesOfField);
    }
}
