using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace DAO
{
    public class Connect
    {
        private static string stringConnect = "Provider=Microsoft.Jet.OleDb.4.0;Data Source= SERVER.mdb";
        protected static OleDbConnection OpenConnect()
        {
            OleDbConnection connect = new OleDbConnection(stringConnect);
            connect.Open();
            return connect;
        }
    }
}
