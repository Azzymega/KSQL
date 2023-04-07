using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using KSQL.Scripts;

namespace KSQL
{
    public partial class Form1 : Form
    {
        DataTable table;
        Database database;
        SQLDatabaseAdapterDataSet adapter;
        public Form1()
        {
            InitializeComponent();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            adapter.Convert();
            dataGridView1.DataSource = adapter.ReturnDataTable();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            database = new Database();
            database.DatabaseInitialize();
            adapter = new SQLDatabaseAdapterDataSet(database,table);
        }
    }
}
