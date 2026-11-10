namespace LAB03
{
    partial class Lab03_Bai01_UDPServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lab03_Bai01_UDPServer));
            lblSeverPort = new Label();
            tbxServerPort = new TextBox();
            lblReceivedMessages = new Label();
            btnServerExit = new Button();
            btnListen = new Button();
            lstReceivedMessages = new ListView();
            SuspendLayout();
            // 
            // lblSeverPort
            // 
            lblSeverPort.AutoSize = true;
            lblSeverPort.BackColor = Color.Transparent;
            lblSeverPort.ForeColor = SystemColors.ControlLightLight;
            lblSeverPort.Location = new Point(78, 42);
            lblSeverPort.Name = "lblSeverPort";
            lblSeverPort.Size = new Size(29, 15);
            lblSeverPort.TabIndex = 0;
            lblSeverPort.Text = "Port";
            // 
            // tbxServerPort
            // 
            tbxServerPort.Location = new Point(113, 34);
            tbxServerPort.Name = "tbxServerPort";
            tbxServerPort.Size = new Size(271, 23);
            tbxServerPort.TabIndex = 1;
            // 
            // lblReceivedMessages
            // 
            lblReceivedMessages.AutoSize = true;
            lblReceivedMessages.BackColor = Color.Transparent;
            lblReceivedMessages.ForeColor = SystemColors.ControlLightLight;
            lblReceivedMessages.Location = new Point(78, 120);
            lblReceivedMessages.Name = "lblReceivedMessages";
            lblReceivedMessages.Size = new Size(108, 15);
            lblReceivedMessages.TabIndex = 2;
            lblReceivedMessages.Text = "Received messages";
            // 
            // btnServerExit
            // 
            btnServerExit.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnServerExit.Location = new Point(402, 397);
            btnServerExit.Name = "btnServerExit";
            btnServerExit.Size = new Size(98, 37);
            btnServerExit.TabIndex = 9;
            btnServerExit.Text = "Thoát";
            btnServerExit.UseVisualStyleBackColor = true;
            btnServerExit.Click += btnServerExit_Click;
            // 
            // btnListen
            // 
            btnListen.BackgroundImage = Properties.Resources.hud_quadros_azul_do_utilizador_interface_elementos_Projeto_moderno_tecnologia_futurista_ao_controle_painel_tela_digital_holograma_janela_jogos_cardápio_tocante_cyber_monitor_conjunto_em_Preto_fundo;
            btnListen.BackgroundImageLayout = ImageLayout.Stretch;
            btnListen.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 163);
            btnListen.ForeColor = SystemColors.ControlLightLight;
            btnListen.Location = new Point(169, 63);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(131, 45);
            btnListen.TabIndex = 10;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // lstReceivedMessages
            // 
            lstReceivedMessages.FullRowSelect = true;
            lstReceivedMessages.Location = new Point(78, 138);
            lstReceivedMessages.Name = "lstReceivedMessages";
            lstReceivedMessages.Size = new Size(306, 296);
            lstReceivedMessages.TabIndex = 11;
            lstReceivedMessages.UseCompatibleStateImageBehavior = false;
            lstReceivedMessages.View = View.Details;
            // 
            // Lab03_Bai01_UDPServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(512, 446);
            Controls.Add(lstReceivedMessages);
            Controls.Add(btnListen);
            Controls.Add(btnServerExit);
            Controls.Add(lblReceivedMessages);
            Controls.Add(tbxServerPort);
            Controls.Add(lblSeverPort);
            Name = "Lab03_Bai01_UDPServer";
            Text = "Lab03_Bai01_UDPServer";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSeverPort;
        private TextBox tbxServerPort;
        private Label lblReceivedMessages;
        private Button btnServerExit;
        private Button btnListen;
        private ListView lstReceivedMessages;
    }
}