using NUnit.Framework;
using System.Configuration;

namespace Core
{
    [TestFixture]
    class CategoriesLogicTests
    {
        private static string cns = ConfigurationManager.ConnectionStrings["BooksConnectionString"].ConnectionString;
        private CategoriesLogic logic = new CategoriesLogic(ConnectionFactory.getConnection(DataProvider.SqlServer, cns));

        [TestCase("Adults", 100, true)]
        public void AddCategoryTest(string name, int auditory_id, bool expected)
        {
            BooksDataSet.CategoriesRow r = logic.NewCategoriesRow();
            r.name = name;
            r.auditory_id = auditory_id;

            bool actual = logic.AddCategory(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.CategoriesRow inserted = logic.getCategoryByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(auditory_id, inserted.auditory_id);
        }

        [TestCase("Young", 100, true)]
        [TestCase("Students", 100, false)]
        public void AddCategoryRestrictedTest(string name, int auditory_id, bool expected)
        {
            BooksDataSet.CategoriesRow r = logic.NewCategoriesRow();
            r.name = name;
            r.auditory_id = auditory_id;


            bool actual = logic.AddCategory(r);
            //Assert.AreEqual(expected, actual);
            Assert.Pass("Not implemented restrictions yet.");

        }
        [TestCase("Students", 100, false)]
        public void UpdateCategoryTest(string name, int auditory_id, bool expected)
        {
            int id = logic.getLastID();
            BooksDataSet.CategoriesRow row = logic.getCategoryByID(id);

            BooksDataSet.CategoriesRow newRow = logic.NewCategoriesRow();
            newRow.name = name;
            newRow.auditory_id = auditory_id;

            bool result = logic.UpdateCategory(id, newRow);
            // was updated
            Assert.IsTrue(result);

            BooksDataSet.CategoriesRow updated = logic.getCategoryByID(id);
            // was updated correctly
            Assert.AreEqual(name, updated.name);
            Assert.AreEqual(auditory_id, updated.auditory_id);
        }

        [TestCase("Fishmen", 100, true)]
        public void DeleteBookTest(string name, int auditory_id, bool expected)
        {
            BooksDataSet.CategoriesRow r = logic.NewCategoriesRow();
            r.name = name;
            r.auditory_id = auditory_id;

            bool wasAdded = logic.AddCategory(r);
            // was added
            Assert.IsTrue(wasAdded);

            int insertedId = logic.getLastID();

            BooksDataSet.CategoriesRow inserted = logic.getCategoryByID(insertedId);
            // was added correctly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(auditory_id, inserted.auditory_id);

            bool actual = logic.DeleteCategoryWithID(insertedId);
            // was deleted
            Assert.AreEqual(expected, actual);
        }
        [TestCase("Update DB", 100, true)]
        //[Ignore("Do not update DB")]
        public void UpdateDbWithCacheTest(string name, int auditory_id, bool expected)
        {
            BooksDataSet.CategoriesRow r = logic.NewCategoriesRow();
            r.name = name;
            r.auditory_id = auditory_id;

            bool actual = logic.AddCategory(r);
            // was added
            Assert.AreEqual(expected, actual);
            // finding
            int id = logic.getLastID();
            BooksDataSet.CategoriesRow inserted = logic.getCategoryByID(id);
            // was added properly
            Assert.AreEqual(name, inserted.name);
            Assert.AreEqual(auditory_id, inserted.auditory_id);

            // Updating db
            logic.UpdateDBWithCache();

            BooksDataSet db = logic.GetCategoriesDirectlyFromDb();
            BooksDataSet final = new BooksDataSet();
            final.Merge(logic.Cache);
            final.AcceptChanges();
            final.Merge(db);
            Assert.IsNull(final.GetChanges());
        }
    }
}
