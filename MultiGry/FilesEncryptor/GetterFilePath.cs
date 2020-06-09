namespace MultiGry.FilesEncryptor
{
    static class GetterFilePath
    {
        public static string GetFilePathFromUser(IFakeConsole DummyConsole) =>
            DummyConsole.ReadLine() + ".txt";
    }
}
