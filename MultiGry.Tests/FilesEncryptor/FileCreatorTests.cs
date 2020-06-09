using System;
using System.IO;
using NUnit.Framework;
using Moq;

namespace MultiGry.Tests
{
    [TestFixture]
    class FileCreatorTests
    {
        [Test]
        public void FileCreation_MethodCalls_CreatesFile()
        {
            string FileName = "file";
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns(FileName);

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(false);

            var Creator = new FilesEncryptor.FileCreator(MockOfFile.Object, MockOfConsole.Object);

            Creator.FileCreation();

            MockOfFile.Verify(m => m.CreateText(FileName + ".txt"), Times.Once());
        }

        [Test]
        public void FileCreation_UserProvidesPathToFileThatDoesNotYetExist_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(false);

            var Output = new StringWriter();
            Console.SetOut(Output);

            // between the request for the file name and the message about the file created, 
            // the console should be cleared. I decided not to check if the console is being cleaned
            string FileNameRequest = "Podaj nazwę pliku (plik będzie w ścieżce " +
                                     "programu, lub można użyć ścieżki względnej): ";

            string MessageAboutProperlyCreatedFile = "Utworzono nowy plik!";

            string ExpectedOutput = FileNameRequest + Environment.NewLine + 
                                    MessageAboutProperlyCreatedFile + Environment.NewLine;

            var Creator = new FilesEncryptor.FileCreator(MockOfFile.Object, MockOfConsole.Object);

            Creator.FileCreation();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void FileCreation_UserProvidesPathToExistingFile_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            var Output = new StringWriter();
            Console.SetOut(Output);

            string FileNameRequest = "Podaj nazwę pliku (plik będzie w ścieżce " +
                                     "programu, lub można użyć ścieżki względnej): ";

            string MessageThatFileExists = "Ten plik już istnieje (w danej ścieżce)!";

            string ExpectedOutput = FileNameRequest + Environment.NewLine +
                                    MessageThatFileExists + Environment.NewLine;

            var Creator = new FilesEncryptor.FileCreator(MockOfFile.Object, MockOfConsole.Object);

            Creator.FileCreation();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void FileCreation_UserIsEnteringWrongPath_DisplaysExceptionMessage()
        {   
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var Output = new StringWriter();
            Console.SetOut(Output);

            string FileNameRequest = "Podaj nazwę pliku (plik będzie w ścieżce " +
                                     "programu, lub można użyć ścieżki względnej): ";

            string ExceptionMessage = "exception message";

            string ExpectedOutput = FileNameRequest + Environment.NewLine +
                                    ExceptionMessage + Environment.NewLine;

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(false);
            MockOfFile.Setup(m => m.CreateText("file.txt")).Throws(new Exception(ExceptionMessage));

            var Creator = new FilesEncryptor.FileCreator(MockOfFile.Object, MockOfConsole.Object);

            Creator.FileCreation();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }
    }
}
