namespace ScreenSpyKiller
{
    partial class BrowserView
    {
        private System.ComponentModel.IContainer components = null;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            webView21.Location = new System.Drawing.Point(0, 0);
            webView21.Name = "webView21";
            webView21.Size = new System.Drawing.Size(800, 450);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            //webView21.Click += this.webView21_Click;
            // 
            // BrowserView
            // 
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(webView21);
            Name = "BrowserView";
            //FormClosing += BrowserForm_FormClosing;
            Load += BrowserForm_Load;
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
        }
    }
}
