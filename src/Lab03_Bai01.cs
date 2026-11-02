namespace LAB03
{
    public partial class Lab03_Bai01 : Form
    {
        public Lab03_Bai01()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            Lab03_Bai01_UDPServer Form = new Lab03_Bai01_UDPServer();
            Form.Show();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUDPCLient_Click(object sender, EventArgs e)
        {
            Lab03_Bai01_UDPClient Form = new Lab03_Bai01_UDPClient();
            Form.Show();
        }
    }
}
