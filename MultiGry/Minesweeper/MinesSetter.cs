using System;
using System.Collections.Generic;

namespace MultiGry.Minesweeper
{
    public class MinesSetter
    {        
        private List<Tuple<int, int>> coordinatesOfMinesDrawn;
        private readonly Tuple<int, int> indexesOfField;
        private readonly INumberGenerator numberGenerator;

        public MinesSetter(Tuple<int, int> indexesOfField, INumberGenerator numberGenerator)
        {
            this.indexesOfField = indexesOfField;
            this.numberGenerator = numberGenerator;
            coordinatesOfMinesDrawn = new List<Tuple<int, int>>();
        }

        public void SetMinesOnBoard(char[,] actualBoardContent, int numberOfMines)
        {
            for (int i = 0; i < numberOfMines; ++i)
            {
                var coordinatePair = GetCoordinatePair();
                if (CanMineBeInThisField(coordinatePair))
                {
                    actualBoardContent[coordinatePair.Item1, coordinatePair.Item2] = MinesweeperGame.BombSign;
                    coordinatesOfMinesDrawn.Add(coordinatePair);
                }

                else
                    --i;
            }
        }

        private Tuple<int, int> GetCoordinatePair()
        {
            int vertical = numberGenerator.Next(0, MinesweeperGame.VerticalDimensionOfBoard);
            int horizontal = numberGenerator.Next(0, MinesweeperGame.HorizontalDimensionOfBoard);
            return Tuple.Create(vertical, horizontal);
        }

        private bool CanMineBeInThisField(Tuple<int, int> coordinatePair) => 
            !coordinatesOfMinesDrawn.Contains(coordinatePair) &&
            !coordinatePair.Equals(indexesOfField);
    }
}
