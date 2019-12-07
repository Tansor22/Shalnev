using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class AuditoriesLogic : AbstractLogic
    {
        public AuditoriesLogic(AbstractConnection c) : base(c)
        {
        }

        public BooksDataSet GetAuditories()
        {
            return Cache;
        }

        public BooksDataSet GetAuditoriesDirectlyFromDb()
        {
            return getRecordsDirectlyFromDb();
        }
        public bool AddAuditory(BooksDataSet.AuditoriesRow row)
        {
            return AddRecord(row);
        }

        public BooksDataSet.AuditoriesRow NewAuditoriesRow()
        {
            return (BooksDataSet.AuditoriesRow)NewRow();
        }

        public bool DeleteAuditoryWithID(int id)
        {
           // getAuditoryByID(id).Delete();
           // return true;
            return DeleteRecordWithId(id);
        }

        public BooksDataSet.AuditoriesRow getAuditoryByID(int id)
        {
            return (BooksDataSet.AuditoriesRow) getRecordWithId(id);
            //return (BooksDataSet.AuditoriesRow)Cache.Tables[Accessor.TableName].Rows.Find("" + id);// FindByauditory_id(id);
        }

        public bool UpdateAuditory(int id, BooksDataSet.AuditoriesRow row)
        {
            /*BooksDataSet.AuditoriesRow oldRow = getAuditoryByID(id);
            if (!row.IsNull("name"))
                oldRow.name = row.name;
            return true;*/
            return UpdateRecord(id, row);
        }


        protected override AbstractDataAccessor provideWithAccessor()
        {
           return new AuditoriesAccessor();
        }
    }
}
