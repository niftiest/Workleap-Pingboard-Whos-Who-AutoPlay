using System.Diagnostics;

namespace PingboardWhosWhoAutoPlay
{
    public partial class MainForm : Form
    {
        private const string GitHubUrl = "https://github.com/niftiest/Workleap-Pingboard-Whos-Who-AutoPlay";
        private CancellationTokenSource? _cts;

        public MainForm()
        {
            InitializeComponent();
        }

        public static void Log(string message)
        {
            var form = (MainForm)Application.OpenForms[0]!;
            if (form.InvokeRequired)
            {
                form.Invoke(() => Log(message));
                return;
            }

            form.LogTextBox.Text = message + Environment.NewLine + form.LogTextBox.Text;
        }

        private void ToggleButton_Click(object sender, EventArgs e)
        {
            if (_cts != null)
                Stop();
            else
                Start();
        }

        private void Start()
        {
            SeleniumDriver.Initialize();
            _cts = new CancellationTokenSource();
            var ct = _cts.Token;

            ToggleButton.BackColor = Color.IndianRed;
            ToggleButton.Text = "STOP PLAYING";

            Task.Run(() =>
            {
                try
                {
                    WebActions.Run(ct);
                }
                catch (OperationCanceledException)
                {
                    // Expected when user clicks Stop
                }
                catch (Exception ex)
                {
                    Log($"Error: {ex.Message}");
                }
            }, ct);
        }

        private void FooterLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(GitHubUrl) { UseShellExecute = true });
        }

        private void Stop()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;

            SeleniumDriver.Dispose();

            ToggleButton.BackColor = SystemColors.Highlight;
            ToggleButton.Text = "START PLAYING";
            LogTextBox.Clear();
        }
    }
}
