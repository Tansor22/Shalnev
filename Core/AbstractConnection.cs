using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface AbstractConnection
    {
        void Open();
        void Close();
        AbstractTransaction BeginTransaction();
        SqlConnection GetConnection();
    }
}
