using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

namespace Lisans
{
    public class LiDateCtrl
    {
        [DllImport("kernel32.dll")]
        public extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Milliseconds;
        }
       
        short gun = 0, ay = 0, yil = 0, saat = 0, dakika = 0, saniye = 0;

        public void NetSaati()
        {
            try
            {
                var qReader = new XmlTextReader(@"http://www.saatkac.com/xml_saat_kod.php?u=TR&lisans=ndemx09okirai");
                while (qReader.Read())
                {
                    if (qReader.NodeType == XmlNodeType.Element)
                    {
                        switch (qReader.Name)
                        {
                            case "gun":
                                gun = Convert.ToInt16(qReader.ReadString());
                                break;
                            case "yil":
                                yil = Convert.ToInt16(qReader.ReadString());
                                break;
                            case "ay":
                                ay = Convert.ToInt16(qReader.ReadString());
                                break;
                            case "saat_24lu_sistem":
                                saat =Convert.ToInt16(Convert.ToInt16(qReader.ReadString())-3);
                                break;
                            case "dakika":
                                dakika = Convert.ToInt16(qReader.ReadString());
                                break;
                            case "saniye":
                                saniye = Convert.ToInt16(qReader.ReadString());
                                break;
                        }
                    }
                }

                

                SYSTEMTIME zaman = new SYSTEMTIME();
                zaman.Day = gun;
                zaman.Month = ay;
                zaman.Year = yil;
                zaman.Hour = saat;
                zaman.Minute = dakika;
                
                SetSystemTime(ref zaman);
                
            }
            catch { }
        }
    }
}
