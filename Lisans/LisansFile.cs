using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace Lisans
{
    public class LisansFile
    {
        public string PathRead()
        {
            string DecTopKey = ((DosyadanOku(new LisansPath().PathKey())));
            return DecTopKey;
        }

        public void PathWrite(string text)
        {
            DosyayaYaz((text));
        }

        string DosyadanOku(string Path)
        {
            StreamReader dosyaOku;
            string GelenKey, TopKey = "";
            dosyaOku = File.OpenText(Path);
            GelenKey = dosyaOku.ReadLine();

            while (GelenKey != null)
            {
                TopKey += GelenKey;
                GelenKey = dosyaOku.ReadLine();
            }
            dosyaOku.Close();
            return TopKey;
        }

        void DosyayaYaz(string text)
        {
            StreamWriter SW = new StreamWriter(new LisansPath().PathKey());
            SW.WriteLine(text);
            SW.Close();

            FileInfo fi = new FileInfo(new LisansPath().PathKey());
            fi.Attributes = FileAttributes.Hidden;
        }
    }
}
