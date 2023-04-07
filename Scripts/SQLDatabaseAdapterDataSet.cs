using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace KSQL.Scripts
{
    public class SQLDatabaseAdapterDataSet
    {
        private Database sqlDataBase;
        private DataTable compDataBase;
        private SQLiteDataAdapter adapter;
        private string query = "SELECT * FROM "; // ПРАВИТЬ!!!1 Сделать уже нормальный конструктор запросов
        public DataTable ReturnDataTable()
        {
            return compDataBase;
        }
        public SQLDatabaseAdapterDataSet(Database database, DataTable dataTable)
        {
            sqlDataBase = database;
            compDataBase = dataTable;
        }
        public void UpdateConnection()
        {
            adapter = new SQLiteDataAdapter("SELECT * FROM " + sqlDataBase.ReturnTableName(0), sqlDataBase.GetConnection());
        }
        public void Convert()
        {
            adapter.Fill(compDataBase);
        }
    }
}
