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

namespace RobotInformations
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Math.Min(80, Console.LargestWindowWidth), Math.Min(60, Console.LargestWindowHeight));
            // Get the object used to communicate with the server.
            XML_Functions.Connection ftp_connection = new XML_Functions.Connection();

            StringBuilder result = new StringBuilder();
            try
            {

                XML_Functions.Create_XML_Settings_File();
                XML_Functions.Read_XML_Settings_File(ftp_connection);

               
                Console.WriteLine(string.Format("New Connection to IP : {0}", ftp_connection.IP));
                Console.WriteLine(string.Format("Username : {0} , Password : {1}", ftp_connection.Username, ftp_connection.Password));


                Staubli_Folder_Structure.xml_info_files.controller_file.Localfilename = ftp_connection.Path + "temp_controller.xml";
                Staubli_Folder_Structure.xml_info_files.arm_file.Localfilename = ftp_connection.Path + "temp_arm.xml";
                Staubli_Folder_Structure.xml_info_files.val3version_file.Localfilename = ftp_connection.Path + "temp_val3version.xml";
                List<string> tempfiles = new List<string>();
                tempfiles.Add(Staubli_Folder_Structure.xml_info_files.arm_file.Localfilename);
                tempfiles.Add(Staubli_Folder_Structure.xml_info_files.controller_file.Localfilename);
                tempfiles.Add(Staubli_Folder_Structure.xml_info_files.val3version_file.Localfilename);

                // create an FTP client
                FtpClient client = new FtpClient(ftp_connection.IP);

                // if you don't specify login credentials, we use the "anonymous" user account
                client.Credentials = new NetworkCredential(ftp_connection.Username, ftp_connection.Password);

                // begin connecting to the server
                client.Connect();

                foreach (FtpListItem item in client.GetListing(Staubli_Folder_Structure.paths.config_path))
                {
                    if (item.Type == FtpFileSystemObjectType.File)
                    {

                        if(item.FullName.Contains("arm"))
                        {
                            if (item.FullName.Contains("cfx"))
                            {
                                Staubli_Folder_Structure.xml_info_files.arm_file.Ftpfilename = item.FullName;
                               
                            }
                        }

                    }
                   
                }
                 
                //Console.WriteLine(Staubli_Folder_Structure.xml_info_files.controller_file.Localfilename + "  " + Staubli_Folder_Structure.xml_info_files.controller_file.Ftpfilename);
                if(client.FileExists(Staubli_Folder_Structure.xml_info_files.controller_file.Ftpfilename))
                {
                client.DownloadFile(Staubli_Folder_Structure.xml_info_files.controller_file.Localfilename, Staubli_Folder_Structure.xml_info_files.controller_file.Ftpfilename);
                }
                if (client.FileExists(Staubli_Folder_Structure.xml_info_files.arm_file.Ftpfilename))
                {
                client.DownloadFile(Staubli_Folder_Structure.xml_info_files.arm_file.Localfilename, Staubli_Folder_Structure.xml_info_files.arm_file.Ftpfilename);
                 }
                 if(client.FileExists(Staubli_Folder_Structure.xml_info_files.val3version_file.Ftpfilename))
                {
                     client.DownloadFile(Staubli_Folder_Structure.xml_info_files.val3version_file.Localfilename, Staubli_Folder_Structure.xml_info_files.val3version_file.Ftpfilename);
                 }
                client.Disconnect();    

                Staubli_Folder_Structure.xml_info_files.controller_file.ItemsList.Add(Staubli_Folder_Structure.xml_items.PowerHourCount);
                Staubli_Folder_Structure.xml_info_files.controller_file.ItemsList.Add(Staubli_Folder_Structure.xml_items.ControlerSerialNumber);

                Staubli_Folder_Structure.xml_info_files.arm_file.ItemsList.Add(Staubli_Folder_Structure.xml_items.ArmOrderNumber);
                Staubli_Folder_Structure.xml_info_files.arm_file.ItemsList.Add(Staubli_Folder_Structure.xml_items.RobotType);

                Staubli_Folder_Structure.xml_info_files.val3version_file.ItemsList.Add(Staubli_Folder_Structure.xml_items.Val3Version);

                XML_Functions.Read_XML_File_Item(Staubli_Folder_Structure.xml_info_files.controller_file);
                XML_Functions.Read_XML_File_Item(Staubli_Folder_Structure.xml_info_files.arm_file);
                XML_Functions.Read_XML_File_Item(Staubli_Folder_Structure.xml_info_files.val3version_file);


                Console.WriteLine("PowerHourCount           : " + Staubli_Folder_Structure.xml_items.PowerHourCount.Value);
                Console.WriteLine("Arm Order number         : " + Staubli_Folder_Structure.xml_items.ArmOrderNumber.Value);
                Console.WriteLine("Controler Serial number  : " + Staubli_Folder_Structure.xml_items.ControlerSerialNumber.Value);
                Console.WriteLine("Robottype                : " + Staubli_Folder_Structure.xml_items.RobotType.Value);
                Console.WriteLine("Val3Version              : " + Staubli_Folder_Structure.xml_items.Val3Version.Value);

                result.Append(Staubli_Folder_Structure.xml_items.PowerHourCount.Value + Environment.NewLine);
                result.Append(Staubli_Folder_Structure.xml_items.ArmOrderNumber.Value + Environment.NewLine);
                result.Append(Staubli_Folder_Structure.xml_items.ControlerSerialNumber.Value + Environment.NewLine);
                result.Append(Staubli_Folder_Structure.xml_items.RobotType.Value + Environment.NewLine);
                result.Append(Staubli_Folder_Structure.xml_items.Val3Version.Value + Environment.NewLine);

                XML_Functions.Delete_XML_Files(tempfiles);
                if(File.Exists(ftp_connection.Informationfilename))
                {
                    File.Delete(ftp_connection.Informationfilename);
                }
               File.WriteAllText(ftp_connection.Informationfilename, result.ToString());

                Unicorn.show();
                Thread.Sleep(500);
                // Console.ReadKey();
                // File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\errors.log", result);

                // FileStream txtfile = File.Create(@"C:\test.txt");
                //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\errors.log", reader.ReadToEnd());

            }
            catch (Exception e)
            {


                Console.Write(e.Message);
                Console.ReadKey();
            }

        }
    }
}
