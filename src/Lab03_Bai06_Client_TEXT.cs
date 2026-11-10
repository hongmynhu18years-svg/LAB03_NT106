using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAB03
{
    public partial class Lab03_Bai06_Client_TEXT : Form
    {
        public Lab03_Bai06_Client_TEXT(string title, string content)
        {
            InitializeComponent();
            this.Text = title;
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;

            var txtContent = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Both,
                Font = new Font("Consolas", 10F),
                Text = content
            };

            this.Controls.Add(txtContent);
        }

        private void Lab03_Bai06_Client_TEXT_Load(object sender, EventArgs e)
        {

        }
    }
}
