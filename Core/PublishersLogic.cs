using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PublishersLogic : AbstractLogic
    {
        public PublishersLogic(AbstractConnection c) : base(c)
        {
        }
        public BooksDataSet getPublishers()
        {
            return Cache;
        }

        public BooksDataSet getPublishersDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddPublisher(BooksDataSet.PublishersRow row)
        {
            return AddRecord(row);
        }
        public BooksDataSet.PublishersRow NewPublishersRow()
        {
            return (BooksDataSet.PublishersRow) NewRow();
        }
        public bool DeletePublisherWithID(int id)
        {
            //getPublisherByID(id).Delete();
            //return true;
            return DeleteRecordWithId(id);
        }
        public BooksDataSet.PublishersRow getPublisherByID(int id)
        {
            //return Cache.Publishers.FindBypublisher_id(id);
            return (BooksDataSet.PublishersRow) getRecordWithId(id);
        }
        public bool UpdatePublisher(int id, BooksDataSet.PublishersRow row)
        {
            /*BooksDataSet.PublishersRow oldRow = getPublisherByID(id);
            if (!row.IsNull("name"))
                oldRow.name = row.name;
            if (!row.IsNull("url"))
                oldRow.url = row.url;
            return true;*/
            return UpdateRecord(id, row);
        }
        protected override AbstractDataAccessor provideWithAccessor()
        {
            return new PublisherAccessor();
        }
    }
}
