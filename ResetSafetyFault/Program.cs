using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResetSafetyFault
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(Math.Min(80, Console.LargestWindowWidth), Math.Min(60, Console.LargestWindowHeight));
            // Get the object used to communicate with the server.
            XML_Functions.Connection ftp_connection = new XML_Functions.Connection("", "", "", "", "");


            try
            {

                XML_Functions.Create_XML_Settings_File();
                XML_Functions.Read_XML_Settings_File(ftp_connection);

                List<String> files = new List<String>();
                files.Add(@"//usr/configs/sfty.dat");
                files.Add(@"//usr/temp/pdrv.sys");
                files.Add(@"//usr/temp/pdrv.dat");

                Console.WriteLine(string.Format("New Connection to IP : {0}", ftp_connection.IP));
                Console.WriteLine(string.Format("Username : {0} , Password : {1}", ftp_connection.Username, ftp_connection.Password));
               


                foreach (String file in files)
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftp_connection.IP + file);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;

                    // This example assumes the FTP site uses anonymous logon.
                    request.Credentials = new NetworkCredential(ftp_connection.Username, ftp_connection.Password);

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine("Delete status: {0}", response.StatusDescription);
                    }
                }

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
                //Console.ReadKey();
                // File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\errors.log", result);

                // FileStream txtfile = File.Create(@"C:\test.txt");
                //File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\errors.log", reader.ReadToEnd());

            }
            catch (WebException e)
            {
                String result = "";
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                

                Console.Write(status);
                //Console.ReadKey();
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
