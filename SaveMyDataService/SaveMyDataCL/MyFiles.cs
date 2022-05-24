using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SaveMyDataCL;

namespace SaveMyDataCL
{
    public class MyFiles
    {
        public static List<Models.Path> GetMyFiles(List<Models.Path> myPaths)
        {
            // Zwraca pełną listę Path do skopiowania, już wzbogaconą o dokładne nazwy plików.

            // Utworzy nowy folder o nazwie z datą kopiowania
            string newDateTimeFormat = DateTime.Now.ToString().Substring(0, 10);
            Regex pattern = new Regex("[:. ]");
            newDateTimeFormat = pattern.Replace(newDateTimeFormat, "_");

            List<Models.Path> fullFilesList = new List<Models.Path>();

            foreach (var i in myPaths)
            {
                if (i.sourcePath.Substring(i.sourcePath.Length - 1, 1) == @"\")
                {
                    //Console.WriteLine($"{i.sourcePath} {i.destinationPath}");

                    string[] files = Directory.GetFiles(i.sourcePath, "*.*", SearchOption.AllDirectories);

                    // niektóre files mogą mieć dodatkowe diry

                    if (files.Count() > 0)
                    {
                        foreach (var j in files)
                        {
                            //var y = i.sourcePath.Split(@"\").ToList().Where(x => x != "").LastOrDefault();
                            string newDirName = $@"{i.sourcePath.Split(Convert.ToChar(@"\")).ToList().Where(x => x != "").LastOrDefault()}_{newDateTimeFormat}\";

                            Models.Path path = new Models.Path();
                            path.sourcePath = $@"{i.sourcePath}{FileName.getFileName(i.sourcePath, j)}";
                            path.destinationPath = $@"{i.destinationPath}{newDirName}{FileName.getFileName(i.sourcePath, j)}";

                            fullFilesList.Add(path);
                        }
                    }

                }
                else
                {
                    //Console.WriteLine($"{i.sourcePath} {i.destinationPath}");

                    if (File.Exists(i.sourcePath))
                    {
                        // Plik nie może się kopiować bezpośrednio do nowej lokalizacji.
                        // Zawsze się musi tworzyć dir powiązany z kopiowaniem.

                        string newSubDir = i.sourcePath.Split(Convert.ToChar(@"\")).ToList().Where(x => x != "").LastOrDefault();

                        string newDirName = $@"{newSubDir.Substring(0, newSubDir.IndexOf("."))}_{newDateTimeFormat}\";

                        Models.Path path = new Models.Path();
                        path.sourcePath = $@"{i.sourcePath}";
                        path.destinationPath = $@"{i.destinationPath}{newDirName}{newSubDir}";

                        fullFilesList.Add(path);
                    }

                }
            }

            return fullFilesList.Distinct().ToList();
        }
    }
}
