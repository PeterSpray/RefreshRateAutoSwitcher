using Windows.Win32.Graphics.Gdi;
using static Windows.Win32.PInvoke;
using System.Runtime.InteropServices;


namespace RefreshRateSwitcher
{
    internal static class Program
    {
        public static Form1 form1 = new Form1();

        static List<DISPLAY_DEVICEW> dds;
        static List<DISPLAY_DEVICEW> dds_adapter;
        public static DEVMODEW GetCurrentSettings(string adapterName)
        {
            DEVMODEW mode = new DEVMODEW();
            mode.dmSize = (ushort)Marshal.SizeOf(mode);
            EnumDisplaySettings(adapterName, ENUM_DISPLAY_SETTINGS_MODE.ENUM_CURRENT_SETTINGS, ref mode);
            return mode;
        }

        public static void apply()
        {
            try
            {


                uint freq = Convert.ToUInt32(form1.freq.Text);
                //On battery
                //if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
                //{
                //    freq = 60U;
                //}
                //else
                //{
                //    freq = 165U;

                //}


                //Get currrent config
                
                if (form1.comboBox1.SelectedItem == null)
                {
                    throw new Exception("Monitor selectiion is incorrect");
                }
                else
                {
                    var adapterName = ((KeyValuePair<string, DISPLAY_DEVICEW>)form1.comboBox1.SelectedItem).Value.DeviceName.ToString();
                    adapterName = adapterName.Substring(0, adapterName.IndexOf("\\Monitor"));
                    DEVMODEW mode = GetCurrentSettings(adapterName);

                    mode.dmDisplayFrequency = freq;


                    unsafe
                    {
                        var result = ChangeDisplaySettingsEx(adapterName, mode, new Windows.Win32.Foundation.HWND(), CDS_TYPE.CDS_UPDATEREGISTRY, null);
                        Console.WriteLine(result);
                        form1.label4.Text = result.ToString();
                    }
                }


            }
            catch (Exception ex)
            {
                form1.label4.Text = ex.Message;

            }

        }

        public static List<DISPLAY_DEVICEW> Get_DISPLAY_DEVICES(bool includeAdapter)
        {
            List<DISPLAY_DEVICEW> dds = new List<DISPLAY_DEVICEW>();
            DISPLAY_DEVICEW dd = new DISPLAY_DEVICEW();
            uint display = 0;
            try
            {
                EnumDisplayDevices(null, display, ref dd, 0);
                dd.cb = (uint)Marshal.SizeOf(dd);
                while (EnumDisplayDevices(null, display, ref dd, 0))
                {
                    try
                    {
                        if (dd.StateFlags != 0)
                        {
                            if (includeAdapter)
                            {
                                dds.Add(dd);
                            }


                            EnumDisplayDevices(dd.DeviceName.ToString(), 0, ref dd, 0);
                            if (dd.StateFlags != 0)
                            {
                                dds.Add(dd);
                            }
                        }
                        display++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);

                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }


            return dds;
        }

        public static void init()
        {
            Dictionary<string, DISPLAY_DEVICEW> dict = new Dictionary<string, DISPLAY_DEVICEW>();
            dds = Get_DISPLAY_DEVICES(false);
            dds_adapter = Get_DISPLAY_DEVICES(true);
            foreach (DISPLAY_DEVICEW mode in dds)
            {
                var adapterName = mode.DeviceName.ToString();
                dict.Add(string.Format("{0}, {1}, {2}", adapterName, dds_adapter.Where(o => o.DeviceName.ToString() == adapterName.Substring(0, adapterName.IndexOf("\\Monitor"))).FirstOrDefault().DeviceString, mode.DeviceString), mode);
            }

            form1.comboBox1.DisplayMember = "Key";
            form1.comboBox1.ValueMember = "Value";
            form1.comboBox1.DataSource = dict.ToList();
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            init();
            Application.Run(form1);


        }
    }
}