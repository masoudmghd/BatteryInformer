using BatteryInformer.UI.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryInformer.UI
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        public SplashScreen(int value, bool isAutoClose)
        {
            InitializeComponent();
            lblValue.Text = $"Your Battery Level is {value}. Please Disconnet your charger.";
            timer.Interval = 10000;
            timerHard.Interval = 1000;
            if (isAutoClose)
            {
                timer.Enabled = true;
            }
            else
            {
                timerHard.Enabled = true;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerHard_Tick(object sender, EventArgs e)
        {
            if (!BatteryService.IsCharging())
            {
                this.Close();
            }
        }
    }
}
