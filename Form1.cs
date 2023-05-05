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
        CreateTransaction transaction;
        CreateColumn column;
        CreateTable table;
        SQLConsole console;
        Database database;
        SQLDatabaseAdapterDataSet adapter;
        public Form1()
        {
            InitializeComponent();
        }
        public void SetDataSource(SQLDatabaseAdapterDataSet adapter)
        {
            dataGridView1.DataSource = adapter.ReturnDataTable();
        }
        public void SetDataSource()
        {
            dataGridView1.DataSource = null;
        }
        public void ReloadDataSource()
        {
            adapter.UpdateConnection(database.GetTableName());
            database.ReloadBase();
            adapter.Convert();
            SetDataSource();
            SetDataSource(adapter);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            table = new CreateTable();
            column = new CreateColumn();
            transaction = new CreateTransaction();
            database = new Database(openFileDialog1,saveFileDialog1,treeView1);
            adapter = new SQLDatabaseAdapterDataSet(database);
            console = new SQLConsole(textBox1,textBox2);
            database.AppendReceiver(new StatusBarStatusController(toolStripLabel1));
            database.AppendReceiver(console);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            database.LoadDatabase();
            adapter.UpdateConnection();
            adapter.Convert();
            SetDataSource(adapter);
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
                SetDataSource();
                SetDataSource(adapter);
            }
        }
        private void SendCommand()
        {
            console.SendCommand(database);
            adapter.Convert();
            SetDataSource(adapter);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SendCommand();
            database.ReloadBase();
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendCommand();
                database.ReloadBase();
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            database.CreateDatabase();
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            if(table.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    database.CommandMake(Qeury.FormCreateTableCommand(table));
                    ReloadDataSource();
                }
                catch
                {
                    database.ChangeStatus(EStatus.COMMAND_ERROR);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(column.ShowDialog() == DialogResult.OK) 
            {
                try
                {
                    database.CommandMake(Qeury.FormAddColumnCommand(database,column));
                    ReloadDataSource();
                }
                catch
                {
                    database.ChangeStatus(EStatus.COMMAND_ERROR);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (transaction.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    database.CommandMake(Qeury.FormAddValuesCommand(database,transaction));
                    ReloadDataSource();
                }
                catch
                {
                    database.ChangeStatus(EStatus.COMMAND_ERROR);
                }
            }
        }
    }
}
