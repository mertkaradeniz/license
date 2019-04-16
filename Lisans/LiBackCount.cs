using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Lisans
{
    public class LiBackCount
    {
        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static int BackCount();

        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static bool LiDemoFinish();

        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static bool LiFinishRead();

        double Year = 0.0, Moon = 0.0, Week = 0.0;

        public double LiDateMinutes()
        {
            try { LiVariables.LiDate = BackCount().ToString(); }
            catch { LiVariables.Error = 10;  }
            
            if (LiVariables.LiDate.Length == 1)
                LiVariables.LiDate = "00" + LiVariables.LiDate;

            if (LiVariables.LiDate.Length == 2)
                LiVariables.LiDate = "0" + LiVariables.LiDate;
          
            
            try
            {
                 Year = Convert.ToDouble(LiVariables.LiDate.ToString().Substring(2, 1)) * 365 * 24 * 60;
                 Moon = Convert.ToDouble(LiVariables.LiDate.ToString().Substring(1, 1)) * 30 * 24 * 60;
                 Week = Convert.ToDouble(LiVariables.LiDate.ToString().Substring(0, 1)) * 7 * 24 * 60;
            }
            catch { new LiDataCtrl().LiDateAdd("100"); LiDateMinutes(); }

            return Year + Moon + Week;
        }

        public bool LiDateDifference()
        {
            if (BackCountRemaining() >= LiDateMinutes())
            {
                new LiDataCtrl().LiDateAdd("0");
                LiDemoFinish(); return true;
            }
            else
            { return false; }
        }

        public bool LiDemoFinishR()
        {
            if (LiFinishRead())
            {
                return true;
            }
            else
                return false;
        }

        public double BackCountRemaining()
        {
            DateTime Start = Convert.ToDateTime(new LiDateFile().PathRead()), Stop = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            TimeSpan DayDifference =(Stop - (Start));
            double Difference = 0;
            Difference = Math.Abs(Convert.ToInt64(DayDifference.TotalDays * 24 * 60));
            return Difference;
        }
    }
}