using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyDataCL
{
    public class FileName
    {
        public static string getFileName(string sourcePath, string filePath)
        {
            // Metoda zwraca część ścieżki, która nie zawiera ścieżki źródłowej.
            // Służy to do odbudowania całego dir w nowym miejscu.
            string fullFileName = String.Empty;

            fullFileName = filePath.Substring(sourcePath.Length);

            return fullFileName;
        }
    }
}
