using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace KSQL.Scripts
{
    public class DatabaseLoader
    {
        private OpenFileDialog openDialog;
        private SaveFileDialog saveDialog; 
        public DatabaseLoader(OpenFileDialog openDialog, SaveFileDialog saveDialog)
        {
            this.openDialog = openDialog;
            openDialog.Title = "Загрузка базы SQLite";
            openDialog.Filter = "Файлы баз данных SQLite (*.db)|*.db";
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
        public string Load()
        {
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                return openDialog.FileName;
            }
            return null;
        }
    }
}
