using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ResetSafetyFault
{
    class XML_Functions
    {

        public static string xml_name = @"C:\ProgramData\StarkIndustries\" + @"settings.xml";
        public static void Create_XML_Settings_File()
        {

          

            try
            {
                System.IO.Directory.CreateDirectory(@"C:\ProgramData\StarkIndustries");
                if (!File.Exists(xml_name))
                {

                    XmlWriterSettings settings = new XmlWriterSettings();

                    // settings.Encoding = Encoding.GetEncoding("UTF-8");
                    settings.Indent = true;
                    settings.IndentChars = "\t";
                    // settings.Indent = true;
                    // settings.NewLineHandling = NewLineHandling.Replace;
                    // settings.IndentChars = " ";
                    // settings.NewLineOnAttributes = true;
                    //  settings.OmitXmlDeclaration = true;



                    using (XmlWriter writer = XmlWriter.Create(xml_name, settings))//
                    {
                        writer.WriteStartDocument();

                        writer.WriteStartElement("Connection_Settings");
                        writer.WriteStartElement("FTP_Connection");

                        writer.WriteElementString("IP", "192.168.10.230");
                        writer.WriteElementString("User", "maintenance");
                        writer.WriteElementString("Pwd", "spec_cal");
                        writer.WriteElementString("Path", @"C:\Logs\");
                        writer.WriteElementString("Filename", "Log.csv");
                        writer.WriteEndElement();


                        writer.WriteEndElement();
                        writer.WriteEndDocument();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }


        /// <summary>
        /// Read the WifiNetwork file 
        /// </summary>
        /// <param name="WifiNetwork_list"></param>
        public static void Read_XML_Settings_File(Connection FTP_Connection)
        {

            try
            {
                // Console.WriteLine(xml_name);
                if (File.Exists(xml_name))
                {

                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(xml_name);

                    foreach (XmlNode ftpConnection in xdoc.SelectNodes("/Connection_Settings/*"))
                    {
                        if (ftpConnection != null)
                        {
                            FTP_Connection.IP = ftpConnection["IP"].InnerText;
                            FTP_Connection.Username = ftpConnection["User"].InnerText;
                            FTP_Connection.Password = ftpConnection["Pwd"].InnerText;
                            FTP_Connection.Path = ftpConnection["Path"].InnerText;
                            FTP_Connection.Filename = ftpConnection["Filename"].InnerText;
                            // Console.WriteLine(WifiNetwork["SSID"].InnerText + WifiNetwork["Key"].InnerText + WifiNetwork["DHCPorSTATIC"].InnerText + WifiNetwork["StaticIP"].InnerText);
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }
        /*
        public static void Change_XML_WifiNetwork_File(WifiNetwork WifiNetwork)
        {

            try
            {
                // Console.WriteLine(xml_name);
                if (File.Exists(xml_name))
                {

                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(xml_name);

                    XmlElement node = xdoc.CreateElement("WifiNetwork");
                    XmlElement SSID = xdoc.CreateElement("SSID");
                    SSID.InnerText = WifiNetwork.SSID;
                    XmlElement Key = xdoc.CreateElement("Key");
                    Key.InnerText = WifiNetwork.Key;
                    XmlElement DHCPorSTATIC = xdoc.CreateElement("DHCPorSTATIC");
                    DHCPorSTATIC.InnerText = WifiNetwork.DHCPorSTATIC;
                    XmlElement StaticIP = xdoc.CreateElement("StaticIP");
                    StaticIP.InnerText = WifiNetwork.StaticIP;
                    node.AppendChild(SSID);
                    node.AppendChild(Key);
                    node.AppendChild(DHCPorSTATIC);
                    node.AppendChild(StaticIP);
                    xdoc.DocumentElement.AppendChild(node);

                    xdoc.Save(xml_name);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        */
        

        public class Connection
        {
            string _ip;
            string _username;
            string _pwd;
            string _path;
            string _filename;


            public Connection(string ip, string username, string pwd,string filename,string path)
            {
                this. _ip = ip;
                this._username = username;
                this._pwd = pwd;
                this._filename = filename;
                this._path = path;
            }

            public string IP { set { _ip = value; } get { return _ip; } }
            public string Username { set { _username = value; } get { return _username; } }
            public string Password { set { _pwd = value; }  get { return _pwd; } }
            public string Path { set { _path = value; } get { return _path; } }
            public string Filename { set { _filename = value; } get { return _filename; } }

        }

    }
}
