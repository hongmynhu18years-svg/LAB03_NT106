namespace LAB03
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Lab03_Bai04_Server.StartServer();
            Application.Run(new Lab03_DieuHuong());
        }
    }
}