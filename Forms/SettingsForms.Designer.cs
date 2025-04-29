namespace SGLinearSessionMonitor.Forms
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        
        internal TextBox txtMaxInstances;
        internal TextBox txtMonitorInterval;
        internal Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtMaxInstances = new System.Windows.Forms.TextBox();
            this.txtMonitorInterval = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            // 
            // txtMaxInstances
            // 
            this.txtMaxInstances.Location = new System.Drawing.Point(120, 30);
            this.txtMaxInstances.Name = "txtMaxInstances";
            this.txtMaxInstances.Size = new System.Drawing.Size(150, 23);
            // 
            // txtMonitorInterval
            // 
            this.txtMonitorInterval.Location = new System.Drawing.Point(120, 70);
            this.txtMonitorInterval.Name = "txtMonitorInterval";
            this.txtMonitorInterval.Size = new System.Drawing.Size(150, 23);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 110);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Text = "Salvar";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 180);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMonitorInterval);
            this.Controls.Add(this.txtMaxInstances);
            this.Name = "SettingsForm";
            this.Text = "Configurações";
        }
    }
}