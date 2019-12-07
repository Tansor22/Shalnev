using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class QueriesBuilder
    {
        private string query = "";
        public string Query {
            get {
                return this.query;
            }
        }
        private string joined(params string[] strings) {
            return string.Join(", ", strings);
        }
        private string[] atted(params string[] strings) {
            string[] atted = new string[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                atted[i] = '@' + strings[i];        
            }
            return atted;
        }
        private string[] equaled(params string[] strings) {
            string[] equaled = new string[strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                equaled[i] = strings[i] + "=@" + strings[i];
            }
            return equaled;
        }

        public QueriesBuilder select()
        {
            select("*");
            return this;
        }
        public QueriesBuilder select(params string[] columns)
        {
            this.query += "SELECT " + joined(columns) + " ";
            return this;
        }
        public QueriesBuilder from(string tableName) {
            this.query += "FROM " + tableName + " ";
            return this;
        }
        public QueriesBuilder insert(string tableName, params string[] columns)
        {
            this.query += "INSERT INTO " + 
                tableName + " (" + 
                joined(columns) + ") VALUES (" + 
                joined(atted(columns)) + ")";
            return this;
        }
        public QueriesBuilder update(string tableName, params string[] columns)
        {
            this.query += "UPDATE " +
                tableName + " SET " + joined(equaled(columns));
        
            return this;
        }
        public QueriesBuilder where(string column)
        {
            this.query += " WHERE " + equaled(column)[0];
            return this;
        }
        public QueriesBuilder delete()
        {
            this.query += "DELETE ";
            return this;
        }
    }
}
