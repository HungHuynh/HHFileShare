using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data.OleDb;

namespace DAO
{
    public class DAOClient:Connect
    {
        public static List<DTOClient> getListClient()
        {
            OleDbConnection connect = null;
            List<DTOClient> listClient = new List<DTOClient>();

            try
            {
                connect = OpenConnect();
                string stringCommand = "SELECT IDClient, Username, Password, IPAdr, NumPort, FlagSta FROM Client";
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbDataReader readData = command.ExecuteReader();
                while (readData.Read())
                {
                    DTOClient client = new DTOClient();
                    client.IDClient = readData.GetInt32(0);
                    if (!readData.IsDBNull(1))
                        client.UserName = readData.GetString(1);
                    if (!readData.IsDBNull(2))
                        client.PassWord = readData.GetString(2);
                    if (!readData.IsDBNull(3))
                        client.IPAdr = readData.GetString(3);
                    if (!readData.IsDBNull(4))
                        client.NumPort = readData.GetInt32(4);
                    if (!readData.IsDBNull(5))
                        client.FlagSta = readData.GetBoolean(5);
                    listClient.Add(client);
                }
                readData.Close();
            }
            catch (Exception)
            {
                listClient = new List<DTOClient>();
            }
            finally
            {
                try
                {
                    if (connect == null && connect.State == System.Data.ConnectionState.Open)
                        connect.Close();
                }
                catch (NullReferenceException)
                {
                }
            }
            return listClient;
        }
        public static bool Insert(DTOClient client)
        {
            OleDbConnection connect = null;
            bool result = true;
            try
            {
                connect = OpenConnect();
                string stringCommand = @"INSERT INTO [Client] (Username,[Password]) VALUES (@User,@Pass)";
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbParameter parameter;

                parameter = new OleDbParameter("@Use", OleDbType.VarChar);
                parameter.Value = client.UserName;
                command.Parameters.Add(parameter);
                
                parameter = new OleDbParameter("@Pass", OleDbType.VarChar);
                parameter.Value = client.PassWord;
                command.Parameters.Add(parameter);
                
                command.ExecuteNonQuery();
               
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                try
                {
                    if (connect == null && connect.State == System.Data.ConnectionState.Open)
                        connect.Close();
                }
                catch (NullReferenceException)
                {
                }
            }
            return result;
        }
        public static bool Update(DTOClient client)
        {
            OleDbConnection connect = null;
            bool result = true;
            try
            {
                connect = OpenConnect();
                string stringCommand = @"UPDATE [Client] SET IPAdr = @IPAdr, NumPort = @Numport,FlagSta =@FlagSta WHERE Username = @Username";
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbParameter parameter;

                parameter = new OleDbParameter("@IPAdr", OleDbType.VarChar);
                parameter.Value = client.IPAdr;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@NumPort", OleDbType.Integer);
                parameter.Value = client.NumPort;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@FlagSta", OleDbType.Boolean);
                parameter.Value = client.FlagSta;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@Username", OleDbType.VarChar);
                parameter.Value = client.UserName;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (connect == null && connect.State == System.Data.ConnectionState.Open)
                    connect.Close();
            }
            return result;
        }
        public static bool Delete(string userName)
        {
            OleDbConnection connect = null;
            bool result = true;
            try
            {
                connect = OpenConnect();
                string stringCommand = "DELETE FROM [Client] WHERE Username = @Username";
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbParameter parameter;
                parameter = new OleDbParameter("@Username", OleDbType.VarChar);
                parameter.Value = userName;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                if (connect == null && connect.State == System.Data.ConnectionState.Open)
                    connect.Close();
            }
            return result;
        }
        public static bool Test(string userName)
        {
            bool result = true;
            List<DTOClient> list = getListClient();
            if (list.Count > 0)
                for (int i = 0; i < list.Count; i++)
                    if (list[i].UserName == userName)
                    {
                        result = false;
                        break;
                    }
            return result;
        }
        public static bool TestLogin(string userName, string passWord)
        {
            bool result = false;
            List<DTOClient> list = getListClient();
            for (int i = 0; i < list.Count; i++)
                if (string.Compare(list[i].UserName, userName, true) == 0 && string.Compare(list[i].PassWord, passWord, false) == 0 && list[i].FlagSta == false)
                {
                    result = true;
                    break;
                }
            return result;
        }
        public static DTOClient Search(int idClient)
        {
            List<DTOClient> list = getListClient();
            foreach (DTOClient client in list)
                if (client.IDClient == idClient )
                    return client;
            return null;
        }
    }
}
