using System;

namespace MultiGry.FilesEncryptor
{
    static class RequestDisplay
    {
        static public void DisplayRequestForFilePath(string MessageToUser) =>
            Console.WriteLine(MessageToUser);

        static public void DisplayRequestForFilePath() =>
            Console.WriteLine("Podaj nazwę pliku (lub ścieżkę względną): ");
    }
}
