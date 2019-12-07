using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class AuditoriesAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] {"name"};
        }

        protected override string id()
        {
           return "auditory_id";
        }

        protected override string tableName()
        {
            return "auditories";
        }
    }
}
