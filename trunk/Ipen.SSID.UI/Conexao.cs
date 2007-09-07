using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
// teste 22-05-2007

namespace Ipen.SSID.UI
{
    class Conexao
    {
        public static OleDbConnection Conectar()
        {
            string connectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;" + 
                                    "Data Source=" + System.Windows.Forms.Application.StartupPath + "\\modelos.mdb;" + 
                                    "Persist Security Info=False;";
            OleDbConnection cn = new OleDbConnection(connectionstring);
            cn.Open();
            return cn;
        }
    }
}
