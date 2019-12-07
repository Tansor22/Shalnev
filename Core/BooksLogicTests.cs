using NUnit.Framework;
using System.Configuration;

namespace Core
{
    [TestFixture]
    class BooksLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["BooksConnectionString"].ConnectionString;
        private BooksLogic logic = new BooksLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Inserterd Book", 10, 12.5f, 100, 100, true)]
        public void AddBookTest(string title, int publisher_id, float price, int author_id, int category_id, bool expected)
        {
            BooksDataSet.BooksRow r = logic.NewBooksRow();
            r.title = title;
            r.publisher_id = publisher_id;
            r.price = price;
            r.author_id = author_id;
            r.category_id = category_id;

            bool actual = logic.AddBook(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.BooksRow inserted = logic.getBookByID(id);
            // was added properly
            Assert.AreEqual(title, inserted.title);
            Assert.AreEqual(publisher_id, inserted.publisher_id);
            Assert.AreEqual(price, inserted.price);
            Assert.AreEqual(author_id, inserted.author_id);
        }

        [TestCase("Some Title", 10, 12.5f, 11, 100, true)]
        [TestCase("Some Title", 10, 12.5f, 11, 100, true)]
        [TestCase("Some Title", 10, 12.5f, 11, 100, true)]
        [TestCase("Another Title", 10, 12.5f, 11, 100, false)]
        [TestCase("Another Title", 10, 12.5f, 11, 100, false)]
        public void AddBookRestrictedTest(string title, int publisher_id, float price, int author_id, int category_id, bool expected)
        {
            BooksDataSet.BooksRow r = logic.NewBooksRow();
            r.title = title;
            r.publisher_id = publisher_id;
            r.price = price;
            r.author_id = author_id;
            r.category_id = category_id;

            bool actual = logic.AddBook(r);
            Assert.AreEqual(expected, actual);

        }
        [TestCase("Updated book", 123.2f)]
        public void UpdateBookTest(string title, float price)
        {
            int id = logic.getLastID();
            BooksDataSet.BooksRow row = logic.getBookByID(id);
            /// ?????? ---new BooksDataSet.BooksDataTable().NewBooksRow()
            BooksDataSet.BooksRow newRow = logic.NewBooksRow();
            newRow.title = title;
            newRow.price = price;

            bool result = logic.UpdateBook(id, newRow);
            // was updated
            Assert.IsTrue(result);

            BooksDataSet.BooksRow updated = logic.getBookByID(id);
            // was updated correctly
            Assert.AreEqual(updated.title, title);
            Assert.AreEqual(updated.price, price);
            // the same values
            Assert.AreEqual(updated.author_id, row.author_id);
            Assert.AreEqual(updated.category_id, row.category_id);
            Assert.AreEqual(updated.publisher_id, row.publisher_id);
        }
        [TestCase("Deleted book", 10, 12.5f, 100, 100, true)]
        public void DeleteBookTest(string title, int publisher_id, float price, int author_id, int category_id, bool expected)
        {
            BooksDataSet.BooksRow r = logic.NewBooksRow();
            r.title = title;
            r.publisher_id = publisher_id;
            r.price = price;
            r.author_id = author_id;
            r.category_id = category_id;

            bool wasAdded = logic.AddBook(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            BooksDataSet.BooksRow inserted = logic.getBookByID(insertedId);
            // was added correctly
            Assert.AreEqual(inserted.title, title);
            Assert.AreEqual(inserted.publisher_id, publisher_id);
            Assert.AreEqual(inserted.category_id, category_id);
            Assert.AreEqual(inserted.price, price);
            Assert.AreEqual(inserted.author_id, author_id);

            bool actual = logic.DeleteBookWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", 10, 12.5f, 2, 100, true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string title, int publisher_id, float price, int author_id, int category_id, bool expected)
        {
            BooksDataSet.BooksRow r = logic.NewBooksRow();
            r.title = title;
            r.publisher_id = publisher_id;
            r.price = price;
            r.author_id = author_id;
            r.category_id = category_id;

            bool actual = logic.AddBook(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.BooksRow inserted = logic.getBookByID(id);
            // was added properly
            Assert.AreEqual(inserted.title, title);
            Assert.AreEqual(inserted.publisher_id, publisher_id);
            Assert.AreEqual(inserted.price, price);
            Assert.AreEqual(inserted.author_id, author_id);
            Assert.AreEqual(inserted.category_id, category_id);

            // Updating db
            logic.UpdateDBWithCache();

            BooksDataSet db = logic.getBooksDirectlyFromDb();
            BooksDataSet final = new BooksDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }
    }
}
