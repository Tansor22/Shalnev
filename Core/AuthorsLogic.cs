using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class AuthorsLogic : AbstractLogic
    {
        public AuthorsLogic(AbstractConnection c) : base(c)
        {
        }
        public BooksDataSet getAuthors()
        {
            return Cache;
        }

        public BooksDataSet getAuthorsDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddAuthor(BooksDataSet.AuthorsRow row)
        {
            return AddRecord(row);
        }

        public BooksDataSet.AuthorsRow NewAuthorsRow()
        {
            return (BooksDataSet.AuthorsRow) NewRow();
        }

        public bool DeleteAuthorWithID(int id)
        {
            //getAuthorByID(id).Delete();
            return DeleteRecordWithId(id);
        }

        public BooksDataSet.AuthorsRow getAuthorByID(int id)
        {
            //return Cache.Authors.FindByauthor_id(id);
            return (BooksDataSet.AuthorsRow) getRecordWithId(id);
        }

        public bool UpdateAuthor(int id, BooksDataSet.AuthorsRow row)
        {
            /*BooksDataSet.AuthorsRow oldRow = getAuthorByID(id);
            if (!row.IsNull("first_name"))
                oldRow.first_name = row.first_name;
            if (!row.IsNull("second_name"))
                oldRow.second_name = row.second_name;
            return true;*/
            return UpdateRecord(id, row);
        }

        protected override AbstractDataAccessor provideWithAccessor()
        {
           return new AuthorsAccessor();
        }
    }
}
