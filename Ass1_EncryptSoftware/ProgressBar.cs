using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Ass1_EncryptSoftware
{
    public partial class ProgressBar : Form
    {
        public ProgressBar()
        {
            InitializeComponent();
        }
        int ListCount;
        int Tick = 0;
        public void Init(int ListCount)
        {
            this.ListCount = ListCount;
            progressBar1.Maximum = ListCount;
            progressBar1.Minimum = 0;
        }
        public void encryptCompleted(string filepath)
        {
            textBox1.AppendText("Encrypting..." + Environment.NewLine);
            textBox1.AppendText(filepath + Environment.NewLine);
            progressBar1.Increment(1);
            this.Refresh();
            Tick++;
            label1.Text = ((float)Tick * 100/ (float)ListCount).ToString() + "%";
            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show("Encrypt all done." + Environment.NewLine + "Total: " + ListCount.ToString() + " Files");
                textBox1.AppendText("Completed");
                this.Close();
            }
        }

        public void decryptCompleted(string filepath)
        {
            textBox1.AppendText("Decrypting..." + Environment.NewLine);
            textBox1.AppendText(filepath + Environment.NewLine);   
            progressBar1.Increment(1);
            this.Refresh();
            Tick++;
            label1.Text = ((float)Tick * 100 / (float)ListCount).ToString() + "%";
            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show("Decrypt all done." + Environment.NewLine + "Total: " + ListCount.ToString() + " Files");
                textBox1.AppendText("Completed");
                this.Close();
            }
        }

        public void errorProcessing(string filepath, string err)
        {
            textBox1.AppendText("Not available" + Environment.NewLine);
            textBox1.AppendText(filepath + Environment.NewLine);
            progressBar1.Increment(1);
            this.Refresh();
            Tick++;
            label1.Text = ((float)Tick * 100 / (float)ListCount).ToString() + "%";
            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show("Error: " + err);
                this.Close();
            }
        }
        #region "MoveForm"

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion
    }
}
