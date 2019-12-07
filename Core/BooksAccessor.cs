using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BooksAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] {"author_id", "title", "publisher_id", "price", "category_id"};
        }

        protected override string tableName()
        {
            return "books";
        }
        protected override string id()
        {
            return "book_id";
        }
    }
}
