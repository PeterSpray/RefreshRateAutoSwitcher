using Windows.Win32.Graphics.Gdi;

namespace RefreshRateAutoSwitcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Dictionary<string, string> dict = new Dictionary<string, string>();
            var dds = Program.displaySettings.Get_DISPLAY_DEVICES(false);
            var dds_adapter = Program.displaySettings.Get_DISPLAY_DEVICES(true);
            foreach (DISPLAY_DEVICEW mode in dds)
            {
                var adapterName = mode.DeviceName.ToString();
                //Look for deviceString of the graphics card, only need deviceName of the video adapter for display settings
                dict.Add(adapterName, string.Format("{0}, {1}", dds_adapter.Where(o => o.DeviceName.ToString() == adapterName.Substring(0, adapterName.IndexOf("\\Monitor"))).FirstOrDefault().DeviceString, mode.DeviceString));
            }

            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
            comboBox1.DataSource = dict.ToList();
            try
            {
                Program.controller.LoadSettings();

                freq_batt.SelectedIndex = ((List<uint>)freq_batt.DataSource).IndexOf(uint.Parse(Program.controller.Freq_batt));
                freq_plugged.SelectedIndex = ((List<uint>)freq_plugged.DataSource).IndexOf(uint.Parse(Program.controller.Freq_plugged));
                comboBox1.SelectedIndex = comboBox1.Items.IndexOf(dict.Where(o => o.Key == Program.controller.AdapterName).FirstOrDefault().Key);

                if (comboBox1.SelectedIndex == -1) { comboBox1.SelectedIndex = 0; };
                if (freq_batt.SelectedIndex == -1) { freq_batt.SelectedIndex = 0; };
                if (freq_plugged.SelectedIndex == -1) { freq_plugged.SelectedIndex = 0; };
            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }

            label4.Text = "";

            AddTask_Update();

        }
        private void AddTask_Update()
        {
            try
            {
                if (Program.controller.IsTaskExist(Program.taskName))
                {
                    AddTask.Text = "Remove task from Task Scheduler";
                }
                else
                {
                    AddTask.Text = "Add task to Task Scheduler";
                }

            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {

            try
            {
                var adapterName = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
                Program.controller.SaveSettings(adapterName, freq_batt.Text, freq_plugged.Text);

                label4.Text = Program.controller.ApplySettings(adapterName, freq_batt.Text, freq_plugged.Text);
            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }

        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            //Save settings
            try
            {
                var adapterName = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
                Program.controller.SaveSettings(adapterName, freq_batt.Text, freq_plugged.Text);

                label4.Text = "Settings saved";

            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }

        }


        private void AddTask_Click(object sender, EventArgs e)
        {

            try
            {
                if (Program.controller.AddRemoveTask(Program.taskName, Program.powerSourceChangeEventID))
                {
                    label4.Text = "Task added to task scheduler";
                }
                else
                {
                    label4.Text = "Task removed from task scheduler";
                }

                AddTask_Update();

            }
            catch (Exception ex)
            {
                label4.Text = ex.Message;
            }

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set the refresh rate comboboxes
            string adapterName;
            if (comboBox1.SelectedItem != null)
            {
                adapterName = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
                freq_batt.DataSource = Program.displaySettings.Get_REFRESH_RATE(adapterName).ToList();
                freq_plugged.DataSource = Program.displaySettings.Get_REFRESH_RATE(adapterName).ToList();

            }

        }
    }
}
