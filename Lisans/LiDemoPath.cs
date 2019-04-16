using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;


namespace Lisans
{
    public class LiDemoPath
    {
        [DllImport("JMT.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        extern static double LiDemoPathKey();
        
        const string AesIV = @"!WPGINM^W?+78!)]";
        string AesKey;
        
        static string MD5Sifrele(string metin)
        {
            //Kullanım amacı Lisans dll de tutulan kelimeyi şifreli hale getirip başka bir şifleme algoritmasında anahtar olarak kullanma.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] btr = Encoding.UTF8.GetBytes(metin);
            btr = md5.ComputeHash(btr);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in btr)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        string GelenAesKey(string AesKey)
        {
            string str = MD5Sifrele(LiDemoPathKey().ToString()); //dll den veri çekilecek.
            return AesKey = AesKey + str.Substring(0, 16);
        }

        string Encrypt(string text)
        {
            AesKey = GelenAesKey(@"");
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AesIV);
            aes.Key = Encoding.UTF8.GetBytes(AesKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            byte[] src = Encoding.Unicode.GetBytes(text);

            using (ICryptoTransform encrypt = aes.CreateEncryptor())
            {
                byte[] dest = encrypt.TransformFinalBlock(src, 0, src.Length);
                return Convert.ToBase64String(dest);
            }
        }

        string Decrypt(string text)
        {
            AesKey = GelenAesKey(@"");
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.BlockSize = 128;
            aes.KeySize = 128;
            aes.IV = Encoding.UTF8.GetBytes(AesIV);
            aes.Key = Encoding.UTF8.GetBytes(AesKey);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            byte[] src = System.Convert.FromBase64String(text);
            using (ICryptoTransform decrypt = aes.CreateDecryptor())
            {
                byte[] dest = decrypt.TransformFinalBlock(src, 0, src.Length);
                return Encoding.Unicode.GetString(dest);
            }
        }

        string DecryptDuzenle(string text)
        {
            string Giden =text.Split('?')[5].Trim();
            return Giden;
        }

        public string PathKey()
        {
            string DecTopKey = DecryptDuzenle(Decrypt(DosyadanOku()));
            return DecTopKey;
        }

        string DosyadanOku()
        {
            StreamReader dosyaOku;
            string GelenKey,TopKey="";
            string dosya_yolu = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\LiDeP.dll";
            dosyaOku = File.OpenText(dosya_yolu);
            GelenKey = dosyaOku.ReadLine();

            while (GelenKey != null)
            {
               TopKey += GelenKey;
               GelenKey = dosyaOku.ReadLine();
            }
            dosyaOku.Close();
            return TopKey;
        } 
    }
}
