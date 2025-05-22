using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ScreenSpyDetector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ListSuspiciousProcesses();
        }

        private void ListSuspiciousProcesses()
        {
            string[] suspiciousKeywords = {
    // Popular Video Conferencing & Collaboration
    "zoom", "teams", "skype", "webex", "gotomeeting", "bluejeans", "googlemeet", "jitsi",
    "lifesize", "ringcentral", "zoho", "starleaf", "ciscojabber", "slack", "discord",

    // Remote Desktop & Remote Access
    "vnc", "tightvnc", "realvnc", "ultravnc", "teamviewer", "anydesk", "splashtop", "remote",
    "mstsc", "rdesktop", "remmina", "nomachine", "logmein", "ammyy", "parallels", "go2assist",
    "pcanywhere", "showmypc", "teamviewer_desktop", "dameware", "chrome_remote_desktop",

    // Screen Recording & Capture
    "obs", "camtasia", "snagit", "bandicam", "sharex", "screenrec", "screencastify",
    "xsplit", "fbxrecord", "loosemoose", "flashback", "fraps", "screenflow", "litecam",
    "debut", "mirillisaction", "action", "screencapture", "recorder", "recording", "capture",
    "record", "screenrecorder", "videocapture", "gifcam",

    // Browsers often used for web conferencing and sharing
    "chrome", "firefox", "edge", "opera", "brave", "vivaldi", "chromium", "torbrowser",

    // Common remote or screen sharing-related terms
    "connect", "remotecontrol", "remotesupport", "share", "screenshare", "screen_share",
    "spy", "monitor", "spycam", "spyware", "keylogger", "injector", "hook", "spyhunter",
    "capturetool", "remotesession",

    // Other communication and remote access tools
    "discord", "mumble", "teamtalk", "zoomit", "gotowebinar", "webinar", "livewebcam",
    "anyviewer", "ezconnect", "bizconf", "netop", "joinme", "teamplace", "screenconnect",

    // Mobile remote & sharing apps (sometimes on PC via emulators)
    "airdroid", "scrcpy", "mobizen", "vemno", "apowermirror", "vysor",

    // Popular system utilities that could be used maliciously
    "powershell", "cmd", "terminal", "powershell_ise", "regedit", "procmon", "procmon64",
    "sysinternals", "autoruns", "processhacker", "processexplorer", "wireshark",

    // Known spyware and hacking tools (often flagged)
    "metasploit", "nmap", "cain", "hydra", "burpsuite", "sqlmap", "johntheripper",
    "hashcat", "aircrack", "ettercap", "maltego", "kali", "backtrack",

    // Virtualization or sandboxing that may hide spying apps
    "vmware", "virtualbox", "hyper-v", "qemu", "parallels", "sandboxie", "wine",

    // Miscellaneous suspicious keywords
    "decrypt", "keylogger", "packetlogger", "sniffer", "wirelesssniffer", "netcat", "nc.exe",
    "tshark", "malware", "ransomware", "trojan", "rootkit", "botnet",

    // Common remote control protocols or ports in process names
    "rdp", "vncserver", "x11vnc", "ssh", "telnet", "ftp", "sftp", "rdpclip",

    // Screen sharing or capture hardware drivers/software
    "elgato", "blackmagic", "epiphan", "avermedia", "hauppauge", "magewell",

    // Messaging apps that support screen sharing
    "whatsapp", "telegram", "signal", "facebookmessenger", "wechat", "line",

    // Streaming software
    "twitch", "xsplit", "streamlabs", "streamelements", "mixer",

    // Cloud or desktop sharing services
    "dropbox", "googledrive", "onedrive", "box", "sharepoint", "citrix", "awsclient"
};

            var sb = new StringBuilder();
            bool flag = false;
            foreach (var proc in Process.GetProcesses())
            {
                try
                {
                    string name = proc.ProcessName.ToLower();
                    if (suspiciousKeywords.Any(s => name.Contains(s)) && !proc.ProcessName.ToString().Contains("ScreenSpy"))
                    {
                        sb.AppendLine($"⚠️ {proc.ProcessName} ({proc.Id})");
                        flag = true;
                    }
                }
                catch { /* Ignore access errors */ }
            }
            if (flag == false)
            {
                sb.AppendLine("Everything looks fine, no suspecious software detected.");
            }


            textBox1.Text = sb.ToString();
        }
    }
}
