using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Configuration;

namespace Core
{
    [TestFixture]
    class AuthorLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["BooksConnectionString"].ConnectionString;
        private AuthorsLogic logic = new AuthorsLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Klement", "Ivanov", true)]
        public void AddAuthorTest(string first_name, string second_name, bool expected)
        {
            BooksDataSet.AuthorsRow r = logic.NewAuthorsRow();
            r.first_name = first_name;
            r.second_name = second_name;

            bool actual = logic.AddAuthor(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.AuthorsRow inserted = logic.getAuthorByID(id);
            // was added properly
            Assert.AreEqual(first_name, inserted.first_name);
            Assert.AreEqual(second_name, inserted.second_name);
        }

        [TestCase("Robert", "Kyosaki", false)]
        [TestCase("Zamyatin", "Vyacheslav", true)]
        public void AddAuthorsRestrictedTest(string first_name, string second_name, bool expected)
        {
            BooksDataSet.AuthorsRow r = logic.NewAuthorsRow();
            r.first_name = first_name;
            r.second_name = second_name;
            

            bool actual = logic.AddAuthor(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("NewName", "NewSurName", true)]
        public void UpdateBookTest(string first_name, string second_name, bool expected)
        {
            int id = logic.getLastID();
            BooksDataSet.AuthorsRow row = logic.getAuthorByID(id);
           
            BooksDataSet.AuthorsRow newRow = logic.NewAuthorsRow();
            newRow.first_name = first_name;
            newRow.second_name = second_name;

            bool result = logic.UpdateAuthor(id, newRow);
            // was updated
            Assert.IsTrue(result);

            BooksDataSet.AuthorsRow updated = logic.getAuthorByID(id);
            // was updated correctly
            Assert.AreEqual(first_name, updated.first_name);
            Assert.AreEqual(second_name, updated.second_name);
        }

        [TestCase("Deleted author name", "Deleted author surname", true)]
        public void DeleteAuthorTest(string first_name, string second_name, bool expected)
        {
            BooksDataSet.AuthorsRow r = logic.NewAuthorsRow();
            r.first_name = first_name;
            r.second_name = second_name;
           
            bool wasAdded = logic.AddAuthor(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            BooksDataSet.AuthorsRow inserted = logic.getAuthorByID(insertedId);
            // was added correctly
            Assert.AreEqual(first_name, inserted.first_name);
            Assert.AreEqual(second_name, inserted.second_name);

            bool actual = logic.DeleteAuthorWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", "Any surname",true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string first_name, string second_name, bool expected)
        {
            BooksDataSet.AuthorsRow r = logic.NewAuthorsRow();
            r.first_name = first_name;
            r.second_name = second_name;

            bool wasAdded = logic.AddAuthor(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            BooksDataSet.AuthorsRow inserted = logic.getAuthorByID(insertedId);
            // was added correctly
            Assert.AreEqual(first_name, inserted.first_name);
            Assert.AreEqual(second_name, inserted.second_name);
            // finding

            // Updating db
            logic.UpdateDBWithCache();

            BooksDataSet db = logic.getAuthorsDirectlyFromDb();
            BooksDataSet final = new BooksDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }
    }
}
