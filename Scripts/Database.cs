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
        private DataReader reader;
        public DataTableData data;
        private DatabaseLoader loader;
        private List<IReciever> receivers;
        private string databaseName;
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private EStatus status;
        public string GetDatabaseName()
        {
            return databaseName;
        }
        public void RefreshTable()
        {
            command.CommandText = "SELECT * FROM " + data.ReturnTableName(0);
            command.ExecuteNonQuery();
            command.CommandText = null;
        }
        public void ExecuteCommand()
        {
            EnterCommand form = new EnterCommand();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    command.CommandText = form.ReturnText();
                    command.ExecuteNonQuery();
                    command.CommandText = null;
                }
                catch
                {
                    ChangeStatus(EStatus.COMMAND_ERROR);
                }
            }
        }
        public void SaveDatabase()
        {
            try
            {
                SQLiteConnection saveStream = new SQLiteConnection("Data Source=" + loader.Save() + "; Version=3;");
                saveStream.Open();
                connection.BackupDatabase(saveStream,"main", "main", -1, null, 0);
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
            reader = new DataReader(new SQLiteCommand("SELECT * FROM sqlite_master", connection));
            data.tablesName = reader.Read();
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
        public Database(OpenFileDialog openDialog, SaveFileDialog saveDialog)
        {
            data = new DataTableData();
            loader = new DatabaseLoader(openDialog,saveDialog);
            receivers = new List<IReciever>();
            connection = new SQLiteConnection();
            command = new SQLiteCommand();
        }
        private void DatabaseInitialize()
        {
            try
            {
                //SQLiteConnection.CreateFile(databaseName);
                connection = new SQLiteConnection("Data Source = "+databaseName+";Version=3;");
                connection.Open();
                command.Connection = connection;
                //command.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT)"; // Тест плагина
                //command.ExecuteNonQuery();
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
