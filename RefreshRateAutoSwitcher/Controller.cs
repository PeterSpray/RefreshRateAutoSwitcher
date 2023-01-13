using Microsoft.Win32.TaskScheduler;
using System.Configuration;

namespace RefreshRateAutoSwitcher
{
    internal class Controller
    {
        readonly Configuration config =
           ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        TaskService ts = new TaskService();
        public Controller() { }
        public string AdapterName { get; private set; } = "";
        public string Freq_batt { get; private set; } = "";
        public string Freq_plugged { get; private set; } = "";

        //Return monitor, freq_batt, freq_plugged
        public void LoadSettings()
        {
            try
            {
                if (config.AppSettings.Settings["freq_batt"] != null)
                {

                    Freq_batt = config.AppSettings.Settings["freq_batt"].Value;
                };

                if (config.AppSettings.Settings["freq_plugged"] != null)
                {

                    Freq_plugged = config.AppSettings.Settings["freq_plugged"].Value;
                };

                if (config.AppSettings.Settings["Monitor"] != null)
                {
                    AdapterName = config.AppSettings.Settings["Monitor"].Value;
                };

            }
            catch
            {
                throw new Exception("Error looading config");

            }
        }
        public void SaveSettings(string monitor, string freq_batt, string freq_plugged)
        {
            try
            {
                Convert.ToUInt32(freq_batt);
                this.Freq_batt = freq_batt;
                config.AppSettings.Settings.Remove("freq_batt");
                config.AppSettings.Settings.Add("freq_batt", freq_batt.ToString());

                Convert.ToUInt32(freq_plugged);
                this.Freq_plugged = freq_plugged;
                config.AppSettings.Settings.Remove("freq_plugged");
                config.AppSettings.Settings.Add("freq_plugged", freq_plugged.ToString());

            }
            catch
            {
                throw new Exception("Refresh rate is incorrect");
            }

            try
            {
                this.AdapterName = monitor;
                config.AppSettings.Settings.Remove("Monitor");
                config.AppSettings.Settings.Add("Monitor", monitor);

            }
            catch
            {
                throw new Exception("Monitor selection is incorrect");
            }

            config.Save(ConfigurationSaveMode.Modified);
        }
        public void AutoRun()
        {
            LoadSettings();
            if (ApplySettings(AdapterName, Freq_batt, Freq_plugged) == "DISP_CHANGE_SUCCESSFUL")
            {
                Application.Exit();
            };
        }
        public string ApplySettings(string adapterName, string freq_batt, string freq_plugged)
        {
            uint freq;
            try
            {
                //On battery
                if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Offline)
                {
                    freq = Convert.ToUInt32(freq_batt);
                }
                else
                {
                    freq = Convert.ToUInt32(freq_plugged);
                }

            }
            catch
            {
                throw new Exception("Refresh rate is incorrect");
            }

            try
            {
                return Program.displaySettings.apply(adapterName, freq);
            }
            catch
            {
                throw new Exception("Monitor selection is incorrect");
            }
        }

        public bool IsTaskExist(string taskName)
        {
            try
            {
                Microsoft.Win32.TaskScheduler.Task t = TaskService.Instance.GetTask(taskName);
                return t != null;

            }
            catch (Exception)
            {
                throw new Exception("Error accessing Task Scheduler");
            }

        }

        //True = task added, false = task removed
        public bool AddRemoveTask(string taskName, int powerSourceChangeEventID)
        {
            try
            {
                //Check if task exist
                Microsoft.Win32.TaskScheduler.Task t = TaskService.Instance.GetTask(Program.taskName);
                if (t == null)
                {
                    //add
                    Program.controller.AddTask(Program.taskName, Program.powerSourceChangeEventID);
                    return true;

                }
                else
                {
                    //remove
                    Program.controller.RemoveTask(Program.taskName);
                    return false;

                };

            }
            catch (Exception)
            {
                throw;
            }
        }
        void AddTask(string taskName, int powerSourceChangeEventID)
        {

            try
            {

                TaskDefinition td = ts.NewTask();
                td.RegistrationInfo.Description = "Auto switch refresh rate when on battery/plugged in.";
                td.Triggers.Add(new EventTrigger("System", "Microsoft-Windows-Kernel-Power", powerSourceChangeEventID));
                td.Actions.Add(new ExecAction(Application.ExecutablePath, Program.autorunArg));
                td.Settings.StopIfGoingOnBatteries = false;
                td.Settings.DisallowStartIfOnBatteries = false;

                ts.RootFolder.RegisterTaskDefinition(taskName, td);


            }
            catch
            {
                throw new Exception("Error adding task");
            }

        }
        void RemoveTask(string taskName)
        {

            try
            {
                Microsoft.Win32.TaskScheduler.Task t = TaskService.Instance.GetTask(taskName);
                ts.RootFolder.DeleteFolder(t.Name);


            }
            catch
            {
                throw new Exception("Error removing task");
            }

        }
    }
}
