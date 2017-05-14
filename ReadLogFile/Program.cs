using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace FTPClient
{
    class Program
    {
        public static string msgclass;

        static void Main(string[] args)
        {
            Console.SetWindowSize(Math.Min(80, Console.LargestWindowWidth), Math.Min(60, Console.LargestWindowHeight));
            // Get the object used to communicate with the server.
            XML_Functions.Connection ftp_connection = new XML_Functions.Connection();


            try
            {

                XML_Functions.Create_XML_Settings_File();
                XML_Functions.Read_XML_Settings_File(ftp_connection);
                if (File.Exists(ftp_connection.Path + ftp_connection.Filename))
                {
                    File.Delete(ftp_connection.Path + ftp_connection.Filename);
                }
                if (File.Exists(ftp_connection.Path + "errors.log"))
                {
                    File.Delete(ftp_connection.Path + "errors.log");
                }
                Console.WriteLine(string.Format("New Connection to IP : {0}", ftp_connection.IP));
                Console.WriteLine(string.Format("Username : {0} , Password : {1}", ftp_connection.Username, ftp_connection.Password));
                Console.WriteLine(string.Format("Datapath : {0} , Filename : {1}", ftp_connection.Path, ftp_connection.Filename));

                StringBuilder result = new StringBuilder();

                string line = "";
                msgclass = ftp_connection.MsgClass;
                // This example assumes the FTP site uses anonymous logon.

                // create an FTP client
                FtpClient client = new FtpClient(ftp_connection.IP);

                // if you don't specify login credentials, we use the "anonymous" user account
                client.Credentials = new NetworkCredential(ftp_connection.Username, ftp_connection.Password);

                // begin connecting to the server
                client.Connect();

                client.DownloadFile(ftp_connection.Path + "errors.log", Staubli_Folder_Structure.files.logfile_path);

                client.Disconnect();

                if (File.Exists(ftp_connection.Path + "errors.log"))
                {
                    using (StreamReader reader = new StreamReader(ftp_connection.Path + "errors.log"))
                    {
                        result.Append(StaticText.header + Environment.NewLine);
                        while ((line = reader.ReadLine()) != null)
                        {

                            result.Append(string.Format(@"41860806845.6366;2;1;{1};1;""{0}"";;;;;;;;""07.05.2017 19:56:51"";"""";""""", line, msgclass) + Environment.NewLine);

                        }
                        // Console.WriteLine("Download Complete, status {0}", response.StatusDescription);

                        result.Append(StaticText.end + Environment.NewLine);

                    }
                }

                File.WriteAllText(ftp_connection.Path + ftp_connection.Filename, result.ToString());
                Unicorn.show();
                Thread.Sleep(500);
                // File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\errors.log", result);

                // FileStream txtfile = File.Create(@"C:\test.txt");
                //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\errors.log", reader.ReadToEnd());

            }
            catch (Exception e)
            {
                String result = "";
               
                result += StaticText.header + Environment.NewLine;
                result += string.Format(@"41860806845.6366;2;1;{1};1;""{0}"";;;;;;;;""07.05.2017 19:56:51"";"""";""""", e.Message, msgclass) + Environment.NewLine;
                result += StaticText.end + Environment.NewLine;
                File.WriteAllText(ftp_connection.Path + ftp_connection.Filename, result);

                Console.Write(e.Message);

            }
        }

        


    }
}
