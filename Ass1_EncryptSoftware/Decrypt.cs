using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;


namespace Ass1_EncryptSoftware
{
    class Decrypt
    {
        private string fpath;
        private string key;
        private string opath;

        public void Init(string filepath, string key, string opath)
        {
            this.fpath = filepath;
            this.key = key;
            this.opath = opath;
        }
        public void DeDES(byte[] input,string opath, string key, int mode)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Crc64.Compute(ASCIIEncoding.ASCII.GetBytes(key));
            des.IV = des.Key;
            if (mode == 48)
                des.Mode = CipherMode.ECB;
            else if (mode ==49)
                des.Mode = CipherMode.CBC;
            else
                if (mode == 50) des.Mode = CipherMode.CFB;
            
            MemoryStream fsread = new MemoryStream(input);
            ICryptoTransform DesDecrypt = des.CreateDecryptor();
            CryptoStream Crypto = new CryptoStream(fsread, DesDecrypt, CryptoStreamMode.Read);
 
            string decryptedString = new StreamReader(Crypto, Encoding.GetEncoding(1252)).ReadToEnd();
            string trimOutput = decryptedString.Remove(0, 20);
            byte[] outputBytes = Encoding.GetEncoding(1252).GetBytes(trimOutput);
            File.WriteAllBytes(opath, outputBytes);
            
        }

        public void DeRSA(byte[] input, string opath, string key, int mode)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key);


            byte[] decrypted = rsa.Decrypt(input, false);
            decrypted = decrypted.Skip(20).ToArray();
            File.WriteAllBytes(opath, decrypted);
        }

        public void DeAES(byte[] input, string opath, string key, int mode)
        {
            SHA256 sha256 = SHA256.Create();
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key));
            System.Buffer.BlockCopy(aes.Key, 0, aes.IV, 0, aes.Key.Length / 2);

            if (mode == 48)
                aes.Mode = CipherMode.ECB;
            else if (mode == 49)
                aes.Mode = CipherMode.CBC;
            else
                if (mode == 50) aes.Mode = CipherMode.CFB;

            MemoryStream fsread = new MemoryStream(input);

            ICryptoTransform AesDecrypt = aes.CreateDecryptor();
            CryptoStream Crypto = new CryptoStream(fsread, AesDecrypt, CryptoStreamMode.Read);

            string stringToWrite = new StreamReader(Crypto, Encoding.GetEncoding(1252)).ReadToEnd();
            string trimOutput = stringToWrite.Remove(0, 20);
            byte[] outputBytes = Encoding.GetEncoding(1252).GetBytes(trimOutput);
            File.WriteAllBytes(opath, outputBytes);
        }

        public void DeBlow(byte[] input, string opath, string key, int mode)
        {
            SHA256 sha256 = SHA256.Create();
            BlowFish blowFish = new BlowFish(sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(key)));
            blowFish.SetIV(Crc64.Compute(ASCIIEncoding.ASCII.GetBytes(key)));

            byte[] decrypted = { };
            string cfbDecrypted;
            if (mode == 48)
                decrypted = blowFish.Decrypt_ECB(input);
            else if (mode == 49)
                decrypted = blowFish.Decrypt_CBC(input);
            else
                if (mode == 50)
                decrypted = blowFish.Decrypt_CBC(input);

            string stringToWrite = Encoding.GetEncoding(1252).GetString(decrypted);
            string trimOutput = stringToWrite.Remove(0, 20);
            int pos = trimOutput.IndexOf('\0');
            if (pos > 0 )
                trimOutput = trimOutput.Substring(0, pos);
            byte[] outputBytes = Encoding.GetEncoding(1252).GetBytes(trimOutput);
            File.WriteAllBytes(opath, outputBytes);

        }

        public void desfile()
        {
            FileStream fInput = new FileStream(this.fpath, FileMode.Open, FileAccess.Read);
            byte[] arrayinput = new byte[fInput.Length];
            //byte[] hashmessage = new byte[20];
            byte[] encrypted = new byte[arrayinput.Length - 2];

            fInput.Read(arrayinput, 0, arrayinput.Length);
           // System.Buffer.BlockCopy(arrayinput, 0, hashmessage, 0, 20);
            System.Buffer.BlockCopy(arrayinput, 0, encrypted, 0, arrayinput.Length - 2);
            string type = arrayinput[arrayinput.Length-2].ToString();
            int mode = Int32.Parse(arrayinput[arrayinput.Length - 1].ToString());
            
            switch (Int32.Parse(type))
            {
                case 68:
                    DeDES(encrypted,this.opath, this.key, mode);
                    break;
                case 82:
                    DeRSA(encrypted, this.opath, this.key, mode);
                    break;
                case 65:
                    DeAES(encrypted, this.opath, this.key, mode);
                    break;
                case 66:
                    DeBlow(encrypted, this.opath, this.key, mode);
                    break;

            }
        }
    }
}
