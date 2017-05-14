using FluentFTP;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace FastBackup
{
    class Program
    {
      
        static void Main(string[] args)
        {
           
            Console.SetWindowSize(Math.Min(100, Console.LargestWindowWidth), Math.Min(60, Console.LargestWindowHeight));
            // Get the object used to communicate with the server.
            XML_Functions.Connection ftp_connection = new XML_Functions.Connection();

            List<string> files = new List<string>();
            List<string> directories = new List<string>();
            

            string backupname = DateTime.Now.ToString("Y-yyyy M-MM D-dd hh_mm");
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
                foreach (string rootfolder in Staubli_Folder_Structure.paths.rootfolders)
                {
                    directories.Add(rootfolder);
                    foreach (FtpListItem item in client.GetListing(rootfolder))
                    {
                        if (item.Type == FtpFileSystemObjectType.File)
                        {

                            files.Add(item.FullName);

                        }
                        if (item.Type == FtpFileSystemObjectType.Directory)
                        {
                            directories.Add(item.FullName);
                            SearchFiles.getfiles(files, directories, item.FullName, client);
                        }
                    }
                }

                //Create Directories
                foreach (string directory in directories)
                {
                    //Console.WriteLine(ftp_connection.Path + "\\" + backupname + "\\" + directory);
                    System.IO.Directory.CreateDirectory(ftp_connection.Path + "\\" + backupname + "\\" + directory);
                }
                string temp = "";
                string path = "";
                //Put Files in to
                using (var progress = new ProgressBar())
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        temp = files[i].Split('/').Last();
                        path = files[i].Substring(0, files[i].Length - temp.Length - 1);

                        foreach (string directory in directories)
                        {
                            if (string.Equals(path, directory))
                            {
                                client.DownloadFile(ftp_connection.Path + "\\" + backupname + "\\" + path + "\\" + temp, files[i]);
                                progress.Report((double)i / files.Count);
                                progress.Report(files[i].Substring(0,Math.Min(files[i].Length,65)));
                            }
                        }

                    }


                }

                Console.WriteLine("Backup in Folder : " + backupname + " created");
                client.Disconnect();
                watch.Stop();
                Console.WriteLine(string.Format("Benchmark : {0}", watch.ElapsedMilliseconds));
                Unicorn.show();
                Thread.Sleep(500);
                // Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }

        
    }
}
