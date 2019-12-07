using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labi
{
    class Helper
    {
        public static void console(params string[] mes) {
            foreach (string s in mes) {
                if (s == null)
                {
                    System.Diagnostics.Debug.WriteLine("null");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(s);
                }
            }  
        }
        private static int spacesBetweenColumn = 10;
        public static void printDataSet(RemoteServiceReference.BooksDataSet set, string nameTable)
        {
            DataTable dataTable = set.Tables[nameTable];
            // меняем столбцы местами, ставим на первоме место id
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                if (dataTable.Columns[i].Caption.Equals("id"))
                {
                    dataTable.Columns[i].SetOrdinal(0);
                    break;
                }
            }

            List<int> sizeColumn = new List<int>();
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                sizeColumn.Add(0);
            }

            // Считаем макс. размеры строк
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataTableRow = dataTable.Rows[i];
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (sizeColumn[j] < dataTableRow[j].ToString().Length)
                        sizeColumn[j] = dataTableRow[j].ToString().Length;
                }
            }

            // печатаем разделение ==============
            int sum = 0;
            foreach (int VARIABLE in sizeColumn)
            {
                sum += VARIABLE + spacesBetweenColumn;
            }

            for (int i = 0; i < sum; i++)
            {
                System.Diagnostics.Debug.Write("=");
            }

            System.Diagnostics.Debug.WriteLine("\nnameTable = " + nameTable);

            // выводим название строк
            for (int j = 0; j < dataTable.Columns.Count; j++)
            {
                System.Diagnostics.Debug.Write(dataTable.Columns[j].Caption.PadRight((int)sizeColumn[j] + spacesBetweenColumn));
            }

            System.Diagnostics.Debug.WriteLine("");
            // выводим строки
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataTableRow = dataTable.Rows[i];
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    System.Diagnostics.Debug.Write(dataTableRow[j].ToString().PadRight((int)sizeColumn[j] + spacesBetweenColumn));
                }

                System.Diagnostics.Debug.WriteLine("");
            }

            // печатаем разделение ============== 
            for (int i = 0; i < sum; i++)
            {
                System.Diagnostics.Debug.Write("=");
            }

            System.Diagnostics.Debug.WriteLine("");
        }
    }

}

