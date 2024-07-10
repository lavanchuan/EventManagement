using EventManagement;
using EventManagement.Forms;

namespace ServerEvent
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

            //string dateTime = "2024-12-12 12:30";
            //DateTime time = FormatService.StringToDataTime(dateTime);
            //Console.WriteLine(time.ToString());
            //Console.WriteLine(FormatService.DateTimeToString(time));

            ApplicationConfiguration.Initialize();
            Application.Run(new formServer());
            //Application.Run(new InviteMeListForm());
        }
    }
}