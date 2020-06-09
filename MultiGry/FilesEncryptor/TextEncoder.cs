using System.Text;

namespace MultiGry.FilesEncryptor
{
    // for encrypting files, the Caesar cipher was used
    // more information about this: https://pl.wikipedia.org/wiki/Szyfr_Cezara
    public class TextEncoder
    {
        private enum LetterType
        {
            Lowercase, CapitalLetter, Digit, Other
        }

        private StringBuilder EncryptedText;
        private EncoderOperations Operation;
        private int IndexCharacter;
        private LetterType CharacterType;
        private char InitCharacter;
        private char FinalCharacter;
        private int NumberOfCharactersInAllocation;
        private const int CaesarCipherKey = 8;

        public string EncryptingText(string Text)
        {
            EncryptedText = new StringBuilder(Text);
            Operation = EncoderOperations.Encryption;
            return TextEncoding();
        }

        public string DecryptingText(string Text)
        {
            EncryptedText = new StringBuilder(Text);
            Operation = EncoderOperations.Decryption;
            return TextEncoding();
        }

        private string TextEncoding()
        {
            for (IndexCharacter = 0; IndexCharacter < EncryptedText.Length; )
            {
                SetEncryptionRangeForCharacter();

                if (CharacterType != LetterType.Other)
                    CharacterEncoding();

                ++IndexCharacter;
            }

            return EncryptedText.ToString();
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
            CharacterType = GetCharacterType(EncryptedText[IndexCharacter]);

            switch (CharacterType)
            {
                case LetterType.Lowercase:
                    SetValuesNecessaryForEncoding('a', 'z', 26);
                    break;

                case LetterType.CapitalLetter:
                    SetValuesNecessaryForEncoding('A', 'Z', 26);
                    break;

                case LetterType.Digit:
                    SetValuesNecessaryForEncoding('0', '9', 10);
                    break;
            }
        }

        private void SetValuesNecessaryForEncoding(char FirstCharacter, 
                               char LastCharacter, int NumberOfCharacters)
        {
            InitCharacter = FirstCharacter;
            FinalCharacter = LastCharacter;
            NumberOfCharactersInAllocation = NumberOfCharacters;
        }

        private void CharacterEncoding()
        {           
            if (Operation == EncoderOperations.Encryption)
                CharacterEncrypting();

            else
                CharacterDecryption();
        }

        private void CharacterEncrypting()
        {
            if (EncryptedText[IndexCharacter] + CaesarCipherKey <= FinalCharacter)
                EncryptedText[IndexCharacter] += (char) CaesarCipherKey;

            else
                EncryptedText[IndexCharacter] += (char) (CaesarCipherKey - 
                                                 NumberOfCharactersInAllocation);
        }

        private void CharacterDecryption()
        {
            if (EncryptedText[IndexCharacter] + (-CaesarCipherKey) >= InitCharacter)
                EncryptedText[IndexCharacter] += unchecked((char) -CaesarCipherKey);

            else
                EncryptedText[IndexCharacter] += (char) (-CaesarCipherKey + 
                                                 NumberOfCharactersInAllocation);
        } 
    }
}
