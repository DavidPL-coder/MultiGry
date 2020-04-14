using System;
using System.Collections.Generic;

namespace MultiGry.Minesweeper
{
    class MinesSetter
    {        
        private char[,] ActualBoardContent;
        private List<Tuple<int, int>> CoordinatesOfMinesDrawn;
        private readonly Tuple<int, int> IndexesOfField;
        private readonly int VerticalDimension;
        private readonly int HorizontalDimension;
        private readonly char BombSign;
        private Tuple<int, int> CoordinatePair;

        public MinesSetter(MinesweeperGame Game)
        {
            ActualBoardContent = Game.ActualBoardContent;
            IndexesOfField = Game.SelectedFieldIndexes.TupleOfIndexes;

            VerticalDimension = MinesweeperGame.VerticalDimensionOfBoard;
            HorizontalDimension = MinesweeperGame.HorizontalDimensionOfBoard;
            BombSign = MinesweeperGame.BombSign;

            CoordinatesOfMinesDrawn = new List<Tuple<int, int>>();
        }

        public void SetMinesOnBoard()
        {
            for (int i = 0; i < MinesweeperGame.NumberOfMines; ++i)
            {
                SetCoordinatePair();
                if (CanMineBeInThisField())
                {
                    ActualBoardContent[CoordinatePair.Item1, 
                                       CoordinatePair.Item2] = BombSign;
                    CoordinatesOfMinesDrawn.Add(CoordinatePair);
                }

                else
                    --i;
            }
        }

        private void SetCoordinatePair()
        {
            var NumberGenerator = new Random();
            int Vertical = NumberGenerator.Next(0, VerticalDimension);
            int Horizontal = NumberGenerator.Next(0, HorizontalDimension);
            CoordinatePair = Tuple.Create(Vertical, Horizontal);
        }

        private bool CanMineBeInThisField() => 
            !CoordinatesOfMinesDrawn.Contains(CoordinatePair) &&
            !CoordinatesOfMinesDrawn.Contains(IndexesOfField);
    }
}
