using System;

namespace MultiGry.Minesweeper
{
    public class ManagerOfFieldIndexes
    {
        private string selectedIndexesInTextVersion;
        public Tuple<int, int> TupleOfIndexes { private set; get; }
        public int VerticalIndex => TupleOfIndexes.Item1;
        public int HorizontalIndex => TupleOfIndexes.Item2;

        public void UserInputOfFieldIndexesInTextVersion()
        {
            Console.Write("Wybierz pole (podaj pionowy indeks oraz po spacji poziomy indeks): ");
            selectedIndexesInTextVersion = Console.ReadLine();
        }

        public bool CheckIndexesInTextVersion()
        {
            int indexCounter = 0;
            foreach (var sign in selectedIndexesInTextVersion)
            {
                if (CheckIsValueNumberBetween1And8(sign))
                    ++indexCounter;

                else if (sign != ' ' && sign != '\t')
                    return false;
            }
            return indexCounter == 2;
        }

        private bool CheckIsValueNumberBetween1And8(char value) =>
            (value - '0') >= 1 && (value - '0') <= 8;

        public void SetTupleOfIndexes()
        {
            int verticalIndex = -1, horizontalIndex = -1;
            foreach (var sign in selectedIndexesInTextVersion)
                if (CheckIsValueNumberBetween1And8(sign))
                {
                    // thanks to this operation it will be possible to use 
                    // the resulting value as an array index
                    if (verticalIndex == -1)
                        verticalIndex = sign - '1'; 

                    else
                        horizontalIndex = sign - '1';
                }

            TupleOfIndexes = Tuple.Create(verticalIndex, horizontalIndex);
        }
    }
}
