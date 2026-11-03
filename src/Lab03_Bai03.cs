namespace LAB03
{
    public partial class Lab03_Bai03 : Form
    {
        public Lab03_Bai03()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            Lab03_Bai03_TCPServer Form = new Lab03_Bai03_TCPServer();
            Form.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUDPCLient_Click(object sender, EventArgs e)
        {
            Lab03_Bai03_TCPClient Form = new Lab03_Bai03_TCPClient();
            Form.Show();
        }
    }
}
