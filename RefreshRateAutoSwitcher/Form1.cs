using Windows.Win32.Graphics.Gdi;

namespace RefreshRateAutoSwitcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Dictionary<string, DISPLAY_DEVICEW> dict = new Dictionary<string, DISPLAY_DEVICEW>();
            var dds = Program.displaySettings.Get_DISPLAY_DEVICES(false);
            var dds_adapter = Program.displaySettings.Get_DISPLAY_DEVICES(true);
            foreach (DISPLAY_DEVICEW mode in dds)
            {
                var adapterName = mode.DeviceName.ToString();
                dict.Add(string.Format("{0}, {1}, {2}", adapterName, dds_adapter.Where(o => o.DeviceName.ToString() == adapterName.Substring(0, adapterName.IndexOf("\\Monitor"))).FirstOrDefault().DeviceString, mode.DeviceString), mode);
            }

            comboBox1.DisplayMember = "Key";
            comboBox1.ValueMember = "Value";
            comboBox1.DataSource = dict.ToList();
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
                    freq = Convert.ToUInt32(Program.form1.freq.Text);
                }
                 catch (Exception ex)
                {
                    throw new Exception("Refresh rate is incorrect");
                }

                string adapterName;
                if (Program.form1.comboBox1.SelectedItem == null)
                {
                    throw new Exception("Monitor selection is incorrect");
                }
                else
                {
                    adapterName = ((KeyValuePair<string, DISPLAY_DEVICEW>)Program.form1.comboBox1.SelectedItem).Value.DeviceName.ToString();
                }

                label4.Text = Program.displaySettings.apply(adapterName, freq);
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
    }
}