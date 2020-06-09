using System;
using System.IO;
using System.Text;
using Moq;
using NUnit.Framework;

namespace MultiGry.Tests
{
    [TestFixture]
    class EncoderFileTests
    {
        [SetUp]
        public void Setup()
        {
            // this makes it possible to support Windows-1250 encoding during testing
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Test]
        public void FileEncryption_UserEntersPathToFile_EncryptsContentsOfFile()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            string FakeFileContents = "1234567890" + Environment.NewLine +
                                      "aąsślłzżźcć" + Environment.NewLine +
                                      "AĄLŁCĆSŚOÓ";

            byte[] FakeFileBytes = Encoding.GetEncoding("Windows-1250").GetBytes(FakeFileContents);
            var MemoryStreamToRead = new MemoryStream(FakeFileBytes);
            var MemoryStreamToWrite = new MemoryStream();

            var ExpectedEncryptedLinesSentToFile = "9012345678" + Environment.NewLine +
                                                   "iąaśtłhżźkć" + Environment.NewLine +
                                                    "IĄTŁKĆAŚWÓ" + Environment.NewLine;

            var Encoder = new FilesEncryptor.EncoderFile(MockOfConsole.Object, MockOfFile.Object,
                                                         MemoryStreamToRead, MemoryStreamToWrite);

            Encoder.FileEncryption();

            string Actual = Encoding
                            .GetEncoding("Windows-1250")
                            .GetString(MemoryStreamToWrite.ToArray());

            Assert.AreEqual(ExpectedEncryptedLinesSentToFile, Actual);
        }

        [Test]
        public void FileEncryption_UserEntersPathToFile_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            var Output = new StringWriter();
            Console.SetOut(Output);

            string FileNameRequest = "Podaj nazwę pliku (lub ścieżkę względną): ";

            string MessageAboutEncodingCompleted = "Zaszyfrowano plik!";

            string ExpectedOutput = FileNameRequest + Environment.NewLine +
                                    MessageAboutEncodingCompleted + Environment.NewLine;

            var MemoryStreamToRead = new MemoryStream();
            var MemoryStreamToWrite = new MemoryStream();

            var Encoder = new FilesEncryptor.EncoderFile(MockOfConsole.Object, MockOfFile.Object,
                                                         MemoryStreamToRead, MemoryStreamToWrite);

            Encoder.FileEncryption();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void FileEncryption_UserProvidesPathToFileThatDoesNotYetExist_DisplaysRightContent()
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

            var Encoder = new FilesEncryptor.EncoderFile(MockOfConsole.Object, MockOfFile.Object, 
                                                         null, null);

            Encoder.FileEncryption();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void FileDecryption_UserEntersPathToFile_DecryptsContentsOfFile()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            string FakeFileContents = "9012345678" + Environment.NewLine +
                                      "iąaśtłhżźkć" + Environment.NewLine +
                                      "IĄTŁKĆAŚWÓ";

            byte[] FakeFileBytes = Encoding.GetEncoding("Windows-1250").GetBytes(FakeFileContents);
            var MemoryStreamToRead = new MemoryStream(FakeFileBytes);
            var MemoryStreamToWrite = new MemoryStream();

            var ExpectedDecryptedLinesSentToFile = "1234567890" + Environment.NewLine +
                                                   "aąsślłzżźcć" + Environment.NewLine +
                                                   "AĄLŁCĆSŚOÓ" + Environment.NewLine;

            var Encoder = new FilesEncryptor.EncoderFile(MockOfConsole.Object, MockOfFile.Object,
                                                         MemoryStreamToRead, MemoryStreamToWrite);

            Encoder.FileDecryption();

            string Actual = Encoding
                            .GetEncoding("Windows-1250")
                            .GetString(MemoryStreamToWrite.ToArray());

            Assert.AreEqual(ExpectedDecryptedLinesSentToFile, Actual);
        }

        [Test]
        public void FileDecryption_UserEntersPathToFile_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            var Output = new StringWriter();
            Console.SetOut(Output);

            string FileNameRequest = "Podaj nazwę pliku (lub ścieżkę względną): ";

            string MessageAboutEncodingCompleted = "Odkodowano plik!";

            string ExpectedOutput = FileNameRequest + Environment.NewLine +
                                    MessageAboutEncodingCompleted + Environment.NewLine;

            var MemoryStreamToRead = new MemoryStream();
            var MemoryStreamToWrite = new MemoryStream();

            var Encoder = new FilesEncryptor.EncoderFile(MockOfConsole.Object, MockOfFile.Object,
                                                         MemoryStreamToRead, MemoryStreamToWrite);

            Encoder.FileDecryption();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void FileDecryption_UserProvidesPathToFileThatDoesNotYetExist_DisplaysRightContent()
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

            var Encoder = new FilesEncryptor.EncoderFile(MockOfConsole.Object, MockOfFile.Object,
                                                         null, null);

            Encoder.FileDecryption();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }
    }
}
