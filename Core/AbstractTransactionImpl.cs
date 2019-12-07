using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class AbstractTransactionImpl : AbstractTransaction
    {
        private SqlTransaction t;

        public AbstractTransactionImpl(SqlTransaction t) {
            this.t = t;
        }
            
        public void Commit()
        {
            t.Commit();
        }

        public void Rollback()
        {
            t.Rollback();
        }

        public SqlTransaction GetTransaction()
        {
            return t;
        }
    }
}
