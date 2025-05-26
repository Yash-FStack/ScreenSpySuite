using System.Windows.Forms;

namespace ScreenSpyKiller
{
    partial class BrowserView
    {
        private System.ComponentModel.IContainer components = null;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private Label loadingLabel;

        private void InitializeComponent()
        {
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.loadingLabel = new System.Windows.Forms.Label();

            this.SuspendLayout();

            // webView
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(800, 450);
            this.webView.TabIndex = 0;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;

            // loadingLabel
            this.loadingLabel.AutoSize = true;
            this.loadingLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.loadingLabel.Location = new System.Drawing.Point(300, 200);
            this.loadingLabel.Name = "loadingLabel";
            this.loadingLabel.Size = new System.Drawing.Size(180, 21);
            this.loadingLabel.Text = "Loading, please wait...";
            this.loadingLabel.Visible = true;

            // BrowserForm
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.loadingLabel);
            this.Controls.Add(this.webView);
            this.Name = "BrowserForm";
            this.Text = "Safe Browser";
            this.Load += new System.EventHandler(this.BrowserForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
