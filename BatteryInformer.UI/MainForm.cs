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
            mainTimer.Interval = 60000;
            mainTimer.Enabled = true;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            notifyIcon.Visible = true;
            notifyIcon.ContextMenuStrip = contextMenuStrip1;
            menuItemHighLevel.Checked = true;
            menuItemLowLevel.Checked = false;
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            var isCharging = BatteryService.IsCharging();
            var chargeLevel = BatteryService.GetPercentage();
            if (isCharging)
            {
                mainTimer.Stop();
                if (menuItemHighLevel.Checked)
                {
                    if (chargeLevel >= 90 && chargeLevel < 95)
                    {
                        var splashScreen = new SplashScreen(chargeLevel, true);
                        splashScreen.ShowDialog();
                    }
                    else if (chargeLevel >= 95)
                    {
                        var splashScreen = new SplashScreen(chargeLevel, false);
                        splashScreen.ShowDialog();
                    }
                }
                mainTimer.Start();
            }
            else
            {
                if (menuItemLowLevel.Checked)
                {
                    if (chargeLevel == 30 || chargeLevel == 20)
                    {
                        ShowToolTip("Battery Needs You...", $"\r\nYour battery level is {chargeLevel}.\r\nPlease connect your charger.", false);
                    }
                    else if (chargeLevel == 15)
                    {
                        ShowToolTip("Battery Needs You...", $"\r\nPLEASE CONNECT YOUR CHARGER!!!", true);
                    }
                    else
                    {
                        isToolTipLoaded = false;
                    }
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

        private void menuItemLowLevel_Click(object sender, EventArgs e)
        {
            menuItemLowLevel.Checked = !menuItemLowLevel.Checked;
        }

        private void menuItemHighLevel_Click(object sender, EventArgs e)
        {
            menuItemHighLevel.Checked = !menuItemHighLevel.Checked;
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
