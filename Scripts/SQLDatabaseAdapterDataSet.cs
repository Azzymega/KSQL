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
        public SQLiteDataAdapter adapter;
        // private string query = "SELECT * FROM "; // ПРАВИТЬ!!!1 Сделать уже нормальный конструктор запросов 
        public DataTable ReturnDataTable()
        {
            return sqlDataBase.ReturnData().ReturnTable();
        }
        public SQLDatabaseAdapterDataSet(Database database)
        {
            sqlDataBase = database;
            sqlDataBase.ReturnData().SetTable(new DataTable());
        }
        public void UpdateConnection()
        {
            sqlDataBase.SetTableName(sqlDataBase.ReturnData().ReturnTableName(0));
            adapter = new SQLiteDataAdapter("SELECT * FROM " + sqlDataBase.ReturnData().ReturnTableName(0), sqlDataBase.GetConnection());
        }
        public void UpdateConnection(string baseName)
        {
            sqlDataBase.SetTableName(baseName);
            adapter = new SQLiteDataAdapter("SELECT * FROM " + baseName, sqlDataBase.GetConnection());
        }
        public void Convert()
        {
            try
            {
                sqlDataBase.ReturnData().Clear();
                adapter.Fill(sqlDataBase.ReturnData().ReturnTable());
            }
            catch
            {
                sqlDataBase.ChangeStatus(EStatus.LOAD_ERROR);
            }
        }
    }
}
