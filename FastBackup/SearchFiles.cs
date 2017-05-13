using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastBackup
{
    class SearchFiles
    {
        public static void getfiles(List<string> resultlist, List<string> directorylist, string folder, FtpClient ftpclient)
        {
            foreach (FtpListItem item in ftpclient.GetListing(folder))
            {

                // if this is a file
                if (item.Type == FtpFileSystemObjectType.File)
                {

                    resultlist.Add(item.FullName);

                }
                if (item.Type == FtpFileSystemObjectType.Directory)
                {
                    directorylist.Add(item.FullName);
                    getfiles(resultlist,directorylist, item.FullName, ftpclient);
                }
            }

        }
    }
}
