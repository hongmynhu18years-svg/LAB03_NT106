using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_DieuHuong : Form
    {
        public Lab03_DieuHuong()
        {
            InitializeComponent();
        }

        private void btnBai1_Click(object sender, EventArgs e)
        {
            Lab03_Bai01 Form = new Lab03_Bai01();
            Form.Show();
        }

        private void btnBai2_Click(object sender, EventArgs e)
        {
            Lab03_Bai02 Form = new Lab03_Bai02();
            Form.Show();
        }

        private void btnBai3_Click(object sender, EventArgs e)
        {
            Lab03_Bai03 Form = new Lab03_Bai03();
            Form.Show();
        }

        private void btnBai4_Click(object sender, EventArgs e)
        {
            Lab03_Bai04 Form = new Lab03_Bai04();
            Form.Show();
        }

        private void btnBai5_Click(object sender, EventArgs e)
        {

        }

        private void btnBai6_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
