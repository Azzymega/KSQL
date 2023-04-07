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
        private const string query = "SELECT * FROM sqlite_master"; // ПРАВИТЬ!!!1
        private Database sqlDataBase;
        private DataTable compDataBase;
        private SQLiteDataAdapter adapter;
        public DataTable ReturnDataTable()
        {
            return compDataBase;
        }
        public SQLDatabaseAdapterDataSet(Database database, DataTable dataTable)
        {
            sqlDataBase = database;
            compDataBase = dataTable;
            adapter = new SQLiteDataAdapter(query,sqlDataBase.GetConnection());
        }
        public void UpdateConnection()
        {
            adapter = new SQLiteDataAdapter(query, sqlDataBase.GetConnection());
        }
        public void Convert()
        {
            adapter.Fill(compDataBase);
        }
    }
}
