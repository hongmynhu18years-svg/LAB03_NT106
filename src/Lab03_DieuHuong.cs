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
            this.Hide();
        }

        private void btnBai5_Click(object sender, EventArgs e)
        {
            Server serverForm = new Server();
            serverForm.StartPosition = FormStartPosition.Manual;
            serverForm.Location = new Point(50, 100);
            serverForm.Show();

            Lab03_Bai05 clientForm = new Lab03_Bai05();
            clientForm.StartPosition = FormStartPosition.Manual;
            clientForm.Location = new Point(serverForm.Right + 20, 100);
            clientForm.Show();
        }

        private void btnBai6_Click(object sender, EventArgs e)
        {
            Lab03_Bai06 Form = new Lab03_Bai06();
            Form.Show(this);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
