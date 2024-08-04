using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Cqrs.Application.SecurityExtensions
{
    public static class SecurityExtentions
    {
        private static string password = ".web!758558";
        private static byte[] Sifrele(byte[] SifresizVeri, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms,
            alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(SifresizVeri, 0, SifresizVeri.Length);
            cs.Close();
            byte[] sifrelenmisVeri = ms.ToArray();
            return sifrelenmisVeri;
        }
        public static string TextSifrele(this string sifrelenecekMetin)
        {
            try
            {
                byte[] sifrelenecekByteDizisi = System.Text.Encoding.Unicode.GetBytes(sifrelenecekMetin);
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                byte[] SifrelenmisVeri = Sifrele(sifrelenecekByteDizisi,
                pdb.GetBytes(32), pdb.GetBytes(16));
                return Convert.ToBase64String(SifrelenmisVeri);
            }
            catch
            {
                return "";
            }
        }
        public static string TextSifreCoz(this string cozulecekMetin)
        {
            byte[] SifrelenmisByteDizisi = Convert.FromBase64String(cozulecekMetin);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password,

                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                                    0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            byte[] SifresiCozulmusVeri = SifreCoz(SifrelenmisByteDizisi,
                pdb.GetBytes(32), pdb.GetBytes(16));
            return System.Text.Encoding.Unicode.GetString(SifresiCozulmusVeri);
        }

        private static byte[] SifreCoz(byte[] SifreliVeri, byte[] Key, byte[] IV)
        {

            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(SifreliVeri, 0, SifreliVeri.Length);
            cs.Close();
            byte[] SifresiCozulmusVeri = ms.ToArray();
            return SifresiCozulmusVeri;
        }
    }
}
