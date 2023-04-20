using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSQL.Scripts
{
    public static class Qeury
    {
        public static string FormCreateTableCommand(CreateTable table)
        {
            return "CREATE TABLE " + table.ReturnText() + " (ID)";
        }
        public static string FormAddColumnCommand(Database database, CreateColumn column)
        {
            return "ALTER TABLE " + database.GetTableName() + " ADD " + column.ReturnColumnName();
        }
        public static string FormAddValuesCommand(Database database, CreateTransaction transaction)
        {
            return "INSERT INTO " + database.GetDatabaseName() + " VALUES " + transaction.ReturnText();
        }
    }
}
