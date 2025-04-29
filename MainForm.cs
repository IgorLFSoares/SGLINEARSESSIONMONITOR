using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SGLinearSessionMonitor
{
    public partial class MainForm : Form
    {
        private readonly SessionMonitor _sessionMonitor;
        private bool _isMonitoring;

        public MainForm()
        {
            InitializeComponent();
            _sessionMonitor = new SessionMonitor(5000, "sglinear", UpdateGrid); // Monitorando a cada 5 segundos
            _isMonitoring = false;
            ApplyDarkMode(); // Ativa o tema escuro
        }

        // Método para aplicar o tema escuro
        private void ApplyDarkMode()
        {
            this.BackColor = System.Drawing.Color.FromArgb(34, 34, 34); // Fundo escuro
            this.ForeColor = System.Drawing.Color.White; // Texto claro
            dataGridViewSessions.BackgroundColor = System.Drawing.Color.FromArgb(45, 45, 48);
            dataGridViewSessions.ForeColor = System.Drawing.Color.White;
            btnStartStop.BackColor = System.Drawing.Color.FromArgb(50, 50, 50);
            btnStartStop.ForeColor = System.Drawing.Color.White;
            txtLog.BackColor = System.Drawing.Color.FromArgb(45, 45, 48);
            txtLog.ForeColor = System.Drawing.Color.White;
        }

        // Atualizar o DataGrid com as sessões
        private void UpdateGrid(List<SessionInfo> sessionInfos)
        {
            dataGridViewSessions.Rows.Clear();
            foreach (var sessionInfo in sessionInfos)
            {
                dataGridViewSessions.Rows.Add(sessionInfo.Username, sessionInfo.ProcessCount);
            }
        }

        // Iniciar/Parar monitoramento
        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (_isMonitoring)
            {
                _sessionMonitor.Stop();
                lblStatus.Text = "Monitoramento: Inativo";
                btnStartStop.Text = "Iniciar Monitoramento";
                txtLog.AppendText($"[{DateTime.Now}] Monitoramento interrompido.\n");
            }
            else
            {
                _sessionMonitor.Start();
                lblStatus.Text = "Monitoramento: Ativo";
                btnStartStop.Text = "Parar Monitoramento";
                txtLog.AppendText($"[{DateTime.Now}] Monitoramento iniciado.\n");
            }
            _isMonitoring = !_isMonitoring;
        }

        // Atualizar os logs na tela
        public void UpdateLogs(string message)
        {
            txtLog.AppendText($"[{DateTime.Now}] {message}\n");
        }
    }
}
