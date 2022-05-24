using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyDataCL
{
    public class SaveMyDataController
    {
        public static void saveMyPaths()
        {
            List<Models.Path> myPathsList = MyPaths.getMyPaths();

            List<Models.Path> myFilesList = MyFiles.GetMyFiles(myPathsList);

            CopyFiles.getFilesCopied(myFilesList);

        }
    }
}
