using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class AuthorsAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] { "first_name", "second_name"};
        }

        protected override string id()
        {
            return "author_id";
        }

        protected override string tableName()
        {
            return "authors";
        }
    }
}
