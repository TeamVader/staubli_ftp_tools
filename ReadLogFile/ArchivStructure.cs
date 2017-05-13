using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTPClient
{
    class ArchivStructure
    {

    }

    class StaticText
    {
        public static string header = string.Join(";", "Time_ms", "MsgProc", "StateAfter", "MsgClass", "MsgNumber", "Var1", "Var2", "Var3", "Var4", "Var5", "Var6", "Var7", "Var8", "TimeString", "MsgText", "PLC");
        public static string end = string.Join(";", "$RT_COUNT$", "1", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
    }


    }
