using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class Staubli_Folder_Structure
    {
        public class paths
        {
        public static List<string> rootfolders = new List<string>(){"/usr","/log","/sys"};


        public static string config_path = "/usr/configs/";
        }

        public class files
        {   
        public static string logfile_path = "/log/errors.log";

        public static string controller_config_path = paths.config_path + "controller.cfx";

        public static string val_version_path = paths.config_path + "val3version.xml";

        public static string arm_version_path = paths.config_path + "arm.{0}.cfx";
        }

        public class xml_items
        {
            public static XML_Functions.XML_Item PowerHourCount = new XML_Functions.XML_Item("", "powerHourCount", false);
            public static XML_Functions.XML_Item ArmOrderNumber = new XML_Functions.XML_Item("", "orderNumber", false);
            public static XML_Functions.XML_Item ControlerSerialNumber = new XML_Functions.XML_Item("", "serialNumber", false);
            public static XML_Functions.XML_Item Val3Version = new XML_Functions.XML_Item("", "val3", true);
            public static XML_Functions.XML_Item RobotType = new XML_Functions.XML_Item("", "arm", false);

        }

        public class xml_info_files
        {
            public static XML_Functions.XML_File_Search controller_file = new XML_Functions.XML_File_Search(xml_mainnodes.Controller, files.controller_config_path, "");

            public static XML_Functions.XML_File_Search val3version_file = new XML_Functions.XML_File_Search(xml_mainnodes.Versions, files.val_version_path, "");

            public static XML_Functions.XML_File_Search arm_file = new XML_Functions.XML_File_Search(xml_mainnodes.Arm, files.arm_version_path, "");
        }

        public class xml_mainnodes
        {
            public static string Controller = "/controller/*"; //value
            public static string Arm = "/arm/*"; //value
            public static string Versions = "/Versions/*"; //value
            
        }
    }
}
