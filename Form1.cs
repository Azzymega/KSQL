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
        SQLConsole console;
        Database database;
        SQLDatabaseAdapterDataSet adapter;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            database = new Database(openFileDialog1,saveFileDialog1,treeView1);
            adapter = new SQLDatabaseAdapterDataSet(database);
            database.AppendReceiver(new StatusBarStatusController(toolStripLabel1));
            console = new SQLConsole(textBox1,textBox2);
            database.AppendReceiver(console);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            database.LoadDatabase();
            adapter.UpdateConnection();
            adapter.Convert();
            dataGridView1.DataSource = adapter.ReturnDataTable();
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            database.SaveDatabase();
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text != "Таблицы")
            {
                database.DatabaseInitialize();
                adapter.UpdateConnection(e.Node.Text);
                adapter.Convert();
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = adapter.ReturnDataTable();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            console.SendCommand(database);
            adapter.Convert();
            dataGridView1.DataSource = adapter.ReturnDataTable();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                console.SendCommand(database);
                adapter.Convert();
                dataGridView1.DataSource = adapter.ReturnDataTable();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            database.CreateDatabase();
        }
    }
}
