using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class DTOInfor
    {
        private int id = -1;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string fileName = "";

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }
        private int size = 0;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
        private string fromClient = "";

        public string FromClient
        {
            get { return fromClient; }
            set { fromClient = value; }
        }
        private string ipClient = "";

        public string IpClient
        {
            get { return ipClient; }
            set { ipClient = value; }
        }

        private int portClient = 0;

        public int PortClient
        {
            get { return portClient; }
            set { portClient = value; }
        }
        private bool flag = false;

        public bool Flag
        {
            get { return flag; }
            set { flag = value; }
        }

    }
}



