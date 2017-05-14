using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotInformations
{
    public class RobotInformations
    {
        string _powerHourCount;
        string _ArmOrderNumber;
        string _ControlerSerialNumber;
        string _Val3Version;
        string _RobotType;


        public RobotInformations()
        {
            this._powerHourCount = "";
            this._ArmOrderNumber = "";
            this._ControlerSerialNumber = "";
            this._Val3Version = "";
            this._RobotType = "";

        }

        public string PowerHourCount { set { _powerHourCount = value; } get { return _powerHourCount; } }
        public string ArmOrderNumber { set { _ArmOrderNumber = value; } get { return _ArmOrderNumber; } }
        public string ControlerSerialNumber { set { _ControlerSerialNumber = value; } get { return _ControlerSerialNumber; } }
        public string Val3Version { set { _Val3Version = value; } get { return _Val3Version; } }
        public string RobotType { set { _RobotType = value; } get { return _RobotType; } }
    }


}
