using Windows.Win32.Graphics.Gdi;
using System.Configuration;

namespace RefreshRateAutoSwitcher
{
    public partial class Form1 : Form
    {

        Configuration config =
           ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public Form1()
        {
            InitializeComponent();

            Dictionary<string, string> dict = new Dictionary<string, string>();
            var dds = Program.displaySettings.Get_DISPLAY_DEVICES(false);
            var dds_adapter = Program.displaySettings.Get_DISPLAY_DEVICES(true);
            foreach (DISPLAY_DEVICEW mode in dds)
            {
                var adapterName = mode.DeviceName.ToString();
                dict.Add(adapterName, string.Format("{0}, {1}, {2}", adapterName, dds_adapter.Where(o => o.DeviceName.ToString() == adapterName.Substring(0, adapterName.IndexOf("\\Monitor"))).FirstOrDefault().DeviceString, mode.DeviceString));
            }

            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.DataSource = dict.ToList();
            try
            {
                if (config.AppSettings.Settings["freq_batt"] != null)
                {
                    freq_batt.Text = config.AppSettings.Settings["freq_batt"].Value;
                };

                if (config.AppSettings.Settings["freq_plugged"] != null)
                {
                    freq_plugged.Text = config.AppSettings.Settings["freq_plugged"].Value;
                };

                if (config.AppSettings.Settings["Monitor"] != null)
                {
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dict.Where(o => o.Key == config.AppSettings.Settings["Monitor"].Value).FirstOrDefault());
                };
            }
            catch
            {
                label4.Text = "Error looading config";
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                uint freq;
                try
                {
                    //On battery
                    if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
                    {
                        freq = Convert.ToUInt32(Program.form1.freq_batt.Text);
                    }
                    else
                    {
                        freq = Convert.ToUInt32(Program.form1.freq_plugged.Text);
                    }

                }
                catch
                {
                    throw new Exception("Refresh rate is incorrect");
                }

                try
                {
                    var adapterName = ((KeyValuePair<string, string>)Program.form1.comboBox1.SelectedItem).Key;
                    label4.Text = Program.displaySettings.apply(adapterName, freq);
                }
                catch
                {
                    throw new Exception("Monitor selection is incorrect");
                }
            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            //Save settings

            try
            {
                try
                {
                    Convert.ToUInt32(Program.form1.freq_batt.Text);
                    config.AppSettings.Settings.Remove("freq_batt");
                    config.AppSettings.Settings.Add("freq_batt", freq_batt.Text.ToString());

                    Convert.ToUInt32(Program.form1.freq_plugged.Text);
                    config.AppSettings.Settings.Remove("freq_plugged");
                    config.AppSettings.Settings.Add("freq_plugged", freq_plugged.Text.ToString());

                }
                catch
                {
                    throw new Exception("Refresh rate is incorrect");
                }
                try
                {
                    config.AppSettings.Settings.Remove("Monitor");
                    config.AppSettings.Settings.Add("Monitor", ((KeyValuePair<string, string>)Program.form1.comboBox1.SelectedItem).Key);

                }
                catch
                {
                    throw new Exception("Monitor selection is incorrect");
                }


                config.Save(ConfigurationSaveMode.Modified);
                label4.Text = "Settings saved";

            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }

        }
    }
}