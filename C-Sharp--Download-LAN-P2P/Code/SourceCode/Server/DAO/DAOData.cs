using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DTO;
using System.Data.OleDb;

namespace DAO
{
    public class DAOData:Connect
    {
        public static List<DTOData> getListData()
        {
            OleDbConnection connect = null;
            List<DTOData> listData = new List<DTOData>();

            try
            {
                connect = OpenConnect();
                string stringCommand = "SELECT [IDData],[FileName],[IDClient],[Size],[FlagSta] FROM Data"; 
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbDataReader readData = command.ExecuteReader();
                
                while (readData.Read())
                {
                    DTOData data = new DTOData();
                    data.IDData = readData.GetInt32(0);
                    if (!readData.IsDBNull(1))
                        data.FileName = readData.GetString(1);
                    if (!readData.IsDBNull(2))
                        data.IDClient = readData.GetInt32(2);
                    if (!readData.IsDBNull(3))
                        data.Size = readData.GetInt32(3);
                    if (!readData.IsDBNull(4))
                        data.FlagSta = readData.GetBoolean(4);

                    listData.Add(data);
                }
                readData.Close();
            }
            catch (Exception)
            {
                listData = new List<DTOData>();
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
            return listData;
        }
        public static bool Insert(DTOData data)
        {
            OleDbConnection connect = null;
            bool result = true;
            try
            {
                connect = OpenConnect();
                string stringCommand = "INSERT INTO [Data] ([FileName],IDClient,[Size],FlagSta) VALUES (@FileName,@IDClient,@Size,@FlagSta)";
                //stringCommand += "";
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbParameter parameter;

                parameter = new OleDbParameter("@FileName", OleDbType.VarChar);
                parameter.Value = data.FileName;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@IDClient", OleDbType.Integer);
                parameter.Value = data.IDClient;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@Size", OleDbType.Integer);
                parameter.Value = data.Size;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@FlagSta", OleDbType.Boolean);
                parameter.Value = data.FlagSta;
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
                catch (Exception)
                {
                }
            }
            return result;
        }
        public static bool Update(DTOData data)
        {
            OleDbConnection connect = null;
            bool result = true;
            try
            {
                connect = OpenConnect();
                string stringCommand = "UPDATE [Data] SET [FileName] = @FileName,[IDClient] = @IDClient,[Size] =@Size,[FlagSta] =@FlagSta WHERE IDData = @IDData";
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbParameter parameter;

                parameter = new OleDbParameter("@FileName", OleDbType.VarChar);
                parameter.Value = data.FileName;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@IDClient", OleDbType.Integer);
                parameter.Value = data.IDClient;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@Size", OleDbType.Integer);
                parameter.Value = data.Size;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@FlagSta", OleDbType.Boolean);
                parameter.Value = data.FlagSta;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@IDData", OleDbType.Integer);
                parameter.Value = data.IDData;
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
        public static bool Delete(string fileName,int idClient)
        {
            OleDbConnection connect = null;
            bool result = true;
            try
            {
                connect = OpenConnect();
                string stringCommand = "DELETE FROM [Data] WHERE FileName = @FileName and IDClient = @IDClient";
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbParameter parameter;
                parameter = new OleDbParameter("@FileName", OleDbType.VarChar);
                parameter.Value = fileName;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@IDClient", OleDbType.Integer);
                parameter.Value = idClient;
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
        public static bool DeleteAll(int idClient)
        {
            OleDbConnection connect = null;
            bool result = true;
            try
            {
                connect = OpenConnect();
                string stringCommand = "DELETE FROM [Data] WHERE IDClient = @IDClient";
                OleDbCommand command = new OleDbCommand(stringCommand, connect);
                OleDbParameter parameter;
      
                parameter = new OleDbParameter("@IDClient", OleDbType.Integer);
                parameter.Value = idClient;
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
        public static List<DTOData> Search(string fileName, string Client)
        {
            int idClient = 0;
            List<DTOClient> listClient = DAOClient.getListClient();
            foreach (DTOClient client in listClient)
                if (client.UserName == Client)
                    idClient = client.IDClient;

            List<DTOData> listFile = new List<DTOData>();
            List<DTOData> listData = getListData();
            if (listData.Count > 0)
                for (int i = 0; i < listData.Count; i++)
                {
                    string str1 = listData[i].FileName.ToLower();
                    if (str1.Contains(fileName.ToLower()) == true && listData[i].IDClient != idClient)
                        listFile.Add(listData[i]);
                }
            return listFile;
        }
        public static bool checkFile(string fileName, int idClient)
        {
            bool result = true;
            List<DTOData> list = getListData();
            foreach (DTOData data in list)
                if (data.FileName == fileName && data.IDClient == idClient)
                    result = false;
            return result;
        }                
    }
}
