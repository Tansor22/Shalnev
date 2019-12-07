using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;

namespace Core
{
    public enum DataProvider { SqlServer, OleDb, Odbc, None }
    public class ConnectionFactory
    {

        public static AbstractConnection getConnection(DataProvider dp, string cns) {
            IDbConnection c = null;
            if (dp.Equals(DataProvider.SqlServer))
            {
                switch (dp)
                {
                    case DataProvider.SqlServer:
                        c = new SqlConnection(cns);
                        break;
                    case DataProvider.OleDb:
                        c = new OleDbConnection(cns);
                        break;
                    case DataProvider.Odbc:
                        c = new OdbcConnection(cns);
                        break;
                }
            }
            else {
                //Helper.console("FABRIKA))))))0)))");
            }
            // FABRIKA))))))0)))
        
            return new AbstractConnectionImpl( (SqlConnection) c);
        }
    }
}
