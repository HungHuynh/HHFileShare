using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAO;
using DTO;
namespace BUS
{
    public class BUSClient
    {
        public static List<DTOClient> getListClient()
        {
            return DAOClient.getListClient();
        }
        public static bool Insert(DTOClient client)
        {
            return DAOClient.Insert(client);
        }
        public static bool Update(DTOClient client)
        {
            return DAOClient.Update(client);
        }
        public static bool Delete(string userName)
        {
            return DAOClient.Delete(userName);
        }
        public static bool Test(string userName)
        {
            return DAOClient.Test(userName);
        }
        public static bool TestLogin(string userName, string passWord)
        {
            return DAOClient.TestLogin(userName, passWord);
        }
        public static DTOClient Search(int idClient)
        {
            return DAOClient.Search(idClient);
        }
    }
}
