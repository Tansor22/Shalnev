using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class PublisherAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] { "name", "url"};
        }

        protected override string id()
        {
           return "publisher_id";
        }

        protected override string tableName()
        {
            return "publishers";
        }
    }
}
