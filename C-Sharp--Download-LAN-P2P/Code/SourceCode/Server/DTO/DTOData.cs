using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO
{
    public class DTOData
    {
        private int iDData = -1;

        public int IDData
        {
            get { return iDData; }
            set { iDData = value; }
        }
        private string fileName = "";

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private int iDClient = -1;

        public int IDClient
        {
            get { return iDClient; }
            set { iDClient = value; }
        }
        private int size = 0;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        private bool flagSta = false;

        public bool FlagSta
        {
            get { return flagSta; }
            set { flagSta = value; }
        }
    }
}
