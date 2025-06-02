using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace ScreenSpyKiller
{
    public partial class Home : Form
    {
        private Dictionary<string, int> currentProcesses = new(); // processName => PID
        private Dictionary<string, DateTime> processStartTimes = new();
        //private List<string> suspiciousNames = new() { "obs", "zoom", "team", "anydesk", "skype", "screen", "snagit", "vnc", "xsplit", "chrome", "edge" };
        private List<string> suspiciousNames = new() { "team"};
        private System.Timers.Timer monitorTimer;
        private string logFilePath = "SuspiciousLog.txt";

        public Home()
        {
            InitializeComponent();
            StartMonitoring();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ScanProcesses();
        }

        private void StartMonitoring()
        {
            monitorTimer = new System.Timers.Timer(5000); // 5 seconds
            monitorTimer.Elapsed += MonitorTimer_Elapsed;
            monitorTimer.Start();
        }

        private void MonitorTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ScanProcesses();
        }

        private void ScanProcesses()
        {
            var allProcesses = Process.GetProcesses();
            var foundNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var proc in allProcesses)
            {
                try
                {
                    string name = proc.ProcessName.ToLower();
                    if (suspiciousNames.Any(s => name.Contains(s)) && !name.Contains("detector"))
                    {
                        AddToListIfNew(name, proc.Id);
                        foundNames.Add(name);
                    }
                }
                catch { continue; }
            }

            // Remove closed processes
            var toRemove = currentProcesses.Keys.Except(foundNames).ToList();

            this.Invoke(() =>
            {
                foreach (var name in toRemove)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (checkedListBox1.Items[i].ToString().Equals(name, StringComparison.OrdinalIgnoreCase))
                        {
                            checkedListBox1.Items.RemoveAt(i);
                            break;
                        }
                    }

                    if (processStartTimes.TryGetValue(name, out DateTime started))
                    {
                        var duration = DateTime.Now - started;
                        File.AppendAllLines(logFilePath, new[]
                        {
                            $"{DateTime.Now:G} - CLOSED  - {name} | Active Duration: {duration}"
                        });
                    }

                    currentProcesses.Remove(name);
                    processStartTimes.Remove(name);
                }
            });
        }

        private void AddToListIfNew(string procName, int pid)
        {
            if (!currentProcesses.ContainsKey(procName))
            {
                this.Invoke(() =>
                {
                    checkedListBox1.Items.Add(procName);
                    currentProcesses[procName] = pid;

                    if (!processStartTimes.ContainsKey(procName))
                        processStartTimes[procName] = DateTime.Now;

                    lblStatus.Text = $"Detected: {procName}";

                    File.AppendAllLines(logFilePath, new[]
                    {
                        $"{DateTime.Now:G} - STARTED - {procName} (PID: {pid})"
                    });
                });
            }
        }

        private async void btnKill_Click(object sender, EventArgs e)
        {
            var killedList = new List<string>();

            foreach (string selectedProc in checkedListBox1.CheckedItems.Cast<string>().ToList())
            {
                try
                {
                    int pid = currentProcesses[selectedProc];
                    Process.GetProcessById(pid)?.Kill();

                    File.AppendAllLines(logFilePath, new[]
                    {
                        $"{DateTime.Now:G} - MANUALLY KILLED - {selectedProc} (PID: {pid})"
                    });

                    lblStatus.Text = $"Killed: {selectedProc}";
                    killedList.Add(selectedProc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to kill {selectedProc}: {ex.Message}");
                }
            }

            ScanProcesses(); // Refresh

            await Task.Delay(1000); // Slight delay to ensure UI refresh

            // If all are cleared
            if (checkedListBox1.Items.Count == 0)
            {
                await NotifyServerAllKilledAsync(killedList);
            }
        }

        private async Task<string> NotifyServerAllKilledAsync(List<string> killedProcesses)
        {
            using HttpClient client = new();
            try
            {
                string json = JsonSerializer.Serialize(new
                {
                    timestamp = DateTime.Now,
                    killed = killedProcesses
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var _connectionString = "server = 23.101.140.148; user id = dnh2020; password = mcn@123; database = SecureHire; TrustServerCertificate = True;";
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var email = "yash@gmail.com";
                    var sql = "UPDATE ProctoringEvents SET ProcessesKilled = 1 WHERE Email = @Email";

                    int rowsAffected = await connection.ExecuteAsync(sql, new { Email = email });

                    return $"Rows updated: {rowsAffected}";
                }

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
