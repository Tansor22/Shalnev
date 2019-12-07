using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class AbstractConnectionImpl : AbstractConnection
    {
        private SqlConnection c;

        public AbstractConnectionImpl(SqlConnection c) {
            this.c = c;
        }
        public AbstractTransaction BeginTransaction()
        {
            return new AbstractTransactionImpl(c.BeginTransaction());
        }

        public void Close()
        {
            c.Close();
        }

        public SqlConnection GetConnection()
        {
           return c;
           
        }

        public void Open()
        {
           c.Open();
        }
    }
}
