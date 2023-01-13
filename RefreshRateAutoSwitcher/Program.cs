using Windows.Win32.Graphics.Gdi;
using static Windows.Win32.PInvoke;
using System.Runtime.InteropServices;
using RefreshRateAutoSwitcher;

namespace RefreshRateAutoSwitcher
{
    internal static class Program
    {
        public static Form1 form1;
        public static DisplaySettings displaySettings;
        public static Controller controller;
        public static readonly string autorunArg = "/autorun";
        public static readonly bool autorun = false;
        public static readonly string taskName = @"RefreshRateAutoSwitcher";
        public static readonly int powerSourceChangeEventID = 105;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            displaySettings = new DisplaySettings();
            controller = new Controller();
            if (args.Contains(autorunArg))
            {
                controller.AutoRun();
            }
            else
            {
                form1 = new Form1();
                Application.Run(form1);
            }
        }
    }
}