using System;
using System.IO;
using Moq;
using NUnit.Framework;

namespace MultiGry.Tests
{
    [TestFixture]
    class FilesEncryptorOptionTests
    {
        [Test]
        public void OptionExecuting_MethodCalls_DisplaysOptionsCorrectly()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoFor7 = new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false);
            MockOfConsole.Setup(m => m.ReadKey()).Returns(ConsoleKeyInfoFor7);
            
            var Output = new StringWriter();
            Console.SetOut(Output);
            string ExpectedOutput = "1. Utwórz plik" + Environment.NewLine +
                                    "2. Otwórz plik" + Environment.NewLine +
                                    "3. Szyfruj plik" + Environment.NewLine +
                                    "4. Odkoduj plik" + Environment.NewLine +
                                    "5. Odczytaj zaszyfrowany plik" + Environment.NewLine +
                                    "6. Edytuj plik" + Environment.NewLine +
                                    "7. Powrót do menu głównego" + Environment.NewLine;

            var Option = new FilesEncryptor.FilesEncryptorOption(MockOfConsole.Object, 
                                                                 null, null, null, null);

            Option.OptionExecuting();

            Assert.AreEqual(ExpectedOutput, Output.ToString());
        }

        [Test]
        public void OptionExecuting_UserSelectsFirstOption_CreatesFile()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoFor1 = new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false);
            var ConsoleKeyInfoFor7 = new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false);
            MockOfConsole.SetupSequence(m => m.ReadKey())
                .Returns(ConsoleKeyInfoFor1)
                .Returns(ConsoleKeyInfoFor7);

            var MockOfFileCreator = new Mock<FilesEncryptor.IFileCreator>();

            var Option = new FilesEncryptor.FilesEncryptorOption(MockOfConsole.Object, 
                                                                 MockOfFileCreator.Object, 
                                                                 null, null, null);

            Option.OptionExecuting();

            MockOfFileCreator.Verify(m => m.FileCreation(), Times.Once());
        }

        [Test]
        public void OptionExecuting_UserSelectsSecondOption_OpensFile()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoFor2 = new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false);
            var ConsoleKeyInfoFor7 = new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false);
            MockOfConsole.SetupSequence(m => m.ReadKey())
                .Returns(ConsoleKeyInfoFor2)
                .Returns(ConsoleKeyInfoFor7);

            var MockOfFileContentDisplay = new Mock<FilesEncryptor.IFileContentDisplay>();

            var Option = new FilesEncryptor.FilesEncryptorOption(MockOfConsole.Object, null, 
                                                                 MockOfFileContentDisplay.Object, 
                                                                 null, null);

            Option.OptionExecuting();

            MockOfFileContentDisplay.Verify(m => m.OpenFile(), Times.Once());
        }

        [Test]
        public void OptionExecuting_UserSelectsThirdOption_EncryptsFile()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoFor3 = new ConsoleKeyInfo('3', ConsoleKey.D3, false, false, false);
            var ConsoleKeyInfoFor7 = new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false);
            MockOfConsole.SetupSequence(m => m.ReadKey())
                .Returns(ConsoleKeyInfoFor3)
                .Returns(ConsoleKeyInfoFor7);

            var MockOfEncoderFile = new Mock<FilesEncryptor.IEncoderFile>();

            var Option = new FilesEncryptor.FilesEncryptorOption(MockOfConsole.Object, null, null,
                                                                 MockOfEncoderFile.Object, null);

            Option.OptionExecuting();

            MockOfEncoderFile.Verify(m => m.FileEncryption(), Times.Once());
        }

        [Test]
        public void OptionExecuting_UserSelectsFourthOption_DecryptsFile()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoFor4 = new ConsoleKeyInfo('4', ConsoleKey.D4, false, false, false);
            var ConsoleKeyInfoFor7 = new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false);
            MockOfConsole.SetupSequence(m => m.ReadKey())
                .Returns(ConsoleKeyInfoFor4)
                .Returns(ConsoleKeyInfoFor7);

            var MockOfEncoderFile = new Mock<FilesEncryptor.IEncoderFile>();

            var Option = new FilesEncryptor.FilesEncryptorOption(MockOfConsole.Object, null, null,
                                                                 MockOfEncoderFile.Object, null);

            Option.OptionExecuting();

            MockOfEncoderFile.Verify(m => m.FileDecryption(), Times.Once());
        }

        [Test]
        public void OptionExecuting_UserSelectsFifthOption_ReadsEncryptedFile()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoFor5 = new ConsoleKeyInfo('5', ConsoleKey.D5, false, false, false);
            var ConsoleKeyInfoFor7 = new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false);
            MockOfConsole.SetupSequence(m => m.ReadKey())
                .Returns(ConsoleKeyInfoFor5)
                .Returns(ConsoleKeyInfoFor7);

            var MockOfFileContentDisplay = new Mock<FilesEncryptor.IFileContentDisplay>();

            var Option = new FilesEncryptor.FilesEncryptorOption(MockOfConsole.Object, null,
                                                                 MockOfFileContentDisplay.Object, 
                                                                 null, null);

            Option.OptionExecuting();

            MockOfFileContentDisplay.Verify(m => m.ReadEncryptedFile(), Times.Once());
        }

        [Test]
        public void OptionExecuting_UserSelectsSixthOption_EditsFile()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoFor6 = new ConsoleKeyInfo('6', ConsoleKey.D6, false, false, false);
            var ConsoleKeyInfoFor7 = new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false);
            MockOfConsole.SetupSequence(m => m.ReadKey())
                .Returns(ConsoleKeyInfoFor6)
                .Returns(ConsoleKeyInfoFor7);

            var MockOfIWindowsNotebook = new Mock<FilesEncryptor.IWindowsNotebook>();

            var Option = new FilesEncryptor.FilesEncryptorOption(MockOfConsole.Object, null,
                                                                 null, null, 
                                                                 MockOfIWindowsNotebook.Object);

            Option.OptionExecuting();

            MockOfIWindowsNotebook.Verify(m => m.EditFile(), Times.Once());
        }

        [Test]
        public void OptionExecuting_UserSelectsSeventhOption_FinishesItsOperations()
        {
            var MockOfConsole = new Mock<IFakeConsole>();
            var ConsoleKeyInfoFor1 = new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false);
            var ConsoleKeyInfoFor2 = new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false);
            var ConsoleKeyInfoFor3 = new ConsoleKeyInfo('3', ConsoleKey.D3, false, false, false);
            var ConsoleKeyInfoFor4 = new ConsoleKeyInfo('4', ConsoleKey.D4, false, false, false);
            var ConsoleKeyInfoFor5 = new ConsoleKeyInfo('5', ConsoleKey.D5, false, false, false);
            var ConsoleKeyInfoFor6 = new ConsoleKeyInfo('6', ConsoleKey.D6, false, false, false);
            var ConsoleKeyInfoFor7 = new ConsoleKeyInfo('7', ConsoleKey.D7, false, false, false);
            MockOfConsole.SetupSequence(m => m.ReadKey())
                .Returns(ConsoleKeyInfoFor1)
                .Returns(ConsoleKeyInfoFor2)
                .Returns(ConsoleKeyInfoFor3)
                .Returns(ConsoleKeyInfoFor4)
                .Returns(ConsoleKeyInfoFor5)
                .Returns(ConsoleKeyInfoFor6)
                .Returns(ConsoleKeyInfoFor7) // for 7 key
                .Returns(ConsoleKeyInfoFor4)
                .Returns(ConsoleKeyInfoFor2);

            var MockOfFileCreator = new Mock<FilesEncryptor.IFileCreator>();
            var MockOfFileContentDisplay = new Mock<FilesEncryptor.IFileContentDisplay>();
            var MockOfEncoderFile = new Mock<FilesEncryptor.IEncoderFile>();
            var MockOfIWindowsNotebook = new Mock<FilesEncryptor.IWindowsNotebook>();

            var Option = new FilesEncryptor.FilesEncryptorOption(MockOfConsole.Object, 
                                                                 MockOfFileCreator.Object,
                                                                 MockOfFileContentDisplay.Object, 
                                                                 MockOfEncoderFile.Object,
                                                                 MockOfIWindowsNotebook.Object);
            Option.OptionExecuting();

            // if the method ends its operation when the 7 key is pressed, 
            // the ReadKey method will call exactly 7 times (look above at MockOfConsole.SetupSequence)
            MockOfConsole.Verify(m => m.ReadKey(), Times.Exactly(7));
        }
    }
}