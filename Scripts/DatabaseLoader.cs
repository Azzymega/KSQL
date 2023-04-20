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
        public DatabaseLoader(OpenFileDialog openDialog)
        {
            this.openDialog = openDialog;
            openDialog.Title = "Загрузка базы SQLite";
            openDialog.Filter = "Файлы баз данных SQLite (*.db)|*.db";
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
