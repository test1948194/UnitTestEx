using NUnit.Framework;
using System;
using UnitTestEx;

namespace UnitTestProject
{
    [TestFixture]
    public class FileStorageTest
    {
        public const string MAX_SIZE_EXCEPTION = "DIFFERENT MAX SIZE";
        public const string NULL_FILE_EXCEPTION = "NULL FILE";
        public const string NO_EXPECTED_EXCEPTION_EXCEPTION = "There is no expected exception";
        public const string SPACE_STRING = "  ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public const string REPEATED_STRING = "AA";
        public const string TIC_TOC_TOE_STRING = "tictoctoe.game";
        public const int NEW_SIZE = 500;

        public FileStorage storage = new FileStorage(NEW_SIZE);

        // Провайдеры 
        static object[] NewFilesData =
        {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING) },
            new object[] { new File(SPACE_STRING, CONTENT_STRING) },
            new object[] { new File(FILE_PATH_STRING, CONTENT_STRING) }
        };

        // Тестирование записи файла 
        [Test, TestCaseSource(nameof(NewFilesData))]
        public void WriteTest(File file)
        {
            Assert.That(storage.Write(file), Is.True);
            storage.DeleteAllFiles();
        }

        // Тестирование записи дублирующегося файла 
        [Test, TestCaseSource(nameof(NewFilesData))]
        public void WriteExceptionTest(File file)
        {
            storage.Write(file);

            // используем TestDelegate для явного указания типа делегата
            Assert.Throws<FileNameAlreadyExistsException>(new TestDelegate(() => storage.Write(file)));

            storage.DeleteAllFiles();
        }

        // Тестирование проверки существования файла 
        [Test, TestCaseSource(nameof(NewFilesData))]
        public void IsExistsTest(File file)
        {
            String name = file.GetFilename();
            Assert.That(storage.IsExists(name), Is.False);
            storage.Write(file);
            Assert.That(storage.IsExists(name), Is.True);
            storage.DeleteAllFiles();
        }

        // Тестирование удаления файла
        [Test]
        public void DeleteTest()
        {
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            storage.Write(file);
            Assert.That(storage.Delete(file.GetFilename()), Is.True);
            Assert.That(storage.IsExists(file.GetFilename()), Is.False);
            storage.DeleteAllFiles();
        }

        // Тестирование получения файлов 
        [Test]
        public void GetFilesTest()
        {
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            storage.Write(file);
            Assert.That(storage.GetFiles().Count, Is.EqualTo(1));
            storage.DeleteAllFiles();
        }

        // Тестирование получения файла
        [Test, TestCaseSource(nameof(NewFilesData))]
        public void GetFileTest(File expectedFile)
        {
            storage.Write(expectedFile);
            File actualFile = storage.GetFile(expectedFile.GetFilename());
            Assert.That(actualFile, Is.Not.Null);
            Assert.That(actualFile.GetFilename(), Is.EqualTo(expectedFile.GetFilename()));
            Assert.That(actualFile.GetSize(), Is.EqualTo(expectedFile.GetSize()));
            storage.DeleteAllFiles();
        }

        // новая проверка удаления всех фалов
        [Test]
        public void DeleteAllFilesTest()
        {
            storage.Write(new File("file1.txt", "content1"));
            storage.Write(new File("file2.txt", "content2"));
            storage.Write(new File("file3.txt", "content3"));
            Assert.That(storage.GetFiles().Count, Is.EqualTo(3));
            storage.DeleteAllFiles();
            Assert.That(storage.GetFiles().Count, Is.EqualTo(0));
        }

        // новая проверка null файлов
        [Test]
        public void WriteNullFileTest()
        {
            Assert.That(storage.Write(null), Is.False);
        }

        // новая проверка вместимости хранилища
        [Test]
        public void StorageCapacityTest()
        {
            FileStorage smallStorage = new FileStorage(10);
            File file = new File("small.txt", "1234567890");
            Assert.That(smallStorage.Write(file), Is.True);
            Assert.That(smallStorage.GetFiles().Count > 0 ? 5.0 : 10.0, Is.EqualTo(5.0));
            smallStorage.DeleteAllFiles();
        }
    }
}