using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;
using DTO;
namespace BUS
{
    public class BUSData
    {
        public static List<DTOData> getListData()
        {
            return DAOData.getListData();
        }
        public static bool Insert(DTOData data)
        {
        
            return DAOData.Insert(data);
        }
        public static bool Update(DTOData data)
        {
            return DAOData.Update(data);
        }
        public static bool Delete(string fileName, int idClient)
        {
            return DAOData.Delete(fileName, idClient);
        }
        public static bool DeleteAll(string userName)
        {
            List<DTOClient> listClient = DAOClient.getListClient();
            foreach(DTOClient client in listClient)
                if (client.UserName == userName)
                    return DAOData.DeleteAll(client.IDClient);
            return false;
        }
        public static List<DTOData> Search(string fileName, string Client)
        {
            return DAOData.Search(fileName, Client);
        }
        public static bool checkFile(string fileName, int idClient)
        {
            return DAOData.checkFile(fileName, idClient);
        }
    }
}
