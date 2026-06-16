using NUnit.Framework;
using System;
using UnitTestEx;

namespace UnitTestProject
{
    [TestFixture]
    public class FileTest
    {
        public const string SIZE_EXCEPTION = "Wrong size";
        public const string NAME_EXCEPTION = "Wrong name";
        public const string SPACE_STRING = " ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";

        static object[] FilesData =
        {
            new object[] {new File(FILE_PATH_STRING, CONTENT_STRING), FILE_PATH_STRING, CONTENT_STRING},
            new object[] { new File(SPACE_STRING, SPACE_STRING), SPACE_STRING, SPACE_STRING}
        };

        // Тестируем получение размера
        [Test, TestCaseSource(nameof(FilesData))]
        public void GetSizeTest(File newFile, String name, String content)
        {
            double length = content.Length / 2.0;
            Assert.That(newFile.GetSize(), Is.EqualTo(length), SIZE_EXCEPTION);
        }

        // Тестируем получение имени
        [Test, TestCaseSource(nameof(FilesData))]
        public void GetFilenameTest(File newFile, String name, String content)
        {
            Assert.That(newFile.GetFilename(), Is.EqualTo(name), NAME_EXCEPTION);
        }

        // новая проверка файлов с расширением текста
        [Test]
        public void FileWithExtensionTest()
        {
            File file = new File("document.txt", "content");
            Assert.That(file.GetFilename(), Is.Not.Null);
            Assert.That(file.GetFilename(), Is.EqualTo("document.txt"));
        }

        // новая проверка текстового файла с пустым контентом,
        // размер должен быть 0 так как 0 / 2 = 0
        [Test]
        public void EmptyContentFileTest()
        {
            File file = new File("empty.txt", "");
            Assert.That(file.GetSize(), Is.EqualTo(0));
        }

        // новая проверка рассчета размера для большого контента,
        // тут создаем строку из 100 символов а размер должен быть = 50 так как половина
        [Test]
        public void LargeContentFileTest()
        {
            string content = new string('A', 100);
            File file = new File("large.txt", content);
            Assert.That(file.GetSize(), Is.EqualTo(50.0));
        }
    }
}