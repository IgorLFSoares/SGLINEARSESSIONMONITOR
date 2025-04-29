// SGLinearSessionMonitor/Forms/SettingsForm.cs
using SGLinearSessionMonitor.Models;
using System;
using System.Windows.Forms;

namespace SGLinearSessionMonitor.Forms
{
    public partial class SettingsForm : Form
    {
        private ConfigSettings _configSettings;

        public SettingsForm()
        {
            InitializeComponent();
            _configSettings = ConfigSettings.Load();
            txtMaxInstances.Text = _configSettings.MaxInstancesPerUser.ToString();
            txtMonitorInterval.Text = _configSettings.MonitorIntervalSeconds.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _configSettings.MaxInstancesPerUser = int.Parse(txtMaxInstances.Text);
            _configSettings.MonitorIntervalSeconds = int.Parse(txtMonitorInterval.Text);

            _configSettings.Save();
            MessageBox.Show("Configurações salvas com sucesso!");
        }
    }
}
