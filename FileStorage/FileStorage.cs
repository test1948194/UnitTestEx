using System;
using System.Collections.Generic;
using System.Text;
using UnitTestEx;

namespace UnitTestEx
{
    public class FileStorage
    {
        private List<File> files = new List<File>();
        private double availableSize = 100;
        private double maxSize = 100;
         
        public FileStorage(int size) {
            maxSize = size;
            // изменил прибавление на присваивание 
            availableSize = maxSize;
        }
         
        public FileStorage() {
        }
 
        public bool Write(File file) {
            if (file == null)
            {
                return false;
            }

            if (IsExists(file.GetFilename()))
            {
                throw new FileNameAlreadyExistsException();
            }

            // изменил больше либо равно на больше
            if (file.GetSize() > availableSize) 
            {
                return false;
            }

            files.Add(file);
            // убран пробел в GetSize()
            availableSize -= file.GetSize(); 

            return true;
        }
         
        public bool IsExists(String fileName) {
            
            foreach (File file in files) {
                // изменил Contains на Equals
                if (file.GetFilename().Equals(fileName)) {
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
                    // исправил Contains на Equals
                    if (file.GetFilename().Equals(fileName)) 
                    {
                        return file;
                    }
                }
            }
            return null;
        }
         
        public bool DeleteAllFiles()
        {
            // исправил так как было RemoveRange с ошибкой
            files.Clear(); 
            availableSize = maxSize;
            return files.Count == 0;
        }

    }
}
