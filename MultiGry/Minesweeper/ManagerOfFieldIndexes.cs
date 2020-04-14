using System;

namespace MultiGry.Minesweeper
{
    class ManagerOfFieldIndexes
    {
        private string SelectedIndexesInTextVersion;
        public Tuple<int, int> TupleOfIndexes { private set; get; }
        public int Vertical => TupleOfIndexes.Item1;
        public int Horizontal => TupleOfIndexes.Item2;

        public void UserInputOfFieldIndexesInTextVersion()
        {
            Console.Write("Wybierz pole (podaj pionowy indeks oraz po spacji " +
                          "poziomy indeks): ");

            SelectedIndexesInTextVersion = Console.ReadLine();
        }

        public bool CheckIndexesInTextVersion()
        {
            int IndexCounter = 0;
            foreach (var item in SelectedIndexesInTextVersion)
            {
                if (CheckIsValueNumberBetween1And8(item))
                    ++IndexCounter;

                else if (item != ' ' && item != '\t')
                    return false;
            }
            return IndexCounter == 2;
        }

        private bool CheckIsValueNumberBetween1And8(char Value) =>
            (Value - '0') >= 1 && (Value - '0') <= 8;

        public void SetTupleOfIndexes()
        {
            int VerticalIndex = -1, HorizontalIndex = -1;
            foreach (char item in SelectedIndexesInTextVersion)
                if (CheckIsValueNumberBetween1And8(item))
                {
                    // thanks to this operation it will be possible to use 
                    // the resulting value as an array index
                    if (VerticalIndex == -1)
                        VerticalIndex = item - '1'; 

                    else
                        HorizontalIndex = item - '1';
                }

            TupleOfIndexes = Tuple.Create(VerticalIndex, HorizontalIndex);
        }
    }
}
