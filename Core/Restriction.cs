using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Restriction
    {
        Predicate<DataRow> predicate;
        BooksDataSet dataSet;
        private Restriction(Predicate<DataRow> p, BooksDataSet ds) {
            predicate = p;
            dataSet = ds;
        }
        public bool Validate(DataRow row) {
            return predicate.Invoke(row);
        }
        public static List<Restriction> getRestrictions(BooksDataSet ds, string tableName) {
            List<Restriction> restrictions = new List<Restriction>();
            switch (tableName.ToLower()) {
                case "books":
                    fillBooksRestrictions(ds, restrictions);
                        break;
                case "publishers":
                    fillPublishersRestrictions(ds, restrictions);
                    break;

            }
            return restrictions;
        }

        private static void fillPublishersRestrictions(BooksDataSet ds, List<Restriction> restrictions)
        {
            List<string> forbiddenUrls = new List<string> { "www.mail.ru" };
            restrictions.Add(new Restriction(row =>
            {
                BooksDataSet.PublishersRow publishersRow = (BooksDataSet.PublishersRow)row;
                return !forbiddenUrls.Contains(publishersRow.url);
            }, ds));
        }

        private static void fillBooksRestrictions(BooksDataSet ds, List<Restriction> restrictions) {
            int booksLimit = 2;
            restrictions.Add(new Restriction(row => 
            {
                BooksDataSet.BooksRow booksRow = (BooksDataSet.BooksRow) row;
                return ds.Books.AsEnumerable().Where(r => r.author_id == booksRow.author_id).Count() <= booksLimit;
            }, ds));
        }
    }
}
