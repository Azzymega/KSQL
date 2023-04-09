using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSQL.Scripts
{
    public class SQLConsole : IReciever
    {
        private TextBox console;
        private TextBox consoleInput;
        public SQLConsole(TextBox console, TextBox consoleInput)
        {
            this.console = console;
            this.consoleInput = consoleInput;
        }
        public void SendCommand(Database dbase)
        {
            try
            {
                dbase.CommandMake(consoleInput.Text);
                AppendConsoleText(consoleInput.Text);
            }
            catch (Exception ex)
            {
                AppendConsoleText(consoleInput.Text);
                AppendConsoleText(ex.Message);
            }
        }
        public void AppendConsoleText(string text)
        {
            console.Text += text+"\r\n";
        }
        public void Recieve(string message)
        {
            AppendConsoleText(message);
        }
    }
}
