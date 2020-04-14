using System;

namespace MultiGry.Hangman
{
    class GetterRandomWordAndDisplayedCharacters
    {
        public string RandomWord { private set; get; }
        public char[] DisplayedCharacters { private set; get; }

        public GetterRandomWordAndDisplayedCharacters()
        {
            SetRandomWord();
            SetDefaultValueForDisplayedCharacters();
        }

        private void SetRandomWord()
        {
            string[] Words = GetWordsFromResourceFile();
            var GeneratorNumber = new Random();
            int RandomNumberWord = GeneratorNumber.Next(0, Words.Length);
            RandomWord = Words[RandomNumberWord];
        }

        private string[] GetWordsFromResourceFile()
        {
            string HangmanWords = Properties.Resources.HangmanGameWords;
            var Words = HangmanWords.Split(new string[] { "\r\n", " ", "\t" },
                                           StringSplitOptions.RemoveEmptyEntries);
            return Words;
        }

        // in place of unguessed letters there is an underline:
        private void SetDefaultValueForDisplayedCharacters()
        {
            DisplayedCharacters = new char[RandomWord.Length];
            for (int i = 0; i < RandomWord.Length; ++i)
                DisplayedCharacters[i] = '_';           
        }
    }
}
