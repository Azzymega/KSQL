using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace KSQL.Scripts
{
    public class DataTableData
    {
        private DataTable dataTable;
        public List<string> tablesName;
        public void SetTable(DataTable dataTable)
        {
            this.dataTable = dataTable;
        }
        public DataTableData()
        {
            dataTable = new DataTable();
            tablesName = new List<string>();
        }
        public DataTable ReturnTable()
        {
            return dataTable;
        }
        public string ReturnTableName(int index)
        {
            return tablesName[index];
        }
        public List<string> ReturnTableList()
        {
            return tablesName;
        }
    }
}
