using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai06 : Form
    {
        public Lab03_Bai06()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            new Lab03_Bai06_Server().Show(this);
        }

        private void btnOpenClient_Click(object sender, EventArgs e)
        {
            new Lab03_Bai06_Client().Show(this);
        }
    }
}
