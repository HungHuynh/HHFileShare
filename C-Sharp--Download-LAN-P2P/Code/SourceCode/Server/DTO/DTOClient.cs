using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DTOClient
    {
        private int iDClient = -1;

        public int IDClient
        {
            get { return iDClient; }
            set { iDClient = value; }
        }
        private string userName = "";

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string passWord = "";

        public string PassWord 
        {
            get { return passWord; }
            set { passWord = value; }
        }
        private string iPAdr = "";

        public string IPAdr
        {
            get { return iPAdr; }
            set { iPAdr = value; }
        }
        private int numPort = 0;

        public int NumPort
        {
            get { return numPort; }
            set { numPort = value; }
        }
        private bool flagSta = false;

        public bool FlagSta
        {
            get { return flagSta; }
            set { flagSta = value; }
        }

    }
}
