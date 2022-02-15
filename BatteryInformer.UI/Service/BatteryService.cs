using System;
using System.Windows.Forms;

namespace BatteryInformer.UI.Service
{
    public static class BatteryService
    {
        private static readonly PowerStatus _powerStatus;
        static BatteryService()
        {
            _powerStatus = SystemInformation.PowerStatus;
        }
        public static int GetPercentage()
        {
            var remainedPercentage = _powerStatus.BatteryLifePercent;
            return Convert.ToInt32(remainedPercentage * 100);
        }
        public static bool IsCharging()
        {
            return _powerStatus.PowerLineStatus == PowerLineStatus.Online;
        }
    }
}
