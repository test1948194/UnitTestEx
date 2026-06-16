using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestEx
{
    public class FileStorage
    {
        private List<File> files = new List<File>();// исправил - добавил тип <File>
        private double availableSize = 100;
        private double maxSize = 100;

        public FileStorage(int size)
        {
            maxSize = size;
            availableSize = maxSize;// исправил подписку +=,на присваивание =
        }

        public FileStorage()
        {
        }

        public bool Write(File file)
        {
            if (file == null)
            {
                return false;
            }

            if (IsExists(file.GetFilename()))
            {
                throw new FileNameAlreadyExistsException();
            }

            if (file.GetSize() > availableSize) // исправил >= на >
            {
                return false;
            }

            files.Add(file);
            availableSize -= file.GetSize();  //  убрал пробел  

            return true;
        }

        public bool IsExists(String fileName)
        {
            foreach (File file in files)
            { // изменил Contains на Equals
                if (file.GetFilename().Equals(fileName))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Delete(String fileName)
        {
            File fileToDelete = GetFile(fileName);
            if (fileToDelete != null)
            {
                availableSize += fileToDelete.GetSize();
                return files.Remove(fileToDelete);
            }
            return false;
        }

        public List<File> GetFiles()
        {
            return files;
        }

        public File GetFile(String fileName)
        {
            if (IsExists(fileName))
            {
                foreach (File file in files)
                {
                    if (file.GetFilename().Equals(fileName))
                    {
                        return file;
                    }
                }
            }
            return null;
        }

        public bool DeleteAllFiles()
        { // исправил - было RemoveRange с ошибкой
            files.Clear();
            availableSize = maxSize;
            return files.Count == 0;
        }
    }
}