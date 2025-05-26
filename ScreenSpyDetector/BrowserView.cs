using Microsoft.Web.WebView2.WinForms;
using System;
using System.Windows.Forms;

namespace ScreenSpyKiller
{
    public partial class BrowserView : Form
    {
        public BrowserView()
        {
            InitializeComponent();
            webView.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted;
            webView.NavigationStarting += WebView_NavigationStarting;
        }

        private async void BrowserForm_Load(object sender, EventArgs e)
        {
            loadingLabel.Visible = true;
            await webView.EnsureCoreWebView2Async(null);
            webView.Source = new Uri("https://www.google.com/");
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, EventArgs e)
        {
            loadingLabel.Visible = false;
        }
        private void WebView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            string allowedDomain = "https://www.google.com/"; 
            string requestedUrl = e.Uri;

            if (!requestedUrl.Equals(allowedDomain))
            {
                e.Cancel = true;
                MessageBox.Show("Navigation to this site is not allowed.", "Blocked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
