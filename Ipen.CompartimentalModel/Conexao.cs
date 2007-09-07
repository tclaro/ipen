using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;

namespace Ipen.CompartimentalModel
{
    public class Conexao
    {
        public static string Arquivo;

        public static OleDbConnection Conectar()
        {
            string connectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                                    "Data Source=" + Arquivo + ";" +
                                    "Persist Security Info=False;";
            OleDbConnection cn = new OleDbConnection(connectionstring);
            cn.Open();
            return cn;
        }
    }
}
