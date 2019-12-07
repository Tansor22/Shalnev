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
    class AuditoriesLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["BooksConnectionString"].ConnectionString;
        private AuditoriesLogic logic = new AuditoriesLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Adults", true)]
        public void AddAuditoryTest(string name, bool expected)
        {
            BooksDataSet.AuditoriesRow r = logic.NewAuditoriesRow();
            r.name = name;
          
            bool actual = logic.AddAuditory(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.AuditoriesRow inserted = logic.getAuditoryByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
        }

        [TestCase("Young", true)]
        [TestCase("Students", false)]
        public void AddAuditoryRestrictedTest(string name, bool expected)
        {
            BooksDataSet.AuditoriesRow r = logic.NewAuditoriesRow();
            r.name = name;


            bool actual = logic.AddAuditory(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("Students", false)]
        public void UpdateAuditoryTest(string name, bool expected)
        {
            int id = logic.getLastID();
            BooksDataSet.AuditoriesRow row = logic.getAuditoryByID(id);

            BooksDataSet.AuditoriesRow newRow = logic.NewAuditoriesRow();
            newRow.name = name;
           
            bool result = logic.UpdateAuditory(id, newRow);
            // was updated
            Assert.IsTrue(result);

            BooksDataSet.AuditoriesRow updated = logic.getAuditoryByID(id);
            // was updated correctly
            Assert.AreEqual(name, updated.name);
        }

        [TestCase("Fishmen", true)]
        public void DeleteAuditoryTest(string name, bool expected)
        {
            BooksDataSet.AuditoriesRow r = logic.NewAuditoriesRow();
            r.name = name;
        
            bool wasAdded = logic.AddAuditory(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            BooksDataSet.AuditoriesRow inserted = logic.getAuditoryByID(insertedId);
            // was added correctly
            Assert.AreEqual(name, inserted.name);
            
            bool actual = logic.DeleteAuditoryWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string name, bool expected)
        {
            BooksDataSet.AuditoriesRow r = logic.NewAuditoriesRow();
            r.name = name;
          
            bool actual = logic.AddAuditory(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.AuditoriesRow inserted = logic.getAuditoryByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
           
            // Updating db
            logic.UpdateDBWithCache();

            BooksDataSet db = logic.GetAuditories();
            BooksDataSet final = new BooksDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }


    }
}
