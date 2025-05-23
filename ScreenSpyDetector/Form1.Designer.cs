namespace ScreenSpyKiller
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button btnKill;
        private System.Windows.Forms.Label lblStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.btnKill = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(360, 184);
            this.checkedListBox1.TabIndex = 0;
            // 
            // btnKill
            // 
            this.btnKill.Location = new System.Drawing.Point(12, 232);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(360, 30);
            this.btnKill.TabIndex = 1;
            this.btnKill.Text = "Kill Selected Processes";
            this.btnKill.UseVisualStyleBackColor = true;
            this.btnKill.Click += new System.EventHandler(this.btnKill_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 200);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(360, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Scanning...";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(384, 281);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnKill);
            this.Controls.Add(this.checkedListBox1);
            this.Name = "Form1";
            this.Text = "Suspicious Process Killer";
            this.ResumeLayout(false);
        }
    }
}
