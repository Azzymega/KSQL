using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KSQL
{
    public partial class CreateColumn : Form
    {
        public CreateColumn()
        {
            InitializeComponent();
        }
        public string ReturnColumnName()
        {
            return textBox2.Text;
        }
    }
}
