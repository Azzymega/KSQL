using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSQL.Scripts
{
    public class DatabaseSaver
    {
        private SaveFileDialog saveDialog;
        public DatabaseSaver(SaveFileDialog saveDialog)
        {
            this.saveDialog = saveDialog;
            saveDialog.AddExtension = true;
            saveDialog.DefaultExt = "db";
            saveDialog.Title = "Сохранение базы SQLite";
            saveDialog.Filter = "Файлы баз данных SQLite (*.db)|*.db";
        }
        public string Save()
        {
            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                return saveDialog.FileName;
            }
            return null;
        }
    }
}
