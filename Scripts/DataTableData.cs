using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

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
        public void Clear()
        {
            dataTable.Clear();
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();
        }
        public string ReturnTableName(int index)
        {
            try
            {
                return tablesName[index];
            }
            catch
            {
                MessageBox.Show(ExceptionTemplateCreator.ProduceExceptionText(EStatus.LOAD_ERROR));
                return null;
            }
        }
        public List<string> ReturnTableList()
        {
            return tablesName;
        }
    }
}
