using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class BUSInfor
    {
        public static List<DTOInfor> getListInfor()
        {
            return DAOInfor.getListInfor();
        }
        public static bool Insert(DTOInfor infor)
        {
            return DAOInfor.Insert(infor);
        }
        public static bool Delete(string file, string client)
        {
            return DAOInfor.Delete(file,client);
        }
        public static bool Update(DTOInfor infor)
        {
            return DAOInfor.Update(infor);
        }
        public static bool DeleteAll()
        {
            return DAOInfor.DeleteAll();
        }
    }
}
