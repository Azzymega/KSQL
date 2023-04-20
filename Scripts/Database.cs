using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;

namespace KSQL.Scripts
{
    public class Database : ITransmitter
    {
        private string tableName;
        private Tree tree;
        private DataReader reader;
        private DataTableData data;
        private DatabaseLoader loader;
        private DatabaseSaver saver;
        private List<IReciever> receivers;
        private string databaseName;
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private EStatus status;
        public void SetTableName(string table)
        {
            tableName = table;
        }
        public string GetTableName()
        {
            return tableName;
        }
        public string GetDatabaseName()
        {
            return databaseName;
        }
        public DataTableData ReturnData()
        {
            return data;
        }
        public void CommandMake(string com)
        {
            command.CommandText = com;
            command.ExecuteNonQuery();
            command.CommandText = null;
        }
        private SQLiteConnection ConnectionInit(string pathSource)
        {
            return new SQLiteConnection("Data Source=" + pathSource + "; Version=3;");
        }
        public void SaveDatabase()
        {
            try
            {
                SQLiteConnection saveStream = ConnectionInit(saver.Save());
                saveStream.OpenAsync().Wait();
                connection.BackupDatabase(saveStream,"main", "main", -1, null, 0);
            }
            catch 
            {
                ChangeStatus(EStatus.SAVE_ERROR);
            }
        }
        public void CreateDatabase()
        {
            try
            {
                SQLiteConnection.CreateFile(saver.Save());
            }
            catch
            {
                ChangeStatus(EStatus.SAVE_ERROR);
            }
        }
        public void LoadDatabase()
        {
            databaseName = loader.Load();
            DatabaseInitialize();
            reader = new DataReader(new SQLiteCommand("SELECT * FROM sqlite_master", connection),this);
            data.tablesName = reader.Read();
            tree.Initialize(data.tablesName);
        }
        public void ReloadBase()
        {
            DatabaseInitialize();
            reader = new DataReader(new SQLiteCommand("SELECT * FROM sqlite_master", connection), this);
            data.tablesName = reader.Read();
            tree.Initialize(data.tablesName);
        }
        public SQLiteConnection GetConnection()
        {
            return connection;
        }
        public void AppendReceiver(IReciever reciever)
        {
            receivers.Add(reciever);
        }
        public EStatus GetCurrentStatus()
        {
            return status;
        }
        public Database(OpenFileDialog openDialog, SaveFileDialog saveDialog, TreeView treeView)
        {
            tree = new Tree(treeView);
            data = new DataTableData();
            loader = new DatabaseLoader(openDialog);
            saver = new DatabaseSaver(saveDialog);
            receivers = new List<IReciever>();
            connection = new SQLiteConnection();
            command = new SQLiteCommand();
        }
        public void DatabaseInitialize()
        {
            try
            {
                connection = ConnectionInit(databaseName);
                connection.Open();
                command.Connection = connection;              
                ChangeStatus(EStatus.SUCCESS);
            }
            catch
            {
                ChangeStatus(EStatus.LOAD_ERROR);
            }
        }
        public void ChangeStatus(EStatus status)
        {
            this.status=status;
            MessageBox.Show(ExceptionTemplateCreator.ProduceExceptionText(status));
            SendQuery();
        }
        public void SendQuery()
        {
            foreach (IReciever item in receivers)
            {
                item.Recieve(ExceptionTemplateCreator.ProduceExceptionText(status));
            }
        }
    }
}
