using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Client
{
    public class DAOInfor:Connect
    {
        public static List<DTOInfor> getListInfor()
        {
            OleDbConnection connect = null;
            List<DTOInfor> listInfor = new List<DTOInfor>();
            try
            {
                connect = openConnect();
                string strCommand = "SELECT * FROM Infor";
                OleDbCommand command = new OleDbCommand(strCommand, connect);
                OleDbDataReader read = command.ExecuteReader();
                 while (read.Read())
                {
                    DTOInfor infor = new DTOInfor();
                    infor.Id = read.GetInt32(0);
                    infor.FileName = read.GetString(1);
                    infor.Size = read.GetInt32(2);
                    infor.IpClient = read.GetString(3);
                    infor.PortClient = read.GetInt32(4);
                    infor.FromClient = read.GetString(5);
                    infor.Flag = read.GetBoolean(6);
                    listInfor.Add(infor);
                }
                read.Close();
            }
            catch (Exception)
            {
                listInfor = new List<DTOInfor>();
            }
            finally
            {
                if (connect == null && connect.State == System.Data.ConnectionState.Open)
                    connect.Close();
            }
            return listInfor;
        }
        public static bool Insert(DTOInfor infor)
        {
            bool result = true;
            OleDbConnection connect = null;
            try
            {
                connect = openConnect();
                string strCommand = "INSERT INTO [Infor] ([FileName],[Size],[IPClient],[PortClient],[FromClient],[Finish]) VALUES (@file,@size,@ip,@port,@client,@flag)";
                OleDbCommand command = new OleDbCommand(strCommand, connect);

                OleDbParameter parameter;
                parameter = new OleDbParameter("@file", OleDbType.VarChar);
                parameter.Value = infor.FileName;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@size", OleDbType.Integer);
                parameter.Value = infor.Size;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@ip", OleDbType.VarChar);
                parameter.Value = infor.IpClient;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@port", OleDbType.Integer);
                parameter.Value = infor.PortClient;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@client", OleDbType.VarChar);
                parameter.Value = infor.FromClient;
                command.Parameters.Add(parameter);              

                parameter = new OleDbParameter("@flag", OleDbType.Boolean);
                parameter.Value = infor.Flag;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception )
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
        public static bool Delete(string file, string client)
        {
            bool result = true;
            OleDbConnection connect = null;
            try
            {
                connect = openConnect();
                string strCommand = "DELETE FROM Infor WHERE FileName = @file AND FromClient = @client";
                OleDbCommand command = new OleDbCommand(strCommand, connect);
                OleDbParameter parameter ;

                parameter = new OleDbParameter("@file", OleDbType.VarChar);
                parameter.Value = file;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@client", OleDbType.VarChar);
                parameter.Value = client;
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
        public static bool Update(DTOInfor infor)
        {
            bool result = true;
            OleDbConnection connect = null;
            try
            {
                connect = openConnect();
                string strCommand = "UPDATE [Infor] SET [FileName] = @file,[Size] = @size,[IPClient] = @ip,[PortClient] = @port,[FromClient] = @client,[Finish] = @flag WHERE FileName = @file AND FromClient = @client";
                OleDbCommand command = new OleDbCommand(strCommand, connect);

                OleDbParameter parameter;
                parameter = new OleDbParameter("@file", OleDbType.VarChar);
                parameter.Value = infor.FileName;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@size", OleDbType.Integer);
                parameter.Value = infor.Size;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@ip", OleDbType.VarChar);
                parameter.Value = infor.IpClient;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@port", OleDbType.Integer);
                parameter.Value = infor.PortClient;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@client", OleDbType.VarChar);
                parameter.Value = infor.FromClient;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@flag", OleDbType.Boolean);
                parameter.Value = infor.Flag;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@file", OleDbType.VarChar);
                parameter.Value = infor.FileName;
                command.Parameters.Add(parameter);

                parameter = new OleDbParameter("@client", OleDbType.VarChar);
                parameter.Value = infor.FromClient;
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception )
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
        public static bool DeleteAll()
        {
            bool result = true;
            OleDbConnection connect = null;
            try
            {
                connect = openConnect();
                string strCommand = "DELETE FROM Infor";
                OleDbCommand command = new OleDbCommand(strCommand, connect);
                OleDbParameter parameter;

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
    }
}
  
      
               
       
        
       
      
       

