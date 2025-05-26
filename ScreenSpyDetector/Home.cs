using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScreenSpyKiller
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            string[] suspiciousKeywords = {
                "zoom", "teams", "skype", "obs", "vnc", "anydesk", "chrome",
                "mstsc", "connect", "recorder", "capture", "snagit", "camstudio",
                "screen", "stream", "share", "virtual", "mirror", "remote", "support",
                "logmein", "teamviewer", "ultravnc", "xsplit", "screencast", "wirecast"
            };

            var uniqueNames = Process.GetProcesses()
                .Where(p =>
                {
                    try
                    {
                        string name = p.ProcessName.ToLower();
                        return suspiciousKeywords.Any(k => name.Contains(k)) &&
                               !name.Contains("screenspyguard") && !name.Contains("visualstudio");
                    }
                    catch { return false; }
                })
                .Select(p => p.ProcessName)
                .Distinct()
                .ToList();

            if (uniqueNames.Count == 0)
            {
                listBox.Items.Add("✅ No suspicious processes found.");
                btnKill.Enabled = false;
            }
            else
            {
                foreach (string name in uniqueNames)
                {
                    var item = new CheckBox
                    {
                        Text = name,
                        AutoSize = true
                    };
                    item.CheckedChanged += CheckBox_CheckedChanged;
                    panelProcesses.Controls.Add(item);
                }
            }

            UpdateButtonState();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            btnKill.Enabled = panelProcesses.Controls.OfType<CheckBox>().Any(cb => cb.Checked);
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            var toKill = panelProcesses.Controls.OfType<CheckBox>().Where(cb => cb.Checked).Select(cb => cb.Text);

            foreach (string processName in toKill)
            {
                foreach (var proc in Process.GetProcessesByName(processName))
                {
                    try { proc.Kill(); } catch { }
                }
            }

            // Proceed to WebView2 form
            BrowserView browserForm = new BrowserView();
            browserForm.Show();
            this.Hide();
        }
    }
}
