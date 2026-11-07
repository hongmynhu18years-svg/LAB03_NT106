using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai06_Client_Anh : Form
    {
        private PictureBox picBox;
        public Lab03_Bai06_Client_Anh(string title, byte[] imageBytes)
        {
            InitializeComponent();
            this.Text = title;
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            var picBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            try
            {
                using (var ms = new MemoryStream(imageBytes))
                {
                    picBox.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể hiển thị ảnh: " + ex.Message);
            }

            this.Controls.Add(picBox);
        }

        private void Lab03_Bai06_Client_Anh_Load(object sender, EventArgs e)
        {

        }
    }
}
