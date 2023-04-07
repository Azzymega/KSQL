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
        private SaveFileDialog saveDialog; // В будущем.
        public DatabaseLoader(OpenFileDialog openDialog, SaveFileDialog saveDialog)
        {
            this.openDialog = openDialog;
            this.saveDialog = saveDialog;
        }
        public void Load()
        {
            saveDialog.ShowDialog();
        }
    }
}
