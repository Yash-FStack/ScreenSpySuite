using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ScreenSpyKiller
{
    public partial class Form1 : Form
    {
        private List<Process> suspiciousProcesses = new();
        private string[] suspiciousKeywords = {
            "zoom", "teams", "skype", "obs", "vnc", "anydesk", "chrome",
            "mstsc", "connect", "recorder", "capture", "snipping", "sharex", "screen", "cam"
        };

        public Form1()
        {
            InitializeComponent();
            LoadSuspiciousProcesses();
        }

        private void LoadSuspiciousProcesses()
        {
            checkedListBox1.Items.Clear();
            suspiciousProcesses.Clear();

            var processGroups = Process.GetProcesses()
                .GroupBy(p => p.ProcessName.ToLower())
                .Where(g => suspiciousKeywords.Any(k => g.Key.Contains(k)) &&
                            !g.Key.Contains("screenspy") &&
                            !g.Key.Contains("visualstudio"))
                .ToList();

            foreach (var group in processGroups)
            {
                var representative = group.First();
                suspiciousProcesses.Add(representative);
                checkedListBox1.Items.Add(representative.ProcessName, false);
            }

            lblStatus.Text = suspiciousProcesses.Count > 0
                ? $"{suspiciousProcesses.Count} suspicious processes found."
                : "No suspicious processes found.";
            btnKill.Enabled = suspiciousProcesses.Count > 0;
        }

        private void btnKill_Click(object sender, EventArgs e)
        {
            foreach (var item in checkedListBox1.CheckedItems)
            {
                string nameToKill = item.ToString().ToLower();

                foreach (var proc in Process.GetProcessesByName(nameToKill))
                {
                    try
                    {
                        proc.Kill();
                    }
                    catch
                    {
                        MessageBox.Show($"Could not kill process {proc.ProcessName}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }

            MessageBox.Show("Selected processes have been terminated.");
            LoadSuspiciousProcesses();
        }
    }
}
