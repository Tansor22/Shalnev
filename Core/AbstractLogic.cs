using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class AbstractLogic
    {
        private AbstractDataAccessor _a;
        protected AbstractDataAccessor Accessor
        {
            get
            {
                if (_a == null) {
                    _a = provideWithAccessor();
                }
                return _a;
            }       
        }
        private AbstractConnection _c;
        protected AbstractConnection Connection
        {
            get
            {
                return _c;
            }
        }
        public BooksDataSet _cache;
        public BooksDataSet Cache
        {
            get
            {
                if (_cache == null) {
                    _cache = new BooksDataSet();
                }
                return _cache;
            }
        }
        protected abstract AbstractDataAccessor provideWithAccessor();

        public AbstractLogic(AbstractConnection c) {        
            _c = c;
            // getting all records from db
            Connection.Open();

            AbstractTransaction t = Connection.BeginTransaction();
            Accessor.Read(t, Connection, Cache);
            t.Commit();

            Connection.Close();
        }
        public BooksDataSet getRecordsDirectlyFromDb()
        {
            BooksDataSet ds = new BooksDataSet();
            // getting all records from db
            Connection.Open();

            AbstractTransaction t = Connection.BeginTransaction();
            Accessor.Read(t, Connection, ds);
            t.Commit();

            Connection.Close();
            return ds;
        }
        public DataRow NewRow() {
            return Cache.Tables[Accessor.TableName].NewRow();
        }
        public DataRow getRecordWithId(int id) {
            return Cache.Tables[Accessor.TableName].Rows.Find(id);
        }
        public bool UpdateRecord(int id, DataRow row) {
            DataRow oldRow = getRecordWithId(id);
            if (oldRow != null)
            {
                foreach (string columnName in Accessor.Columns) {
                    if (!row.IsNull(columnName))
                        oldRow[columnName] = row[columnName];
                }
                return true;
            }
            else return false;
        }
        public bool DeleteRecordWithId(int id)
        {
            DataRow toDelete = getRecordWithId(id);
            if (toDelete != null)
            {
                toDelete.Delete();
                return true;
            }
            else return true;
        }
        public bool AddRecord(DataRow row) {
            // restriction for TableName
            foreach (Restriction restriction in Restriction.getRestrictions(Cache, Accessor.TableName))
            {
                if (!restriction.Validate(row))
                    return false;
            }
            Cache.Tables[Accessor.TableName].Rows.Add(row);
            //Cache.AcceptChanges();
            return true;
        }
        public int getLastID()
        {
            return (int) Cache.Tables[Accessor.TableName].AsEnumerable().Max(r => r[Accessor.Id]);
        }
       
        public bool UpdateDBWithCache()
        {
            Connection.Open();
            AbstractTransaction t = Connection.BeginTransaction();
            Accessor.Write(t, Connection, Cache);
            t.Commit();
            Connection.Close();
            Cache.AcceptChanges();
            return true;
        }
        public bool UpdateDBWithCache(BooksDataSet cache)
        {
            Connection.Open();
            AbstractTransaction t = Connection.BeginTransaction();
            Accessor.Write(t, Connection, cache);
            t.Commit();
            Connection.Close();
            //cache.AcceptChanges();
            return true;
        }
    }
}
