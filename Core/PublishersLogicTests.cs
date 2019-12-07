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
    class PublishersLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["BooksConnectionString"].ConnectionString;
        private PublishersLogic logic = new PublishersLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Google", "www.google.com", true)]
        public void AddPublisherTest(string name, string url, bool expected)
        {
            BooksDataSet.PublishersRow r = logic.NewPublishersRow();
            r.name = name;
            r.url = url;

            bool actual = logic.AddPublisher(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.PublishersRow inserted = logic.getPublisherByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(url, inserted.url);
        }

        [TestCase("Mail Ru", "www.mail.ru", false)]
        [TestCase("ASTU", "www.altstu.edu", true)]
        public void AddPublisherRestrictedTest(string name, string url, bool expected)
        {
            BooksDataSet.PublishersRow r = logic.NewPublishersRow();
            r.name = name;
            r.url = url;
            bool actual = logic.AddPublisher(r);
            Assert.AreEqual(expected, actual);

        }
        [TestCase("www.newurl.com")]
        public void UpdatePublisherTest(string url)
        {
            int id = logic.getLastID();
            BooksDataSet.PublishersRow row = logic.getPublisherByID(id);
            BooksDataSet.PublishersRow newRow = logic.NewPublishersRow();
            newRow.url = url;

            bool result = logic.UpdatePublisher(id, newRow);
            // was updated
            Assert.IsTrue(result);

            BooksDataSet.PublishersRow updated = logic.getPublisherByID(id);
            // was updated correctly
            Assert.AreEqual(updated.url, url);
            // the same values
            Assert.AreEqual(updated.name, row.name);
        }
        [TestCase("O'Reily", "www.oreily.com", true)]
        public void DeleteBookTest(string name, string url, bool expected)
        {
            BooksDataSet.PublishersRow r = logic.NewPublishersRow();
            r.name = name;
            r.url = url;

            bool wasAdded = logic.AddPublisher(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            BooksDataSet.PublishersRow inserted = logic.getPublisherByID(insertedId);
            // was added correctly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(url, inserted.url);
        
            bool actual = logic.DeletePublisherWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Petersburg", "www.petersburg.ru", true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string name, string url, bool expected)
        {
            BooksDataSet.PublishersRow r = logic.NewPublishersRow();
            r.name = name;
            r.url = url;

            bool actual = logic.AddPublisher(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.PublishersRow inserted = logic.getPublisherByID(id);
            // was added properly
            Assert.AreEqual(inserted.name, name);
            Assert.AreEqual(inserted.url, url);

            // Updating db
            logic.UpdateDBWithCache();

            BooksDataSet db = logic.getPublishersDirectlyFromDb();
            BooksDataSet final = new BooksDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }
    }
}
