using SaveMyDataCL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyDataCL
{
    public class CopyFiles
    {
        public static void getFilesCopied(List<Models.Path> filesToCopy)
        {
            // Metoda kopiuje pliki.
            filesToCopy = verifyDirs(filesToCopy);

            foreach (var i in filesToCopy)
            {
                File.Copy($@"{i.sourcePath}", $@"{i.destinationPath}");
            }
        }

        public static List<Models.Path> verifyDirs(List<Models.Path> filesToCopy)
        {
            List<string> sourceDirList = new List<string>();
            List<string> destinationDirList = new List<string>();

            foreach (var i in filesToCopy.Distinct())
            {
                sourceDirList.Add($@"{System.IO.Path.GetDirectoryName(i.sourcePath)}\");
                destinationDirList.Add($@"{System.IO.Path.GetDirectoryName(i.destinationPath)}\");
            }

            destinationDirList.Sort();

            foreach (var i in destinationDirList.Distinct().ToList())
            {
                if (!Directory.Exists(i))
                {
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(i));
                }
                else
                {
                    // Jeśli istnieje, to też coś trzeba z tym zrobić.
                    // Return zmodyfikowanej listy jest ok. Nie można już skopiować istniejących plików.

                    var tempList = filesToCopy.ToList();
                    foreach (var z in tempList)
                    {

                        if (System.IO.Path.GetDirectoryName(z.destinationPath) == System.IO.Path.GetDirectoryName(i))
                        {
                            // Console.WriteLine("ok");
                            filesToCopy.RemoveAll(x => x.destinationPath.Contains(System.IO.Path.GetDirectoryName(z.destinationPath).ToString()));
                        }
                    }
                }


            }
            return filesToCopy;
        }
    }
}
