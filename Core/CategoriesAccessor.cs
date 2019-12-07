using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class CategoriesAccessor : AbstractDataAccessor
    {
        protected override string[] columnNames()
        {
            return new string[] { "name", "auditory_id" };
        }

        protected override string id()
        {
            return "category_id";
        }

        protected override string tableName()
        {
            return "categories";
        }
    }
}
