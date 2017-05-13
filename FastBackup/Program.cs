using FluentFTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FastBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Math.Min(80, Console.LargestWindowWidth), Math.Min(60, Console.LargestWindowHeight));
            // Get the object used to communicate with the server.
            XML_Functions.Connection ftp_connection = new XML_Functions.Connection("", "", "", "", "");

            List<string> files = new List<string>();
            List<string> directories = new List<string>();
            try
            {

                XML_Functions.Create_XML_Settings_File();
                XML_Functions.Read_XML_Settings_File(ftp_connection);
                // create an FTP client

                Console.WriteLine(string.Format("New Connection to IP : {0}", ftp_connection.IP));
                Console.WriteLine(string.Format("Username : {0} , Password : {1}", ftp_connection.Username, ftp_connection.Password));

                // create an FTP client
                FtpClient client = new FtpClient(ftp_connection.IP);

                // if you don't specify login credentials, we use the "anonymous" user account
                client.Credentials = new NetworkCredential(ftp_connection.Username, ftp_connection.Password);

                // begin connecting to the server
                client.Connect();

                Stopwatch watch = new Stopwatch();
                watch.Start();
                // get a list of files and directories in the "/htdocs" folder
                foreach (FtpListItem item in client.GetListing("/usr"))
                {

                    // if this is a file
                    if (item.Type == FtpFileSystemObjectType.File)
                    {

                        files.Add(item.FullName);

                    }
                    if (item.Type == FtpFileSystemObjectType.Directory)
                    {
                        directories.Add(item.FullName);
                        //Subdirectories
                        foreach (FtpListItem subitem in client.GetListing(item.FullName))
                        {
                            if (subitem.Type == FtpFileSystemObjectType.File)
                            {

                                files.Add(subitem.FullName);

                            }
                            if (subitem.Type == FtpFileSystemObjectType.Directory)
                            {
                                
                             directories.Add(subitem.FullName);
                                foreach (FtpListItem subsubitem in client.GetListing(subitem.FullName))
                                {
                                    if (subsubitem.Type == FtpFileSystemObjectType.File)
                                    {

                                        files.Add(subsubitem.FullName);

                                    }
                                }
                            }
                        }
                    }



                }

                //Create Directories
                foreach(string directory in directories)
                {
                   
                    System.IO.Directory.CreateDirectory(ftp_connection.Path+directory);
                }
                string temp="";
                string path="";
                //Put Files in to
                foreach (string file in files)
                {
                    temp = file.Split('/').Last();
                    path = file.Substring(0,file.Length-temp.Length-1);

                    foreach(string directory in directories)
                    {
                    if(string.Equals(path,directory))
                    {
                    client.DownloadFile(ftp_connection.Path+path+"\\"+temp, file);
                 
                    }
                    }
                   
                }
                
                client.Disconnect();
                watch.Stop();
                Console.WriteLine(string.Format("Benchmark : {0}", watch.ElapsedMilliseconds));
                Unicorn.show();
                Console.ReadKey();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }
    }
}
