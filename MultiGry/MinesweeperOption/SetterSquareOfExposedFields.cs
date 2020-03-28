using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    class SetterSquareOfExposedFields
    {
        private Tuple<int, int> SelectedIndexes;
        private Rect Square;
        private Random NumberGenerator; 

        public SetterSquareOfExposedFields(Tuple<int, int> SelectedIndexes)
        {
            this.SelectedIndexes = SelectedIndexes;
            NumberGenerator = new Random();
        }

        public Rect GetSquareAroundSelectedField()
        {
            Square.Left = SelectedIndexes.Item2 - 1;
            Square.Top = SelectedIndexes.Item1 - 1;
            Square.Right = SelectedIndexes.Item2 + 1;
            Square.Bottom = SelectedIndexes.Item1 + 1;

            return Square;
        }

        public Rect GetRandomSquare()
        {
            SetTopAndBottomOfSquare();
            SetLeftAndRightOfSquare();

            return Square;
        }

        private void SetTopAndBottomOfSquare()
        {
            if (AreFieldsToBeUncoveredUpwards())
            {
                Square.Top = SelectedIndexes.Item1 - 2;
                Square.Bottom = SelectedIndexes.Item1;
            }

            else
            {
                Square.Top = SelectedIndexes.Item1;
                Square.Bottom = SelectedIndexes.Item1 + 2;
            }
        }

        private bool AreFieldsToBeUncoveredUpwards() =>
            NumberGenerator.Next(2) == 1;

        private void SetLeftAndRightOfSquare()
        {
            if (AreFieldsToBeUncoveredLeft())
            {
                Square.Left = SelectedIndexes.Item2 - 2;
                Square.Right = SelectedIndexes.Item2;
            }

            else
            {
                Square.Left = SelectedIndexes.Item2;
                Square.Right = SelectedIndexes.Item2 + 2;
            }
        }

        private bool AreFieldsToBeUncoveredLeft() =>
            NumberGenerator.Next(2) == 1;
    }
}
