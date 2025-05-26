namespace ScreenSpyKiller
{
    partial class Home
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel panelProcesses;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.ListBox listBox;

        private void InitializeComponent()
        {
            this.panelProcesses = new System.Windows.Forms.FlowLayoutPanel();
            this.btnKill = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // panelProcesses
            this.panelProcesses.AutoScroll = true;
            this.panelProcesses.Location = new System.Drawing.Point(20, 20);
            this.panelProcesses.Name = "panelProcesses";
            this.panelProcesses.Size = new System.Drawing.Size(500, 250);

            // btnKill
            this.btnKill.Location = new System.Drawing.Point(20, 280);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(200, 40);
            this.btnKill.Text = "Kill and Continue";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Enabled = false;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);

            // listBox
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(20, 330);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(500, 95);

            // Form1
            this.ClientSize = new System.Drawing.Size(550, 450);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnKill);
            this.Controls.Add(this.panelProcesses);
            this.Name = "Form1";
            this.Text = "Suspicious Process Checker";
            this.ResumeLayout(false);
        }
    }
}
