using FluentFTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FTPClient
{
    class Program
    {
        public static string msgclass;

        static void Main(string[] args)
        {
            Console.SetWindowSize(Math.Min(80, Console.LargestWindowWidth), Math.Min(60, Console.LargestWindowHeight));
            // Get the object used to communicate with the server.
            XML_Functions.Connection ftp_connection = new XML_Functions.Connection("", "", "", "", "", "");


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

                client.DownloadFile(ftp_connection.Path + "errors.log", "//log/errors.log");

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
                DisplayRainbow(@"Woooooooow ... rainbows everywhere ... and a unicorn O_o o_O !!!
                                                    /
                                                  .7
                                       \       , //
                                       |\.--._/|//
                                      /\ ) ) ).'/
                                     /(  \  // /
                                    /(   J`((_/ \
                                   / ) | _\     /
                                  /|)  \  eJ    L
                                 |  \ L \   L   L
                                /  \  J  `. J   L
                                |  )   L   \/   \
                               /  \    J   (\   /
             _....___         |  \      \   \```
      ,.._.-'        '''--...-||\     -. \   \
    .'.=.'                    `         `.\ [ Y
   /   /                                  \]  J
  Y / Y                                    Y   L
  | | |          \                         |   L
  | | |           Y                        A  J
  |   I           |                       /I\ /
  |    \          I             \        ( |]/|
  J     \         /._           /        -tI/ |
   L     )       /   /'-------'J           `'-:.
   J   .'      ,'  ,' ,     \   `'-.__          \
    \ T      ,'  ,'   )\    /|        ';'---7   /
     \|    ,'L  Y...-' / _.' /         \   /   /
      J   Y  |  J    .'-'   /         ,--.(   /
       L  |  J   L -'     .'         /  |    /\
       |  J.  L  J     .-;.-/       |    \ .' /
       J   L`-J   L____,.-'`        |  _.-'   |
        L  J   L  J                  ``  J    |
        J   L  |   L                     J    |
         L  J  L    \                    L    \
         |   L  ) _.'\                    ) _.'\
         L    \('`    \                  ('`    \
          ) _.'\`-....'                   `-....'
         ('`    \
          `-.___/   sk");

                Thread.Sleep(1000);
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

        static readonly ConsoleColor[] colors = { ConsoleColor.Magenta, ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Yellow, ConsoleColor.Red };

        static void DisplayRainbow(string text)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            try
            {
                int colorIndex = 0;
                for (int i = 0; i < text.Length; ++i)
                {
                    Console.ForegroundColor = colors[colorIndex++ % colors.Length];

                    Console.Write(text[i]);
                }
            }
            finally
            {
                Console.ForegroundColor = originalColor;
            }
        }


    }
}
