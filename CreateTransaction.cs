﻿using System;
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
    public partial class CreateTransaction : Form
    {
        public CreateTransaction()
        {
            InitializeComponent();
        }
        public string ReturnText()
        {
            return textBox1.Text;
        }
    }
}
