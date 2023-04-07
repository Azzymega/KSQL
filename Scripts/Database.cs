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
        private DatabaseLoader loader;
        private List<IReciever> receivers;
        private string databaseName;
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private string currentStatus;
        public string GetDatabaseName()
        {
            return databaseName;
        }
        public void LoadDatabase()
        {
            databaseName = loader.Load();
            DatabaseInitialize();
        }
        public SQLiteConnection GetConnection()
        {
            return connection;
        }
        public void AppendReceiver(IReciever reciever)
        {
            receivers.Add(reciever);
        }
        public string GetCurrentStatus()
        {
            return currentStatus;
        }
        public Database(OpenFileDialog openDialog, SaveFileDialog saveDialog)
        {
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
                ChangeStatus("База подключена");
            }
            catch
            {
                ChangeStatus("База отключена (Ошибка)");
            }
        }
        private void ChangeStatus(string status)
        {
            currentStatus=status;
            SendQuery();
        }
        public void SendQuery()
        {
            foreach (IReciever item in receivers)
            {
                item.Recieve(currentStatus);
            }
        }
    }
}
