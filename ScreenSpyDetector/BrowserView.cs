using Microsoft.Web.WebView2.WinForms;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenSpyKiller
{
    public partial class BrowserView : Form
    {
        [DllImport("user32.dll")] static extern bool ShowCursor(bool bShow);
        [DllImport("user32.dll")] static extern bool BlockInput(bool fBlockIt);

        private Process recorder;

        public BrowserView()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
        }

        private async void BrowserForm_Load(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://google.com");
            StartRecording();

            // Wait 10 seconds without blocking UI
            await Task.Delay(10000);

            // Then call close logic
            await BtnClose_Click();
        }


        private void StartRecording()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + @"\ScreenCapture.mp4";
            recorder = new Process();
            recorder.StartInfo.FileName = "C:\\Users\\Yash\\Downloads\\ffmpeg-7.1.1-full_build\\ffmpeg-7.1.1-full_build\\bin\\ffmpeg.exe";
            recorder.StartInfo.Arguments = $"-y -f gdigrab -framerate 15 -i desktop \"{path}\"";
            recorder.StartInfo.CreateNoWindow = true;
            recorder.StartInfo.UseShellExecute = false;
            recorder.Start();
        }

        private async Task BtnClose_Click()
        {
            Form savingForm = new Form()
            {
                Width = 320,
                Height = 130,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                ControlBox = false,
                TopMost = true,
                Text = "Please Wait"
            };

            Label message = new Label()
            {
                Text = "Saving video, please wait...",
                AutoSize = false,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50,
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };

            Button closeButton = new Button()
            {
                Text = "Close",
                DialogResult = DialogResult.OK,
                Width = 80,
                Height = 30,
                Top = 60,
                Left = (savingForm.ClientSize.Width - 80) / 2,
                Enabled = false
            };

            savingForm.Controls.Add(message);
            savingForm.Controls.Add(closeButton);

            savingForm.Shown += async (s2, e2) =>
            {
                if (recorder != null && !recorder.HasExited)
                {
                    recorder.Kill();
                    await Task.Run(() => recorder.WaitForExit());
                }

                message.Text = "Video saved successfully!";
                closeButton.Enabled = true;
            };

            savingForm.AcceptButton = closeButton;
            savingForm.ShowDialog();

            recorder.Kill();
            Application.Exit();
        }
      
    }
}
