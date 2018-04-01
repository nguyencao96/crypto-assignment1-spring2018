using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;


namespace Ass1_EncryptSoftware
{
    public class Encrypt
    {
        private string fpath;
        private string key;
        private string opath;
        private int mode;
        public Encrypt()
        {
        }
        public void Init(string filepath, string key, int mode, string opath)
        {
            this.fpath = filepath;
            this.key = key;
            this.mode = mode;
            this.opath = opath;

        }
        public void EnDES()
        {
            //Prepare File
            FileStream fsInput = new FileStream(fpath, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(opath, FileMode.Create, FileAccess.Write);
            //Create Cryptor
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            switch (mode)
            {
                case 0:
                    DES.Mode = CipherMode.ECB;
                    break;
                case 1:
                    DES.Mode = CipherMode.CBC;
                    break;
                case 2:
                    DES.Mode = CipherMode.CFB;
                    break;

            }

            DES.Key = Crc64.Compute(ASCIIEncoding.ASCII.GetBytes(key));
            DES.IV = DES.Key;

            SHA1 sha1 = SHA1.Create();

            ICryptoTransform DesEncrypt = DES.CreateEncryptor();
            CryptoStream Crypto = new CryptoStream(fsEncrypted, DesEncrypt, CryptoStreamMode.Write);

            byte[] ByteArrayinput = new byte[fsInput.Length];
            fsInput.Read(ByteArrayinput, 0, ByteArrayinput.Length);
            byte[] Hashdata = sha1.ComputeHash(ByteArrayinput);

            byte[] Final = new byte[Hashdata.Length + ByteArrayinput.Length];
            
            System.Diagnostics.Debug.WriteLine("Write hashdata from " + 0 + " to " + Hashdata.Length + " out of " + Final.Length);
            System.Buffer.BlockCopy(Hashdata, 0, Final, 0, Hashdata.Length);
            System.Diagnostics.Debug.WriteLine("Write inputdata from " + Hashdata.Length + " to " + ByteArrayinput.Length + " out of " + Final.Length);
            System.Buffer.BlockCopy(ByteArrayinput, 0, Final, Hashdata.Length, ByteArrayinput.Length);
            //string mergeString = Encoding.GetEncoding(1252).GetString(Final);
            Crypto.Write(Final, 0, Final.Length);
            Crypto.Close();

            fsInput.Close();
            fsEncrypted.Close();

            StreamWriter sw = File.AppendText(this.opath);
            sw.Write("D");
            sw.Write(this.mode);
            sw.Close();
        }
        public void EnAES()
        {
            FileStream fInput = new FileStream(this.fpath, FileMode.Open, FileAccess.Read);
            FileStream fOutput = new FileStream(this.opath, FileMode.Create, FileAccess.Write);

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();


            SHA256 sha256 = SHA256.Create();
            aes.Key = sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(this.key));
            System.Buffer.BlockCopy(aes.Key, 0, aes.IV, 0, aes.Key.Length / 2);

            switch (mode)
            {
                case 0:
                    aes.Mode = CipherMode.ECB;
                    break;
                case 1:
                    aes.Mode = CipherMode.CBC;
                    break;
                case 2:
                    aes.Mode = CipherMode.CFB;
                    break;

            }

            byte[] arrayinput = new byte[fInput.Length];
            fInput.Read(arrayinput, 0, arrayinput.Length);

            SHA1 sha1 = SHA1.Create();
            byte[] Hashdata = sha1.ComputeHash(arrayinput);
            byte[] Final = new byte[Hashdata.Length + arrayinput.Length];
            System.Buffer.BlockCopy(Hashdata, 0, Final, 0, Hashdata.Length);
            System.Buffer.BlockCopy(arrayinput, 0, Final, Hashdata.Length, arrayinput.Length);

            ICryptoTransform aesEncrypt = aes.CreateEncryptor();
            CryptoStream cstream = new CryptoStream(fOutput, aesEncrypt, CryptoStreamMode.Write);

            cstream.Write(Final, 0, Final.Length);
            cstream.Close();

            fInput.Close();
            fOutput.Close();

            StreamWriter sw = File.AppendText(this.opath);
            sw.Write("A");
            sw.Write(this.mode);
            sw.Close();
        }
       
        public void EnRSA()
        {
            FileStream fInput = new FileStream(this.fpath, FileMode.Open, FileAccess.Read);
            byte[] arrayinput = new byte[fInput.Length];
            fInput.Read(arrayinput, 0, arrayinput.Length);

            SHA1 sha1 = SHA1.Create();
            byte[] Hashdata = sha1.ComputeHash(arrayinput);
            byte[] Final = new byte[Hashdata.Length + arrayinput.Length];
            System.Buffer.BlockCopy(Hashdata, 0, Final, 0, Hashdata.Length);
            System.Buffer.BlockCopy(arrayinput, 0, Final, Hashdata.Length, arrayinput.Length);


            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(File.ReadAllText(this.key));
            RSAParameters rsaparam = rsa.ExportParameters(false);
            //RSACryptoServiceProvider mylocal = new RSACryptoServiceProvider();
            //mylocal.ImportParameters(rsaparam);
            byte[] arrayenc;
            arrayenc = rsa.Encrypt(Final, false);

            FileStream fOutput = new FileStream(this.opath, FileMode.Create, FileAccess.Write);
            fOutput.Write(arrayenc, 0, arrayenc.Length);

            fInput.Close();
            fOutput.Close();

            StreamWriter sw = File.AppendText(this.opath);
            sw.Write("R");
            sw.Write(this.mode);
            sw.Close();
        }
        public void EnBlowFish()
        {
            //Read Input file
            FileStream fInput = new FileStream(this.fpath, FileMode.Open, FileAccess.Read);
            //FileStream fOutput = new FileStream(this.opath, FileMode.Create, FileAccess.Write);
            byte[] arrayinput = new byte[fInput.Length];
            fInput.Read(arrayinput, 0, arrayinput.Length);
            //Hash Plaintext
            SHA1 sha1 = SHA1.Create();
            byte[] Hashdata = sha1.ComputeHash(arrayinput);
            byte[] Final = new byte[Hashdata.Length + arrayinput.Length];
            System.Buffer.BlockCopy(Hashdata, 0, Final, 0, Hashdata.Length);
            System.Buffer.BlockCopy(arrayinput, 0, Final, Hashdata.Length, arrayinput.Length);
            //Hash key
            SHA256 sha256 = SHA256.Create();
            //            aes.Key = sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(this.key));
            //          System.Buffer.BlockCopy(aes.Key, 0, aes.IV, 0, aes.Key.Length / 2);

            BlowFish b = new BlowFish(sha256.ComputeHash(ASCIIEncoding.ASCII.GetBytes(this.key)));
            b.SetIV(Crc64.Compute(ASCIIEncoding.ASCII.GetBytes(this.key)));

            byte[] arrayenc;
            if (this.mode == 0)
                arrayenc = b.Encrypt_ECB(Final);
            else if (this.mode == 1)
                arrayenc = b.Encrypt_CBC(Final);
            else
                arrayenc = b.Encrypt_CBC(Final);

            FileStream fOutput = new FileStream(this.opath, FileMode.Create, FileAccess.Write);
            fOutput.Write(arrayenc, 0, arrayenc.Length);

            fInput.Close();
            fOutput.Close();

            StreamWriter sw = File.AppendText(this.opath);
            sw.Write("B");
            sw.Write(this.mode);
            sw.Close();
        }
        public void EnLOKI(int ciphermode) { }
    }
}
