namespace LAB03
{
    partial class Server
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Server));
            start = new Button();
            Rtb_log = new RichTextBox();
            log_server = new Label();
            SuspendLayout();
            // 
            // start
            // 
            start.BackColor = Color.Transparent;
            start.FlatAppearance.BorderSize = 0;
            start.FlatStyle = FlatStyle.Flat;
            start.Font = new Font("Times New Roman", 20F);
            start.ForeColor = Color.SaddleBrown;
            start.Location = new Point(338, 97);
            start.Name = "start";
            start.Size = new Size(209, 39);
            start.TabIndex = 0;
            start.Text = "Bắt đầu Server";
            start.UseVisualStyleBackColor = false;
            start.Click += start_Click;
            // 
            // Rtb_log
            // 
            Rtb_log.Font = new Font("Times New Roman", 12F);
            Rtb_log.Location = new Point(132, 244);
            Rtb_log.Name = "Rtb_log";
            Rtb_log.ReadOnly = true;
            Rtb_log.Size = new Size(612, 148);
            Rtb_log.TabIndex = 2;
            Rtb_log.Text = "";
            Rtb_log.TextChanged += Rtb_log_TextChanged;
            // 
            // log_server
            // 
            log_server.AutoSize = true;
            log_server.BackColor = Color.Transparent;
            log_server.FlatStyle = FlatStyle.Flat;
            log_server.Font = new Font("Times New Roman", 15F, FontStyle.Bold);
            log_server.ForeColor = Color.SaddleBrown;
            log_server.Location = new Point(310, 196);
            log_server.Name = "log_server";
            log_server.Size = new Size(266, 23);
            log_server.TabIndex = 3;
            log_server.Text = "LOG HOẠT ĐỘNG SERVER";
            log_server.Click += log_server_Click;
            // 
            // Server
            // 
            AutoScaleDimensions = new SizeF(9F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(877, 442);
            Controls.Add(log_server);
            Controls.Add(Rtb_log);
            Controls.Add(start);
            Font = new Font("Times New Roman", 13F);
            Margin = new Padding(4);
            Name = "Server";
            Text = "server";
            Load += Server_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button start;
        private RichTextBox Rtb_log;
        private Label log_server;
    }
}