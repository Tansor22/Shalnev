using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public abstract class AbstractDataAccessor
    {
        protected abstract string tableName();
        public string TableName {
            get {
                return tableName();
            }
        }
        protected abstract string[] columnNames();
        public string[] Columns
        {
            get
            {
                return columnNames();
            }
        }
        protected abstract string id();
        public string Id
        {
            get
            {
                return id();
            }
        }
     
        private void bindParams(SqlParameterCollection target, params string[] columns) {
            foreach (string column in columns) {
                target.Add(new SqlParameter() {
                    SourceColumn = column,
                    ParameterName = '@' + column
                });
            }
        }

        public void Read(AbstractTransaction t, AbstractConnection c, BooksDataSet ds) {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(new QueriesBuilder().select().from(TableName).Query, c.GetConnection(),
                t.GetTransaction());

            adapter.Fill(ds, tableName());
            Console.WriteLine("\nRead " + tableName());
            //Helper.printDataSet(ds, tableName());
        }

        public void Write(AbstractTransaction t, AbstractConnection c, BooksDataSet ds) {
            SqlDataAdapter adapter = new SqlDataAdapter();

            // SELECT
            String sqlSelect = "SELECT * FROM " + tableName();
            adapter.SelectCommand =
                new SqlCommand(sqlSelect, c.GetConnection(),
                    t.GetTransaction());
            // INSERT
            String sqlInsert = new QueriesBuilder().insert(TableName, Columns).Query;
  
            adapter.InsertCommand = new SqlCommand(sqlInsert, c.GetConnection(),
                t.GetTransaction());
            bindParams(adapter.InsertCommand.Parameters, Columns);

            //UPDATE
            String sqlUpdate = new QueriesBuilder().update(TableName, Columns).where(Id).Query;
            adapter.UpdateCommand = new SqlCommand(sqlUpdate, c.GetConnection(),
                t.GetTransaction());
            bindParams(adapter.UpdateCommand.Parameters, Columns);
            bindParams(adapter.UpdateCommand.Parameters, Id);

            // DELETE
            String sqlDelete = new QueriesBuilder().delete().from(TableName).where(Id).Query;
            adapter.DeleteCommand = new SqlCommand(sqlDelete, c.GetConnection(),
                t.GetTransaction());
            bindParams(adapter.DeleteCommand.Parameters, Id);

            //Helper.console(sqlSelect, sqlInsert, sqlUpdate, sqlDelete);
            // Updating
            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(adapter);
            //adapter.AcceptChangesDuringUpdate = true;
            adapter.Update(ds, TableName);
            // TODO: ???
            //ds.AcceptChanges();

            Console.WriteLine("\nUpdate " + TableName);
            //Helper.printDataSet(ds, TableName);
        }
    }
}
