namespace Core
{
    public class CategoriesLogic : AbstractLogic
    {
        public CategoriesLogic(AbstractConnection c) : base(c)
        {
        }

        public BooksDataSet GetCategories()
        {
            return Cache;
        }

        public BooksDataSet GetCategoriesDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddCategory(BooksDataSet.CategoriesRow row)
        {
            return AddRecord(row);
        }

        public BooksDataSet.CategoriesRow NewCategoriesRow()
        {
            return (BooksDataSet.CategoriesRow) NewRow();
        }

        public bool DeleteCategoryWithID(int id)
        {
            //getCategoryByID(id).Delete();
            //return true;
            return DeleteRecordWithId(id);
        }

        public BooksDataSet.CategoriesRow getCategoryByID(int id)
        {
            //return Cache.Categories.FindBycategory_id(id);
            return (BooksDataSet.CategoriesRow) getRecordWithId(id);
        }

        public bool UpdateCategory(int id, BooksDataSet.CategoriesRow row)
        {
            /*BooksDataSet.CategoriesRow oldRow = getCategoryByID(id);
            if (!row.IsNull("name"))
                oldRow.name = row.name;
            if (!row.IsNull("auditory_id"))
                oldRow.auditory_id = row.auditory_id;
            return true;*/
            return UpdateRecord(id, row);
        }

        protected override AbstractDataAccessor provideWithAccessor()
        {
            return new CategoriesAccessor();
        }
    }
}
