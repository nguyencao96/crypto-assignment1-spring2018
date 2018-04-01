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
            //FileStream fOutput = new FileStream(this.opath, FileMode.Create, FileAccess.Write);
            MemoryStream fsread = new MemoryStream(input);

            ICryptoTransform DesDecrypt = des.CreateDecryptor();
            CryptoStream Crypto = new CryptoStream(fsread, DesDecrypt, CryptoStreamMode.Read);

            StreamWriter fsDecrypted = new StreamWriter(this.opath);
            string stringToWrite = new StreamReader(Crypto, Encoding.GetEncoding(1252)).ReadToEnd();
            string trimOutput = stringToWrite.Remove(0, 20);
            //byte[] bytes = Encoding.GetEncoding(1252).GetBytes(stringToWrite);
            fsDecrypted.Write(trimOutput);
           // fsDecrypted.Flush();
            fsDecrypted.Close();
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
            }
            
            
        }
        
    }
}
