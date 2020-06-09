using System;
using System.IO;
using NUnit.Framework;
using Moq;

namespace MultiGry.Tests
{
    [TestFixture]
    class WindowsNotebookTests
    {
        [Test]
        public void OpenFileInNotebook_UserEntersPathToFile_LaunchesWindowsNotebook()
        {
            string FileName = "file";
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns(FileName);

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            var FakeProcess = new FilesEncryptor.FakeProcess
            {
                ExpectedFileName = "notepad.exe",
                ExpectedArguments = FileName + ".txt",
                ExpectedUseShellExecute = false,
                ExpectedRedirectStandardOutput = false
            };

            var Notebook = new FilesEncryptor.WindowsNotebook(MockOfConsole.Object, MockOfFile.Object,
                                                              FakeProcess);

            Notebook.EditFile();

            // if the start method was called and the correct data was given, 
            // it means that the notebook is launching correctly:
            Assert.IsTrue(FakeProcess.AreAllDataCorrect_AfterCallingStartMethod);
        }

        [Test]
        public void OpenFileInNotebook_UserEntersPathToFile_DisplaysRightContent()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            MockOfConsole.Setup(m => m.ReadLine()).Returns("file");

            var MockOfFile = new Mock<FilesEncryptor.IFakeFile>();
            MockOfFile.Setup(m => m.Exists("file.txt")).Returns(true);

            var FakeProcess = new FilesEncryptor.FakeProcess();

            var Output = new StringWriter();
            Console.SetOut(Output);

            string ExpectedOutput = "Podaj nazwę pliku (lub ścieżkę względną): " + Environment.NewLine;

            var Notebook = new FilesEncryptor.WindowsNotebook(MockOfConsole.Object, MockOfFile.Object,
                                                              FakeProcess);

            Notebook.EditFile();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void OpenFileInNotebook_UserProvidesPathToFileThatDoesNotYetExist_DisplaysRightContent()
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

            var Notebook = new FilesEncryptor.WindowsNotebook(MockOfConsole.Object, MockOfFile.Object, null);

            Notebook.EditFile();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }
    }
}
