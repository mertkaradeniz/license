using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace Lisans
{
    public class LiDataCtrl
    {
        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static int LiNewKey(long gelen_pr, string gelen_key);

        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static Int64 LiShowParaMetreMAC(Int64 MAC);

        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static Int64 LiShowParaMetreVol(Int64 Vol);

        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static double LiParametreSeriN(Int64 Key);
       
        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static double LiParametreDateReplaceKey(Int64 Key);

        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static bool LiDateReg(int Key);
        
        [DllImport("kernel32.dll")]
        private static extern long GetVolumeInformation(string PathName, StringBuilder VolumeNameBuffer, UInt32 VolumeNameSize, ref UInt32 VolumeSerialNumber, ref UInt32 MaximumComponentLength, ref UInt32 FileSystemFlags, StringBuilder FileSystemNameBuffer, UInt32 FileSystemNameSize);

        string LiParametreSeriNumber, LiParametreReplaceKey;

        double GetVolumeSerial()
        {
            string strDriveLetter = "C";
            uint serNum = 0;
            uint maxCompLen = 0;
            StringBuilder VolLabel = new StringBuilder(256); // Label
            UInt32 VolFlags = new UInt32();
            StringBuilder FSName = new StringBuilder(256); // File System Name
            strDriveLetter += ":\\"; // fix up the passed-in drive letter for the API call
            long Ret = GetVolumeInformation(strDriveLetter, VolLabel, (UInt32)VolLabel.Capacity, ref serNum, ref maxCompLen, ref VolFlags, FSName, (UInt32)FSName.Capacity);
            return Convert.ToDouble(serNum);
        }

        double MAC()
        {
            NetworkInterface[] arayuz;
            PhysicalAddress mac;
            arayuz = NetworkInterface.GetAllNetworkInterfaces();
            mac = arayuz[0].GetPhysicalAddress();
            String mac2 = mac.ToString();
            mac2 = Convert.ToString(Convert.ToInt64(mac2, 16), 10);
            return Convert.ToDouble(mac2);
        }
       
        public bool IfDemoFileNull()
        {
          if(File.Exists(new LiDemoPath().PathKey())) return true; else return false;
        }

        public bool IfDateFileNull(string path)
        {
            if (File.Exists(path)) return true; else return false;
        }

        bool IfLisansFileNull()
        {
            if (File.Exists("")) return  true;else return false;
        }

        //public bool DEMOANDDATENULL() //İf Null Block
        //{
        //    if (!IfDateFileNull() & !IfDemoFileNull()) return true; else return false;
        //}

        public bool DEMOANDLISANSNULL() //İf Null Block
        {
            if (!IfLisansFileNull() & !IfDemoFileNull()) return true; else return false;
        }

        public bool LISANSNULL() //İf Null Block
        {
            if (!IfLisansFileNull()) return true; else return false;
        }

        public string PRMTRE_EDIT(string gln)
        {
            string[] tut;
            tut = gln.Split('-');
            string Submit = "";
            for (int i = 0; i < tut.Count(); i++)
            {
                Submit += tut[i].Trim();
            }
            return Submit;
        }

        double PARAMAC()
        {
             LiVariables.ParaMAC = LiShowParaMetreMAC(Convert.ToInt64(MAC()));
             return LiVariables.ParaMAC;
        }

        double PARAVOL()
        {
            LiVariables.ParaVOL = LiShowParaMetreVol(Convert.ToInt64(GetVolumeSerial()));
            return LiVariables.ParaVOL;
        }

        public void InputParametre(string text)
        {
            text = text.Split('-')[0] + text.Split('-')[1] + text.Split('-')[2] + text.Split('-')[3] + text.Split('-')[4] + text.Split('-')[5];
            LiVariables.ParaMAC =Convert.ToInt64(text.Split('M')[0]);
            LiVariables.ParaVOL = Convert.ToInt64(text.Split('M')[1]);           
        }

        public string LISHOWPARAMETRE()
        {
            PARAMAC(); PARAVOL();

            int syc = 0;
            string LiParametreKey = "";
            int STOPKEY = 30; //Warning

            for (double i = 1; i < STOPKEY; i++)
            {
                if (i % 5 == 0) LiParametreKey += "-";
                else
                { LiParametreKey += (LiVariables.ParaMAC + "M" + LiVariables.ParaVOL)[Convert.ToInt16(syc)].ToString(); syc++; }

            }   
            return LiParametreKey;
        }

        public string LIPARAMETRESERINUMBER()
        {
           
            Int64 LiParametreKeyEdit1 = Math.Abs(Convert.ToInt64(LiVariables.ParaVOL));
            try { LiParametreSeriN(LiParametreKeyEdit1); }
            catch { LISHOWPARAMETRE(); }
            return Registry.LocalMachine.OpenSubKey("Software\\LiParametre\\Parametre").GetValue("LiParametreSeriNumber").ToString();
        }

        public string LIPARAMETREDATEREPLACEKEY()
        {
            Int64 LiParametreKeyEdit2 = (Convert.ToInt64(LiVariables.ParaMAC));
            try { LiParametreDateReplaceKey(LiParametreKeyEdit2); }
            catch { LISHOWPARAMETRE(); }
            
            return Registry.LocalMachine.OpenSubKey("Software\\LiParametre\\Parametre").GetValue("LiParametreReplaceKey").ToString();
        }

        public string LIKEY()
        {
             LIPARAMETRESERINUMBER(); LIPARAMETREDATEREPLACEKEY();

             LiParametreSeriNumber = Registry.LocalMachine.OpenSubKey("Software\\LiParametre\\Parametre").GetValue("LiParametreSeriNumber").ToString();
             LiParametreReplaceKey = Registry.LocalMachine.OpenSubKey("Software\\LiParametre\\Parametre").GetValue("LiParametreReplaceKey").ToString();
            return LISHOWKEY(LiParametreSeriNumber+"J000M"+LiParametreReplaceKey);
        }

        public string LISHOWKEY(string text)
        {
            int syc = 0;
            string LiParametreKey = "";
            int STOPKEY = 30; //Warning
            string txt = text;
            for (double i = 1; i < STOPKEY; i++)
            {
                if (i % 5 == 0) LiParametreKey += "-";
                else
                { LiParametreKey += txt[Convert.ToInt16(syc)].ToString(); syc++; }

            }
            return LiParametreKey;
        }

        public void KEYSPLIT()
        {
            LIKEY();

            string LiKey = LISHOWKEY(LiParametreSeriNumber + "J000M" + LiParametreReplaceKey);
            LiVariables.LiProSeriNo = LiKey.Split('J')[0];
            LiVariables.LiDateRecKey = LiKey.Split('M')[1];
        }

        public void LiDateAdd(string text)
        {
            string LiDate = text;
            LiDateReg(Convert.ToInt16(LiDate));      
        }

        public string LICTRLAPP()
        {
            return "";
        }


    }
}
