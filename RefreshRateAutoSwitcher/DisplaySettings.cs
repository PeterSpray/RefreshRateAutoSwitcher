using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Win32.Graphics.Gdi;
using static Windows.Win32.PInvoke;
using System.Runtime.InteropServices;


namespace RefreshRateAutoSwitcher
{
    internal class DisplaySettings
    {
        List<DISPLAY_DEVICEW> dds;
        List<DISPLAY_DEVICEW> dds_adapter;
        DEVMODEW GetCurrentSettings(string adapterName)
        {
            DEVMODEW mode = new DEVMODEW();
            mode.dmSize = (ushort)Marshal.SizeOf(mode);
            EnumDisplaySettings(adapterName, ENUM_DISPLAY_SETTINGS_MODE.ENUM_CURRENT_SETTINGS, ref mode);
            return mode;
        }

        public string apply(string adapterName, uint freq)
        {
            try
            {
                //Get currrent config

                adapterName = adapterName.Substring(0, adapterName.IndexOf("\\Monitor"));
                DEVMODEW mode = GetCurrentSettings(adapterName);

                mode.dmDisplayFrequency = freq;

                unsafe
                {
                    var result = ChangeDisplaySettingsEx(adapterName, mode, new Windows.Win32.Foundation.HWND(), CDS_TYPE.CDS_UPDATEREGISTRY, null);
                    Console.WriteLine(result);
                    return result.ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        public DisplaySettings()
        {
            dds = new List<DISPLAY_DEVICEW>();
            dds_adapter = new List<DISPLAY_DEVICEW>();

            var dd = new DISPLAY_DEVICEW();
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
                            dds_adapter.Add(dd);


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
        }

        public List<DISPLAY_DEVICEW> Get_DISPLAY_DEVICES(bool includeAdapter)
        {

            if (includeAdapter)
                return dds_adapter;
            else
                return dds;
        }
    }
}
