using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiGry
{
    // for encrypting files, the Caesar cipher was used
    class TextEncoder
    {
        string EncryptedText;
        public enum Operations
        {
            Encryption, Decryption
        }
        Operations Operation;
        int IndexCharacter;
        private enum LetterType
        {
            Lowercase, CapitalLetter, Digit, Other
        }
        LetterType CharacterType;
        char InitialCharacter;
        char FinalCharacter;
        int NumberOfCharactersInAllocation;
        private const int CaesarCipherKey = 8;

        public string EncryptingText(string Text)
        {
            EncryptedText = Text;
            Operation = Operations.Encryption;
            return TextEncoding();
        }

        public string DecryptingText(string Text)
        {
            EncryptedText = Text;
            Operation = Operations.Decryption;
            return TextEncoding();
        }

        private string TextEncoding()
        {
            for (IndexCharacter = 0; IndexCharacter < EncryptedText.Length; ++IndexCharacter)
            {
                CharacterType = GetCharacterType(EncryptedText[IndexCharacter]);
                SetEncryptionRangeForCharacter();

                if (CharacterType != LetterType.Other)
                    CharacterEncoding();
            }

            return EncryptedText;
        }

        private LetterType GetCharacterType(char character)
        {
            if (character >= 'a' && character <= 'z')
                return LetterType.Lowercase;

            if (character >= 'A' && character <= 'Z')
                return LetterType.CapitalLetter;

            if (character >= '0' && character <= '9')
                return LetterType.Digit;

            return LetterType.Other;
        }

        private void SetEncryptionRangeForCharacter()
        {
            switch (CharacterType)
            {
                case LetterType.Lowercase:
                    InitialCharacter = 'a';
                    FinalCharacter = 'z';
                    NumberOfCharactersInAllocation = 26;
                    break;

                case LetterType.CapitalLetter:
                    InitialCharacter = 'A';
                    FinalCharacter = 'Z';
                    NumberOfCharactersInAllocation = 26;
                    break;

                case LetterType.Digit:
                    InitialCharacter = '0';
                    FinalCharacter = '9';
                    NumberOfCharactersInAllocation = 10;
                    break;
            }
        }

        private void CharacterEncoding()
        {           
            if (Operation == Operations.Encryption)
                CharacterEncrypting();

            else
                CharacterDecryption();
        }

        private void CharacterEncrypting()
        {
            if (EncryptedText[IndexCharacter] + CaesarCipherKey <= FinalCharacter)
                ChangeOfCharacter(CaesarCipherKey);

            else
                ChangeOfCharacter(CaesarCipherKey - NumberOfCharactersInAllocation);
        }

        private void ChangeOfCharacter(int CipherKey)
        {
            var tmp = new StringBuilder(EncryptedText);
            tmp[IndexCharacter] += (char) CipherKey;
            EncryptedText = tmp.ToString();
        }

        private void CharacterDecryption()
        {
            if (EncryptedText[IndexCharacter] + (-CaesarCipherKey) >= InitialCharacter)
                ChangeOfCharacter(-CaesarCipherKey);

            else
                ChangeOfCharacter(-CaesarCipherKey + NumberOfCharactersInAllocation);
        } 
    }
}
