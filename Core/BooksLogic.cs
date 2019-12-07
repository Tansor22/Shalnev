using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BooksLogic : AbstractLogic
    {
        public BooksLogic(AbstractConnection c) : base(c)
        {
        }
      
        public BooksDataSet getBooks() {   
            return Cache;
        }

        public BooksDataSet getBooksDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddBook(BooksDataSet.BooksRow row)
        {
            return AddRecord(row);
        }

        public BooksDataSet.BooksRow NewBooksRow() {
            return (BooksDataSet.BooksRow) NewRow();
        }

        public bool DeleteBookWithID(int id) {
            //getBookByID(id).Delete();
            //return true;
            return DeleteRecordWithId(id);
        }

        public BooksDataSet.BooksRow getBookByID(int id)
        {
            return (BooksDataSet.BooksRow) getRecordWithId(id);//return Cache.Books.FindBybook_id(id);
        }

        public bool UpdateBook(int id, BooksDataSet.BooksRow row) {
            /* BooksDataSet.BooksRow oldRow = getBookByID(id);
            if (!row.IsNull("title"))
                oldRow.title = row.title;
            if (!row.IsNull("price"))
                oldRow.price = row.price;
            // eto prosto pizdec
            if (!row.IsNull("publisher_id"))
                oldRow.publisher_id = row.publisher_id;
            if (!row.IsNull("author_id"))
                oldRow.author_id = row.author_id;
            if (!row.IsNull("category_id"))
                oldRow.category_id = row.category_id;
            return true; */
            return UpdateRecord(id, row);
        }
        protected override AbstractDataAccessor provideWithAccessor()
        {
            return new BooksAccessor();
        }
       
    }
}
