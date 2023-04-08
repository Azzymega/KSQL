using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSQL.Scripts
{
    public class StatusBarStatusController : IReciever
    {
        private ToolStripLabel statusLabel;
        public StatusBarStatusController(ToolStripLabel statusLabel)
        {
            this.statusLabel = statusLabel;
        }
        public void Recieve(string message)
        {
            statusLabel.Text = message;
        }
    }
}
