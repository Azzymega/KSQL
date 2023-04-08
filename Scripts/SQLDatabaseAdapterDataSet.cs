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
        private SQLiteDataAdapter adapter;
        // private string query = "SELECT * FROM "; // ПРАВИТЬ!!!1 Сделать уже нормальный конструктор запросов 
        public void CommandFormLaunch()
        {
            sqlDataBase.ExecuteCommand();
            sqlDataBase.RefreshTable();
        }
        public DataTable ReturnDataTable()
        {
            return sqlDataBase.data.ReturnTable();
        }
        public SQLDatabaseAdapterDataSet(Database database)
        {
            sqlDataBase = database;
            sqlDataBase.data.SetTable(new DataTable());
        }
        public void UpdateConnection()
        {
            adapter = new SQLiteDataAdapter("SELECT * FROM " + sqlDataBase.data.ReturnTableName(0), sqlDataBase.GetConnection());
        }
        public void Convert()
        {
            try
            {
                sqlDataBase.data.ReturnTable().Clear();
                adapter.Fill(sqlDataBase.data.ReturnTable());
            }
            catch
            {
                sqlDataBase.ChangeStatus(EStatus.LOAD_ERROR);
            }
        }
    }
}
