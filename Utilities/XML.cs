using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Utilities
{
    public class XML_Functions
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
                        writer.WriteElementString("MsgClass", "64");
                        writer.WriteElementString("InfFilename", @"C:\Logs\informations.txt");

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
                            FTP_Connection.MsgClass = ftpConnection["MsgClass"].InnerText;
                            FTP_Connection.Informationfilename = ftpConnection["InfFilename"].InnerText;

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

        public static void Read_XML_File_Item(XML_File_Search xml_file)
        {

            try
            {
                // Console.WriteLine(xml_name);
                if (File.Exists(xml_file.Localfilename))
                {

                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(xml_file.Localfilename);


                    foreach (XML_Item item in xml_file.ItemsList)
                    {
                        
                        string query = string.Format("//*[@name='{0}']", item.Name); // or "//book[@id='{0}']"
                        XmlElement elem = (XmlElement)xdoc.SelectSingleNode(query);

                        if (elem != null)
                        {
                            
                            if (item.GetInnerText == true)
                            {
                                item.Value = elem.InnerText;
                               
                            }
                            else
                            {
                                item.Value = elem.GetAttribute("value");
                                
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Delete a list of files
        /// </summary>
        /// <param name="pathlist"></param>
        public static void Delete_XML_Files(List<string> pathlist)
        {
            foreach (string path in pathlist)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        public class Connection
        {
            string _ip;
            string _username;
            string _pwd;
            string _path;
            string _filename;
            string _msgclass;
            string _informationfilename;

            //string ip, string username, string pwd,string filename,string path,string msgclass, string 
            public Connection()
            {
                this._ip = "";
                this._username = "";
                this._pwd = "";
                this._filename = "";
                this._path = "";
                this._msgclass = "";
                this._informationfilename = "";
            }

            public string IP { set { _ip = value; } get { return _ip; } }
            public string Username { set { _username = value; } get { return _username; } }
            public string Password { set { _pwd = value; } get { return _pwd; } }
            public string Path { set { _path = value; } get { return _path; } }
            public string Filename { set { _filename = value; } get { return _filename; } }
            public string MsgClass { set { _msgclass = value; } get { return _msgclass; } }
            public string Informationfilename { set { _informationfilename = value; } get { return _informationfilename; } }
        }

        public class XML_Item
        {
            string _value;
            string _name;
            bool _getinnertext;

            //string ip, string username, string pwd,string filename,string path,string msgclass, string 
            public XML_Item(string value, string name, bool getinnertext)
            {
                this._value = value;
                this._name = name;
                this._getinnertext = getinnertext;

            }

            public string Value { set { _value = value; } get { return _value; } }
            public string Name { set { _name = value; } get { return _name; } }
            public bool GetInnerText { set { _getinnertext = value; } get { return _getinnertext; } }

        }

        public class XML_File_Search
        {
            List<XML_Item> _itemslist;
            string _mainnode;
            string _ftppath;
            string _ftpfilename;
            string _localfilename;

            //string ip, string username, string pwd,string filename,string path,string msgclass, string 
            public XML_File_Search(string mainnode, string ftpfilename, string localfilename)
            {
                this._itemslist = new List<XML_Item>();
                this._mainnode = mainnode;
                this._ftppath = "";
                this._ftpfilename = ftpfilename;
                this._localfilename = localfilename;

            }

            public string Mainnode { set { _mainnode = value; } get { return _mainnode; } }
            public string Ftppath { set { _ftppath = value; } get { return _ftppath; } }
            public string Ftpfilename { set { _ftpfilename = value; } get { return _ftpfilename; } }
            public string Localfilename { set { _localfilename = value; } get { return _localfilename; } }
            public List<XML_Item> ItemsList { set { _itemslist = value; } get { return _itemslist; } }
        }


    }
}
