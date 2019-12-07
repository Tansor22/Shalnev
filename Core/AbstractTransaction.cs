using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface AbstractTransaction
    {
        void Commit();
        void Rollback();
        SqlTransaction GetTransaction();
    }
}
