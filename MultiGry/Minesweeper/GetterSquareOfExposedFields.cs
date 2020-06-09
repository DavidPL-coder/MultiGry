using System;

namespace MultiGry.Minesweeper
{
    class GetterSquareOfExposedFields
    {
        private readonly Tuple<int, int> selectedIndexes;
        private Rect square;
        private readonly Random numberGenerator; 

        public GetterSquareOfExposedFields(Tuple<int, int> selectedIndexes)
        {
            this.selectedIndexes = selectedIndexes;
            numberGenerator = new Random();
        }

        public Rect GetSquareAroundSelectedField()
        {
            square.Left = selectedIndexes.Item2 - 1;
            square.Top = selectedIndexes.Item1 - 1;
            square.Right = selectedIndexes.Item2 + 1;
            square.Bottom = selectedIndexes.Item1 + 1;

            return square;
        }

        public Rect GetRandomSquare()
        {
            SetTopAndBottomOfSquare();
            SetLeftAndRightOfSquare();

            return square;
        }

        private void SetTopAndBottomOfSquare()
        {
            if (AreFieldsToBeUncoveredUpwards())
            {
                square.Top = selectedIndexes.Item1 - 2;
                square.Bottom = selectedIndexes.Item1;
            }

            else
            {
                square.Top = selectedIndexes.Item1;
                square.Bottom = selectedIndexes.Item1 + 2;
            }
        }

        private bool AreFieldsToBeUncoveredUpwards() =>
            numberGenerator.Next(2) == 1;

        private void SetLeftAndRightOfSquare()
        {
            if (AreFieldsToBeUncoveredLeft())
            {
                square.Left = selectedIndexes.Item2 - 2;
                square.Right = selectedIndexes.Item2;
            }

            else
            {
                square.Left = selectedIndexes.Item2;
                square.Right = selectedIndexes.Item2 + 2;
            }
        }

        private bool AreFieldsToBeUncoveredLeft() =>
            numberGenerator.Next(2) == 1;
    }
}
