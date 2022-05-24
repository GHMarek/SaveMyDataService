using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Resources;
using SaveMyDataCL;

namespace SaveMyDataCL
{
    public class MyPaths
    {
        public static List<Models.Path> getMyPaths()
        {
            // Zwraca listę class Path.
            List<Models.Path> myPathsList = new List<Models.Path>();

            string[] pathStringArr = loadPathsFile();

            if (pathStringArr.Length == 0) { throw new Exception("Brak plików do kopiowania"); }

            foreach (string i in pathStringArr)
            {
                Models.Path newPath = new Models.Path();

                newPath.sourcePath = i.Split(',')[0].Trim();
                newPath.destinationPath = i.Split(',')[1].Trim();

                myPathsList.Add(newPath);

            }

            return myPathsList;
        }

        public static string[] loadPathsFile()
        {
            // Wczytuje listę Paths z pliku.

            string assemblyPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string fileName = @"\myPathsCsv.txt";

            string[] res = System.IO.File.ReadAllText($@"{assemblyPath}{fileName}").Split('\n');

            return res;

        }
    }

}
