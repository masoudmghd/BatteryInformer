using BatteryInformer.UI.Service;
using System;
using System.Windows.Forms;

namespace BatteryInformer.UI
{
    public partial class MainForm : Form
    {
        private bool isToolTipLoaded = false;
        public MainForm()
        {
            InitializeComponent();
            mainTimer.Interval = 10000;
            mainTimer.Enabled = true;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            notifyIcon.Visible = true;
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            var isCharging = BatteryService.IsCharging();
            var chargeLevel = BatteryService.GetPercentage();
            if (isCharging)
            {
                mainTimer.Stop();
                if (chargeLevel >= 80 && chargeLevel < 85)
                {
                    var splashScreen = new SplashScreen(chargeLevel, true);
                    splashScreen.ShowDialog();
                }
                else if (chargeLevel >= 85)
                {
                    var splashScreen = new SplashScreen(chargeLevel, false);
                    splashScreen.ShowDialog();
                }
                mainTimer.Start();
            }
            else
            {
                if (chargeLevel == 40 || chargeLevel == 35 || chargeLevel == 30)
                {
                    ShowToolTip("Battery Needs You...", $"\r\nYour battery level is {chargeLevel}.\r\nPlease connect your charger.", false);
                }
                else if (chargeLevel == 29)
                {
                    ShowToolTip("Battery Needs You...", $"\r\nPLEASE CONNECT YOUR CHARGER!!!", true);
                }
                else
                {
                    isToolTipLoaded = false;
                }
            }
        }

        private void ShowToolTip(string title, string text, bool isAlert)
        {
            if (!isToolTipLoaded)
            {
                notifyIcon.BalloonTipTitle = title;
                notifyIcon.BalloonTipText = text;
                notifyIcon.BalloonTipIcon = isAlert ? ToolTipIcon.Error : ToolTipIcon.Warning;
                notifyIcon.ShowBalloonTip(5000);
                isToolTipLoaded = true;
            }
        }
    }
}
