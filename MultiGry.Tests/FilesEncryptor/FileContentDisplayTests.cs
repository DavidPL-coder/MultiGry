using System;
using System.IO;
using NUnit.Framework;
using Moq;
using System.Text;

namespace MultiGry.Tests
{
    [TestFixture]
    class FileContentDisplayTests
    {
        [SetUp]
        public void Setup()
        {
            // this makes it possible to support Windows-1250 encoding during testing
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Test]
        public void OpenFile_UserEntersPathToFile_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            string FakeFileContents = "1234567890" + Environment.NewLine + 
                                      "aąsślłzżźcć" + Environment.NewLine + 
                                      "AĄLŁCĆSŚOÓ";
            byte[] FakeFileBytes = Encoding.GetEncoding("Windows-1250").GetBytes(FakeFileContents);
            var MemoryStream = new MemoryStream(FakeFileBytes);

            var Output = new StringWriter();
            Console.SetOut(Output);

            // between the file name request and the contents of the file,
            // the console should be cleaned. I decided not to check if the console is being cleaned
            string ExpectedOutput = "Podaj nazwę pliku (lub ścieżkę względną): " + Environment.NewLine +
                                    "Zawartość pliku: " + Environment.NewLine +
                                    FakeFileContents + Environment.NewLine;

            var FileContentDisplay = new FilesEncryptor.FileContentDisplay(MockOfFile.Object,
                                                                           MockOfConsole.Object, 
                                                                           MemoryStream);

            FileContentDisplay.OpenFile();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void OpenFile_UserProvidesPathToFileThatDoesNotYetExist_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(false);

            var Output = new StringWriter();
            Console.SetOut(Output);

            string FileNameRequest = "Podaj nazwę pliku (lub ścieżkę względną): ";

            string MessageThatFileDoesNotExist = "Podany plik nie istnieje! Upewnij się czy " +
                                                 "wprowadziłeś odpowiednią ścieżkę!";

            string ExpectedOutput = FileNameRequest + Environment.NewLine +
                                    MessageThatFileDoesNotExist + Environment.NewLine;

            var FileContentDisplay = new FilesEncryptor.FileContentDisplay(MockOfFile.Object, 
                                                                           MockOfConsole.Object, null);

            FileContentDisplay.OpenFile();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void ReadEncryptedFile_UserEntersPathToFile_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            string FakeFileContents = "9012345678" + Environment.NewLine +
                                      "iąaśtłhżźkć" + Environment.NewLine +
                                      "IĄTŁKĆAŚWÓ";
            byte[] FakeFileBytes = Encoding.GetEncoding("Windows-1250").GetBytes(FakeFileContents);
            var MemoryStream = new MemoryStream(FakeFileBytes);

            var Output = new StringWriter();
            Console.SetOut(Output);

            string ExpectedOutput = "Podaj nazwę pliku (lub ścieżkę względną): " + Environment.NewLine +
                                    "Odszyfrowana zawartość pliku:" + Environment.NewLine +
                                    "1234567890" + Environment.NewLine +
                                    "aąsślłzżźcć" + Environment.NewLine +
                                    "AĄLŁCĆSŚOÓ" + Environment.NewLine;

            var FileContentDisplay = new FilesEncryptor.FileContentDisplay(MockOfFile.Object,
                                                                           MockOfConsole.Object,
                                                                           MemoryStream);

            FileContentDisplay.ReadEncryptedFile();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void ReadEncryptedFile_UserProvidesPathToFileThatDoesNotYetExist_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(false);

            var Output = new StringWriter();
            Console.SetOut(Output);

            string FileNameRequest = "Podaj nazwę pliku (lub ścieżkę względną): ";

            string MessageThatFileDoesNotExist = "Podany plik nie istnieje! Upewnij się czy " +
                                                 "wprowadziłeś odpowiednią ścieżkę!";

            string ExpectedOutput = FileNameRequest + Environment.NewLine +
                                    MessageThatFileDoesNotExist + Environment.NewLine;

            var FileContentDisplay = new FilesEncryptor.FileContentDisplay(MockOfFile.Object,
                                                                           MockOfConsole.Object, null);

            FileContentDisplay.ReadEncryptedFile();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }
    }
}
