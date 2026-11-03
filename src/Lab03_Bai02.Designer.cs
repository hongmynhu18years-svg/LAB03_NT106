namespace LAB03
{
    partial class Lab03_Bai02
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lab03_Bai02));
            btnListen = new Button();
            lvMessages = new ListView();
            columnHeaders1 = new ColumnHeader();
            SuspendLayout();
            // 
            // btnListen
            // 
            btnListen.BackgroundImage = (Image)resources.GetObject("btnListen.BackgroundImage");
            btnListen.BackgroundImageLayout = ImageLayout.Stretch;
            btnListen.Font = new Font("Times New Roman", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnListen.ForeColor = Color.White;
            btnListen.Location = new Point(569, 24);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(177, 73);
            btnListen.TabIndex = 1;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // lvMessages
            // 
            lvMessages.BackColor = Color.FromArgb(0, 0, 64);
            lvMessages.Columns.AddRange(new ColumnHeader[] { columnHeaders1 });
            lvMessages.ForeColor = SystemColors.MenuBar;
            lvMessages.Location = new Point(46, 127);
            lvMessages.Name = "lvMessages";
            lvMessages.Size = new Size(700, 300);
            lvMessages.TabIndex = 2;
            lvMessages.UseCompatibleStateImageBehavior = false;
            lvMessages.View = View.Details;
            // 
            // columnHeaders1
            // 
            columnHeaders1.Tag = "";
            columnHeaders1.Text = "Messages";
            columnHeaders1.Width = 400;
            // 
            // Lab03_Bai02
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources._360_F_1646585193_McFWeW87UkbYnTqm8BTkiKZAGQO0E4QL;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(784, 461);
            Controls.Add(lvMessages);
            Controls.Add(btnListen);
            Name = "Lab03_Bai02";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormTelnet";
            ResumeLayout(false);
        }

        #endregion
        private Button btnListen;
        private ListView lvMessages;
        private ColumnHeader columnHeaders1;
    }
}