using Windows.Win32.Graphics.Gdi;
using System.Configuration;
using Microsoft.Win32.TaskScheduler;

namespace RefreshRateAutoSwitcher
{
    public partial class Form1 : Form
    {

        Configuration config =
           ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        bool autorun = false;
        static string taskName = @"RefreshRateAutoSwitcher";
        static int powerSourceChangeEventID = 105;
        public Form1(bool autorun)
        {
            InitializeComponent();

            Dictionary<string, string> dict = new Dictionary<string, string>();
            var dds = Program.displaySettings.Get_DISPLAY_DEVICES(false);
            var dds_adapter = Program.displaySettings.Get_DISPLAY_DEVICES(true);
            foreach (DISPLAY_DEVICEW mode in dds)
            {
                var adapterName = mode.DeviceName.ToString();
                //Look for deviceString of the graphics card, only need deviceName of the video adapter for display settings
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

            label4.Text = "";

            if (autorun)
            {
                this.autorun = autorun;
            }

            AddTask_Update();

        }

        private void AddTask_Update()
        {
            try
            {
                using (TaskService ts = new TaskService())
                {
                    //Check if task exist
                    Microsoft.Win32.TaskScheduler.Task t = TaskService.Instance.GetTask(taskName);
                    if (t != null)
                    {
                        AddTask.Text = "Remove task from Task Scheduler";
                    }
                    else
                    {
                        AddTask.Text = "Add task to Task Scheduler";
                    }
                }
            }
            catch
            {
                label4.Text = "Error accessing Task Scheduler";
            }
        }

        //For task scheduler
        private void Form1_autorun(object sender, EventArgs e)
        {
            if (autorun)
            {
                ApplyButton_Click(null, null);
                //Auto exit if success
                if (label4.Text == "DISP_CHANGE_SUCCESSFUL")
                {
                    Application.Exit();
                };

            }
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            SaveSettings_Click(null, null);

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

        private void SaveSettings_Click(object sender, EventArgs e)
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


        private void AddTask_Click(object sender, EventArgs e)
        {

            try
            {
                using (TaskService ts = new TaskService())
                {
                    //Check if task exist
                    Microsoft.Win32.TaskScheduler.Task t = TaskService.Instance.GetTask(taskName);
                    if (t == null)
                    {
                        //add
                        try
                        {

                            TaskDefinition td = ts.NewTask();
                            td.RegistrationInfo.Description = "Auto switch refresh rate when on battery/plugged in.";
                            td.Triggers.Add(new EventTrigger("System", "Microsoft-Windows-Kernel-Power", powerSourceChangeEventID));
                            td.Actions.Add(new ExecAction(Application.ExecutablePath, Program.autorunArg));
                            td.Settings.StopIfGoingOnBatteries = false;
                            td.Settings.DisallowStartIfOnBatteries = false;

                            ts.RootFolder.RegisterTaskDefinition(taskName, td);


                            label4.Text = "Task added to task scheduler";
                            AddTask_Update();
                        }
                        catch
                        {
                            label4.Text = "Error adding task";
                        }

                    }
                    else
                    {
                        //remove
                        try
                        {
                            ts.RootFolder.DeleteFolder(t.Name);

                            label4.Text = "Task removed from task scheduler";
                            AddTask_Update();
                        }
                        catch
                        {
                            label4.Text = "Error removing task";
                        }

                    };
                }
            }
            catch
            {
                label4.Text = "Error accessing task scheduler";
            }

        }
    }
}
