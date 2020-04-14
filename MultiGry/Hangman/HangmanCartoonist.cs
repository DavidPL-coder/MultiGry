using System;

namespace MultiGry.Hangman
{
    class HangmanCartoonist
    {
        private string[] HangmanDrawing;
        public int DrawingLength => HangmanDrawing.Length;

        public void CreateHangmanDrawing()
        {
            HangmanDrawing = new string[]
            {
                @"  ___________",
                @"  |    |    |",
                @"  |    |    |",
                @"  |    |    |",
                @"  |    O    |",
                @"  |   /|\   |",
                @"  |  / | \  |",
                @"  |    |    |",
                @"  |    |    |",
                @"  |   / \   |",
                @"  |  /   \  |",
                @"  |         |",
                @"  |         |",
                @" /|\       /|\",
                @"/ | \     / | \"
            };
        }

        public void DisplayHangmanItems(int NumberOfUserErrors)
        {
            Console.WriteLine();
            for (int i = 0; i < NumberOfUserErrors; ++i)
                Console.WriteLine(HangmanDrawing[i]);
        }
    }
}
