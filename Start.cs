﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyToode
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
        }

        private void buttonForm_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); 
            form1.Show(); 
            this.Hide(); 
        }

        private void buttonKassa_Click(object sender, EventArgs e)
        {
            Kassa kassa = new Kassa(); 
            kassa.Show(); 
            this.Hide(); 
        }
    }
}
