using BatteryInformer.UI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryInformer.UI
{
    static class Program
    {
        private static Timer _timer;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Monitor();
            Application.Run(new MainForm());
        }

        static void Monitor()
        {
            CreateTimer();
            StartTimer();
        }

        static void CreateTimer()
        {
            _timer = new Timer();
            _timer.Interval = 5000;
            _timer.Enabled = true;
            _timer.Tick += Timer_Tick;
        }

        static void StartTimer()
        {
            _timer.Start();
        }

        static void StopTimer()
        {
            _timer.Stop();
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            StopTimer();
            if (BatteryService.IsCharging())
            {
                var value = BatteryService.GetPercentage();
                if (value > 85 && value < 90)
                {
                    var splashScreen = new SplashScreen(value, true);
                    splashScreen.ShowDialog();
                }
                else if (value >= 90)
                {
                    var splashScreen = new SplashScreen(value, false);
                    splashScreen.ShowDialog();
                }
            }
            StartTimer();
        }
    }
}
