namespace LAB03
{
    partial class Lab03_Bai01_UDPClient
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
            lblIPRemoteHost = new Label();
            lblPort = new Label();
            lblMessage = new Label();
            tbxRemoteIP = new TextBox();
            tbxRemotePort = new TextBox();
            rtbMessage = new RichTextBox();
            btnClientExit = new Button();
            btnSend = new Button();
            SuspendLayout();
            // 
            // lblIPRemoteHost
            // 
            lblIPRemoteHost.AutoSize = true;
            lblIPRemoteHost.BackColor = Color.Transparent;
            lblIPRemoteHost.ForeColor = SystemColors.ControlLightLight;
            lblIPRemoteHost.Location = new Point(140, 9);
            lblIPRemoteHost.Name = "lblIPRemoteHost";
            lblIPRemoteHost.Size = new Size(87, 15);
            lblIPRemoteHost.TabIndex = 0;
            lblIPRemoteHost.Text = "IP Remote host";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.BackColor = Color.Transparent;
            lblPort.ForeColor = SystemColors.ControlLightLight;
            lblPort.Location = new Point(519, 11);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 1;
            lblPort.Text = "Port";
            // 
            // lblMessage
            // 
            lblMessage.AutoSize = true;
            lblMessage.BackColor = Color.Transparent;
            lblMessage.ForeColor = SystemColors.ControlLightLight;
            lblMessage.Location = new Point(72, 88);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(53, 15);
            lblMessage.TabIndex = 2;
            lblMessage.Text = "Message";
            // 
            // tbxRemoteIP
            // 
            tbxRemoteIP.Location = new Point(140, 29);
            tbxRemoteIP.Name = "tbxRemoteIP";
            tbxRemoteIP.Size = new Size(292, 23);
            tbxRemoteIP.TabIndex = 3;
            // 
            // tbxRemotePort
            // 
            tbxRemotePort.Location = new Point(519, 29);
            tbxRemotePort.Name = "tbxRemotePort";
            tbxRemotePort.Size = new Size(161, 23);
            tbxRemotePort.TabIndex = 4;
            // 
            // rtbMessage
            // 
            rtbMessage.Location = new Point(72, 115);
            rtbMessage.Name = "rtbMessage";
            rtbMessage.Size = new Size(608, 176);
            rtbMessage.TabIndex = 5;
            rtbMessage.Text = "";
            // 
            // btnClientExit
            // 
            btnClientExit.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnClientExit.Location = new Point(727, 327);
            btnClientExit.Name = "btnClientExit";
            btnClientExit.Size = new Size(98, 37);
            btnClientExit.TabIndex = 10;
            btnClientExit.Text = "Thoát";
            btnClientExit.UseVisualStyleBackColor = true;
            btnClientExit.Click += btnClientExit_Click;
            // 
            // btnSend
            // 
            btnSend.BackgroundImage = Properties.Resources.hud_quadros_azul_do_utilizador_interface_elementos_Projeto_moderno_tecnologia_futurista_ao_controle_painel_tela_digital_holograma_janela_jogos_cardápio_tocante_cyber_monitor_conjunto_em_Preto_fundo;
            btnSend.BackgroundImageLayout = ImageLayout.Stretch;
            btnSend.Font = new Font("Times New Roman", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 163);
            btnSend.ForeColor = SystemColors.ControlLightLight;
            btnSend.Location = new Point(690, 182);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(121, 37);
            btnSend.TabIndex = 11;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // Lab03_Bai01_UDPClient
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.download__20_;
            ClientSize = new Size(837, 376);
            Controls.Add(btnSend);
            Controls.Add(btnClientExit);
            Controls.Add(rtbMessage);
            Controls.Add(tbxRemotePort);
            Controls.Add(tbxRemoteIP);
            Controls.Add(lblMessage);
            Controls.Add(lblPort);
            Controls.Add(lblIPRemoteHost);
            Name = "Lab03_Bai01_UDPClient";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lab03_Bai01_UDPClient";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIPRemoteHost;
        private Label lblPort;
        private Label lblMessage;
        private TextBox tbxRemoteIP;
        private TextBox tbxRemotePort;
        private RichTextBox rtbMessage;
        private Button btnClientExit;
        private Button btnSend;
    }
}