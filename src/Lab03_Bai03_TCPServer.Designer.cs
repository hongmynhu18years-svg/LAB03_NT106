namespace LAB03
{
    partial class Lab03_Bai03_TCPServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtPort = new TextBox();
            btnListen = new Button();
            lvServer = new ListView();
            columnHeader1 = new ColumnHeader();
            SuspendLayout();
            // 
            // txtPort
            // 
            txtPort.BackColor = SystemColors.Highlight;
            txtPort.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPort.Location = new Point(48, 42);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(100, 32);
            txtPort.TabIndex = 0;
            txtPort.Text = "8080";
            // 
            // btnListen
            // 
            btnListen.BackgroundImage = Properties.Resources._240_F_1661858452_xa7JJqJPWlDqdD5h9wjISHWQWhOpmbeI3;
            btnListen.BackgroundImageLayout = ImageLayout.Stretch;
            btnListen.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnListen.ForeColor = Color.White;
            btnListen.Location = new Point(154, 42);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(120, 32);
            btnListen.TabIndex = 1;
            btnListen.Text = "▶ Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // lvServer
            // 
            lvServer.BackColor = SystemColors.Highlight;
            lvServer.BackgroundImageTiled = true;
            lvServer.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            lvServer.Font = new Font("Times New Roman", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lvServer.Location = new Point(48, 108);
            lvServer.Name = "lvServer";
            lvServer.Size = new Size(680, 300);
            lvServer.TabIndex = 2;
            lvServer.UseCompatibleStateImageBehavior = false;
            lvServer.View = View.Details;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Messages";
            columnHeader1.Width = 400;
            // 
            // Lab03_Bai03_TCPServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.images2;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(784, 461);
            Controls.Add(lvServer);
            Controls.Add(btnListen);
            Controls.Add(txtPort);
            Name = "Lab03_Bai03_TCPServer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormTCPServer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPort;
        private Button btnListen;
        private ListView lvServer;
        private ColumnHeader columnHeader1;
    }
}