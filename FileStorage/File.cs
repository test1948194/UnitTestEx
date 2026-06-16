using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestEx
{
    public class File
    {
        private string extension;
        private string filename;
        private string content;
        private double size;
         
        public File(String filename, String content)
        {
            this.filename = filename;
            this.content = content;
            // изменил деление на double
            this.size = content.Length / 2.0;

            // изменил проверку на наличие точки
            if (filename.Contains("."))
            {
                this.extension = filename.Split('.')[filename.Split('.').Length - 1];
            }
            else
            {
                this.extension = "";
            }
        }

        public double GetSize()
        {
            // изменил и теперь возвращает double, не преобразовываем к int
            return size; 
        }

        public string GetFilename()
        {
            return filename;
        }
    }
}
