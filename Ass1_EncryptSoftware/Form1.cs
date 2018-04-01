using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Imaging;
using System.Security;
using System.Security.Cryptography;

namespace Ass1_EncryptSoftware
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //defaul page
            EncryptionPage(false);
            DecryptionPage(false);
            SteganographyPage(false);
            SettingPage(false);
            FeedBackPage(false);
            AboutUsPage(false);

        }
        #region "Encrypt"
        List<FileInfo> fList = new List<System.IO.FileInfo>();
        private void EnDirSearch(string sDir)
        {
            try
            {
                foreach (string file in Directory.GetFiles(sDir))
                {
                    fList.Add(new FileInfo(file));
                }
                foreach (string direc in Directory.GetDirectories(sDir))
                {
                    EnDirSearch(direc);
                }
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }
        private void EnInputFileButton_Click(object sender, EventArgs e)
        {
            fList.Clear();

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select File(s)";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {

                                fList.Add(new FileInfo(file));
                                //MessageBox.Show("File(s) found: " + file.ToString(), "SOURCE FILE(S)");

                            }
                        }
                        EnURLlabel1.Visible = true;
                        EnURLlabel1.Text = file.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
            }
        }
        private void EnInputFolderButton_Click(object sender, EventArgs e)
        {
            fList.Clear();
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                EnDirSearch(folderBrowserDialog1.SelectedPath);
                EnURLlabel1.Visible = true;
                EnURLlabel1.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void EnInputFileButton2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openKey = new OpenFileDialog();
            openKey.Filter = "txt files (*.txt)|*.txt";
            openKey.RestoreDirectory = true;
            openKey.Multiselect = false;
            openKey.Title = "Select Key File";
            if (openKey.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openKey.FileNames)
                    try
                    {
                        if ((myStream = openKey.OpenFile()) != null)
                        {
                            using (myStream)
                            {

                                MessageBox.Show("Key file found: " + file.ToString(), "KEY FILE");
                                EnPasstext.Text = File.ReadAllText(file.ToString(), Encoding.UTF8);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read key file from disk. Original error: " + ex.Message);
                    }
            }
        }
        private void EnEncryptButton_Click(object sender, EventArgs e)
        {
            
                string filepath;
                string outpath;
                string key = EnPasstext.Text;

                int cmode = 1;
                if (EnECBCheck.Checked == true)
                    cmode = 0;
                else if (EnCBCCheck.Checked == true)
                    cmode = 1;
                else if (EnCFBcheck.Checked == true)
                    cmode = 2;

                Encrypt EncryptAccess = new Encrypt();


                if (fList.Count != 0)
                {
                    ProgressBar bar = new ProgressBar();
                    bar.Init(fList.Count);
                    bar.Show();
                    #region DES
                    if (des == true)
                        foreach (FileInfo file in fList)
                        {
                            filepath = @file.ToString();
                            outpath = @file.ToString() + ".txt";
                            EncryptAccess.Init(filepath, key, cmode, outpath);
                            EncryptAccess.EnDES();
                            bar.encryptCompleted(filepath);
                    }
                #endregion
                #region AES
                else if (aes == true)
                        foreach (FileInfo file in fList)
                        {
                            filepath = @file.ToString();
                            outpath = @file.ToString() + ".txt";
                            EncryptAccess.Init(filepath, key, cmode, outpath);
                            EncryptAccess.EnAES();
                            bar.encryptCompleted(filepath);
                    }
                    #endregion
                    #region Blowfish
                    else if (blow == true)
                        foreach (FileInfo file in fList)
                        {
                            filepath = @file.ToString();
                            outpath = @file.ToString() + ".txt";
                            EncryptAccess.Init(filepath, key, cmode, outpath);
                            EncryptAccess.EnBlowFish();
                            bar.encryptCompleted(filepath);
                        }
                    #endregion
                    #region RSA
                    else if (rsa == true)

                        foreach (FileInfo file in fList)
                        {
                            key = label23.Text;
                            filepath = @file.ToString();
                            outpath = @file.ToString() + ".txt";
                            EncryptAccess.Init(filepath, key, cmode, outpath);
                            EncryptAccess.EnRSA();
                            bar.encryptCompleted(filepath);
                        }
                    #endregion

                    else if (loki == true) ;
                }
                else
                {
                    MessageBox.Show("Please select file or folder to encrypt!.");
                }
            
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openKey = new OpenFileDialog();
            openKey.Filter = "XML files (*.xml)|*.xml";
            openKey.RestoreDirectory = true;
            openKey.Multiselect = false;
            openKey.Title = "Select Key File";
            if (openKey.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openKey.FileNames)
                    try
                    {
                        if ((myStream = openKey.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                //encode file here
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read key file from disk. Original error: " + ex.Message);
                    }
                label23.Text = openKey.FileName.ToString();
                label23.Visible = true;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                label27.Visible = true;
                label27.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            // generacode here
        }
        #endregion

        #region "Decrypt"
        private void DecryptBtn_Click(object sender, EventArgs e)
        {

            string filepath;
            string outpath;
            string key = DePassText.Text;

            Decrypt DecryptAccess = new Decrypt();


            if (dfList.Count != 0)
            {
                ProgressBar bar = new ProgressBar();
                bar.Init(dfList.Count);
                bar.Show();
                #region DES

                foreach (FileInfo file in dfList)
                {
                    filepath = @file.ToString();
                    outpath = @file.ToString() + ".txt";
                    DecryptAccess.Init(filepath, key, outpath);
                    DecryptAccess.desfile();
                    bar.decryptCompleted(filepath);
                }
                #endregion
                /*
            #region AES
            else if (aes == true)
                foreach (FileInfo file in fList)
                {
                    filepath = @file.ToString();
                    outpath = @file.ToString() + ".txt";
                    EncryptAccess.Init(filepath, key, cmode, outpath);
                    EncryptAccess.EnAES();
                }
            #endregion
            #region Blowfish
            else if (blow == true)
                foreach (FileInfo file in fList)
                {
                    filepath = @file.ToString();
                    outpath = @file.ToString() + ".txt";
                    EncryptAccess.Init(filepath, key, cmode, outpath);
                    EncryptAccess.EnBlowFish();
                }
            #endregion
            #region RSA
            else if (rsa == true)

                foreach (FileInfo file in fList)
                {
                    key = label23.Text;
                    filepath = @file.ToString();
                    outpath = @file.ToString() + ".txt";
                    EncryptAccess.Init(filepath, key, cmode, outpath);
                    EncryptAccess.EnRSA();
                }
            #endregion

            else if (loki == true) ;*/
            }
            else
            {
                MessageBox.Show("Please select file or folder to encrypt!.");
            }
        }
        #endregion

        #region "Stegnography"
        string Text;
        private void StegHideButton1_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openKey = new OpenFileDialog();
            openKey.Filter = "txt files (*.txt)|*.txt";
            openKey.RestoreDirectory = true;
            openKey.Multiselect = false;
            openKey.Title = "Select Key File";
            if (openKey.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openKey.FileNames)
                    try
                    {
                        if ((myStream = openKey.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                Text = File.ReadAllText(file.ToString(), Encoding.UTF8);
                                MessageBox.Show("Read Input Done");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read key file from disk. Original error: " + ex.Message);
                    }

            }
        }
        private void StegHideButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open_dialog.FileName);
            }
        }
        private void StegHideButton3_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            if (Text.Equals(""))
            {
                MessageBox.Show("The text you want to hide can't be empty", "Warning");
            }
            else
            {
                bmp = Steganography.embedText(Text, bmp);
                MessageBox.Show("Your text was hidden in the image successfully!", "Done");

                SaveFileDialog save_dialog = new SaveFileDialog();
                save_dialog.Filter = "Png Image|*.png|Bitmap Image|*.bmp";

                if (save_dialog.ShowDialog() == DialogResult.OK)
                {
                    switch (save_dialog.FilterIndex)
                    {
                        case 0:
                            {
                                bmp.Save(save_dialog.FileName, ImageFormat.Png);
                            }
                            break;
                        case 1:
                            {
                                bmp.Save(save_dialog.FileName, ImageFormat.Bmp);
                            }
                            break;
                    }

                }
            }
        }
        private void StegFindButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files (*.jpeg; *.png; *.bmp)|*.jpg; *.png; *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open_dialog.FileName);
            }
        }
        private void StegFindButton2_Click(object sender, EventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image;

            string extractedText = Steganography.extractText(bmp);

            SaveFileDialog save_dialog = new SaveFileDialog();
            save_dialog.Filter = "txt files (*.txt)|*.txt";

            if (save_dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(save_dialog.FileName + ".txt", extractedText);

            }
        }
        #endregion

        #region "EffectSlide"
        //Mouse MouseClickButton
        bool en = false, de = false, op = false, ab = false, fb = false, st = false;
        bool des = false, rsa = false, blow = false, loki = false, aes = false;
        int PageActiving = 0;
        private void SelectPageEffect(int NumberpageActive)
        {
            switch (PageActiving)
            {
                case 0:
                    EncryptLabel.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 1:
                    DecryptLabel.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 2:
                    StegLabel.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 3:
                    SettingLabel.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 4:
                    FeedbackLabel.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 5:
                    AboutLabel.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 6:
                    DesText.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 7:
                    AesText.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 8:
                    BlowText.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 9:
                    RsaText.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
                case 10:
                    LokiText.ForeColor = Color.FromArgb(255, 255, 255);
                    break;
            }
            switch (NumberpageActive)
            {
                case 0:
                    EncryptLabel.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 1:
                    DecryptLabel.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 2:
                    StegLabel.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 3:
                    SettingLabel.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 4:
                    FeedbackLabel.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 5:
                    AboutLabel.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 6:
                    DesText.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 7:
                    AesText.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 8:
                    BlowText.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 9:
                    RsaText.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
                case 10:
                    LokiText.ForeColor = Color.FromArgb(173, 179, 165);
                    break;
            }
        }
        private void EncryptPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //for color select page
            en = true; des = true;
            de = st = fb = ab = op = false;
            //slide 2 color
            SelectPageEffect(6);
            PageActiving = 6;

            //select page
            EncryptionPage(true);
            DecryptionPage(false);
            SteganographyPage(false);
            SettingPage(false);
            FeedBackPage(false);
            AboutUsPage(false);

            //Check Defaul
            EnCBCCheck.Checked = true;
            EnCBCImage.Visible = true;
            EnCFBImage.Visible = EnECBImage.Visible = false;
            EnCFBcheck.Checked = EnCFBcheck.Checked = false;

            EncryptSymmetricName.Text = "Data Encryption Standard (DES)";
            ShowKey(1);

            //Silde active
            EnSlidepageActivate(true);
        }
        private void DecryptPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //for color select page
            de = true;
            ab = st = fb = op = en = false;

            SelectPageEffect(1);
            PageActiving = 1;

            //select page
            EncryptionPage(false);
            DecryptionPage(true);
            SteganographyPage(false);
            SettingPage(false);
            FeedBackPage(false);
            AboutUsPage(false);
        }
        private void StegPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //for color select page
            st = true;
            de = ab = fb = op = en = false;

            SelectPageEffect(2);
            PageActiving = 2;

            //select page
            EncryptionPage(false);
            DecryptionPage(false);
            SteganographyPage(true);
            SettingPage(false);
            FeedBackPage(false);
            AboutUsPage(false);

        }
        private void SettingPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //for color select page
            op = true;
            de = st = fb = ab = en = false;

            SelectPageEffect(3);
            PageActiving = 3;

            //select page
            EncryptionPage(false);
            DecryptionPage(false);
            SteganographyPage(false);
            SettingPage(true);
            FeedBackPage(false);
            AboutUsPage(false);

        }
        private void FeedbackPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //for color select page
            fb = true;
            de = st = ab = op = en = false;

            SelectPageEffect(4);
            PageActiving = 4;

            //select page
            EncryptionPage(false);
            DecryptionPage(false);
            SteganographyPage(false);
            SettingPage(false);
            FeedBackPage(true);
            AboutUsPage(false);

        }
        private void AboutPanel_MouseClick(object sender, MouseEventArgs e)
        {
            //for color select page
            ab = true;
            de = st = fb = op = en = false;

            SelectPageEffect(5);
            PageActiving = 5;

            //select page
            EncryptionPage(false);
            DecryptionPage(false);
            SteganographyPage(false);
            SettingPage(false);
            FeedBackPage(false);
            AboutUsPage(true);
        }

        private void ShowKey(int slide)
        {
            if (slide == 1)
            {
                EnSymmetrickeypanel.Location = new Point(12, 186);
                EnSymmetrickeypanel.Size = new Size(562, 385);
                EnSymmetrickeypanel.Visible = true;

                EnAsymmetrickeypanel.Location = new Point(510, 587);
                EnAsymmetrickeypanel.Size = new Size(10, 10);
                EnAsymmetrickeypanel.Visible = false;

            }
            else
            {
                EnSymmetrickeypanel.Location = new Point(510, 587);
                EnSymmetrickeypanel.Size = new Size(10, 10);
                EnSymmetrickeypanel.Visible = false;

                EnAsymmetrickeypanel.Location = new Point(12, 186);
                EnAsymmetrickeypanel.Size = new Size(562, 385);
                EnAsymmetrickeypanel.Visible = true;
            }
        }
        private void DESpanel_MouseClick(object sender, MouseEventArgs e)
        {
            des = true;
            rsa = blow = aes = loki = false;
            ShowKey(1);

            SelectPageEffect(6);
            PageActiving = 6;

            EncryptSymmetricName.Text = "Data Encryption Standard (DES)";

        }
        private void AESpanel_MouseClick(object sender, MouseEventArgs e)
        {
            aes = true;
            rsa = blow = loki = des = false;
            ShowKey(1);

            SelectPageEffect(7);
            PageActiving = 7;

            EncryptSymmetricName.Text = "Advanced Encryption Standard (AES)";

        }
        private void Blowpanel_MouseClick(object sender, MouseEventArgs e)
        {
            blow = true;
            des = rsa = aes = loki = false;
            ShowKey(1);

            SelectPageEffect(8);
            PageActiving = 8;

            EncryptSymmetricName.Text = "Blowfist";
        }
        private void RSApanel_MouseClick(object sender, MouseEventArgs e)
        {
            rsa = true;
            des = blow = aes = loki = false;
            ShowKey(0);

            SelectPageEffect(9);
            PageActiving = 9;

            EncryptSymmetricName.Text = "RSA";
        }
        private void LOKIpanel_MouseClick(object sender, MouseEventArgs e)
        {
            loki = true;
            des = rsa = aes = blow = false;
            ShowKey(0);

            SelectPageEffect(10);
            PageActiving = 10;

            EncryptSymmetricName.Text = "Elliptic Curve Diffie-Hellman (ECDH)";
        }
        private void Slidebar1button_Click(object sender, EventArgs e)
        {
            SelectPageEffect(0);
            PageActiving = 0;

            EnSlidepageActivate(false);
        }

        //Active Page
        private void EncryptionPage(bool visible)
        {
            if (!visible)
            {
                //set location
                EncryptSymmetricPage.Location = new Point(800, 300);
                EncryptSymmetricPage.Size = new Size(10, 10);
            }
            else
            {
                //set location
                EncryptSymmetricPage.Location = new Point(199, 28);
                EncryptSymmetricPage.Size = new Size(600, 636);
            }

        }
        private void DecryptionPage(bool visible)
        {
            if (!visible)
            {
                //set location
                DecryptPage.Location = new Point(800, 300);
                DecryptPage.Size = new Size(10, 10);
            }
            else
            {
                //set location
                DecryptPage.Location = new Point(199, 28);
                DecryptPage.Size = new Size(600, 636);
            }
        }
        private void SteganographyPage(bool visible)
        {
            if (!visible)
            {
                //set location
                StegPage.Location = new Point(800, 300);
                StegPage.Size = new Size(10, 10);
            }
            else
            {
                //set location
                StegPage.Location = new Point(199, 28);
                StegPage.Size = new Size(600, 636);
            }
        }
        private void SettingPage(bool visible)
        {

        }
        private void AboutUsPage(bool visible)
        {
            if (!visible)
            {
                //set location
                AboutPage.Location = new Point(800, 300);
                AboutPage.Size = new Size(10, 10);
            }
            else
            {
                //set location
                AboutPage.Location = new Point(199, 28);
                AboutPage.Size = new Size(600, 636);
            }
        }
        private void FeedBackPage(bool visible)
        {
            if (!visible)
            {
                //set location
                FeedbackPage.Location = new Point(800, 300);
                FeedbackPage.Size = new Size(10, 10);
            }
            else
            {
                //set location
                FeedbackPage.Location = new Point(199, 28);
                FeedbackPage.Size = new Size(600, 636);
            }
            FBemailBox.Text = "Please enter your email";
            FBemailBox.ForeColor = System.Drawing.SystemColors.ScrollBar;
        }
        //Control option      
        private void EnECBCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (EnECBCheck.Checked == true)
            {
                EnECBImage.Visible = true;
                EnCBCImage.Visible = EnCFBImage.Visible = false;
                EnCBCCheck.Checked = EnCFBcheck.Checked = false;
            }
        }
        private void EnCBCCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (EnCBCCheck.Checked == true)
            {
                EnCBCImage.Visible = true;
                EnCFBImage.Visible = EnECBImage.Visible = false;
                EnECBCheck.Checked = EnCFBcheck.Checked = false;
            }
        }
        private void EnCFBcheck_CheckedChanged(object sender, EventArgs e)
        {
            if (EnCFBcheck.Checked == true)
            {
                EnCFBImage.Visible = true;
                EnECBImage.Visible = EnCBCImage.Visible = false;
                EnECBCheck.Checked = EnCBCCheck.Checked = false;
            }
        }
        private void EnShowPassCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (EnShowPassCheck.CheckState == CheckState.Checked)
            {
                EnPasstext.PasswordChar = '\0';
            }
            else
            {
                EnPasstext.PasswordChar = '*';
            }
        }
        private void DeShowPassCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (DeShowPassCheck.CheckState == CheckState.Checked)
            {
                DePassText.PasswordChar = '\0';
            }
            else
            {
                DePassText.PasswordChar = '*';
            }
        }
        private void EnSlidepageActivate(bool active)
        {
            //Silde Effect
            if (!active)
            {
                for (int i = 0; i >= -200; i--)
                {
                    c.Location = new Point(i, 150);
                }
            }
            else
            {
                c.Location = new Point(-200, 150);
                for (int i = -200; i <= 0; i++)
                {
                    c.Location = new Point(i, 150);
                }
            }
        }
        private void EnlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StegoInfo Newpage = new StegoInfo();
            Newpage.Show();
        }
        private void FBemailBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (FBemailBox.Text == "Please enter your email")
            {
                FBemailBox.Text = "";
                FBemailBox.ForeColor = Color.Black;
            }
        }
        private void StegInfoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StegoInfo ShowInfo = new StegoInfo();
            ShowInfo.Show();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                panel2.Enabled = true;
            }
            else
            {
                panel2.Enabled = false;
            }
        }

        //Slide 1       
        private void EncryptPanel_MouseEnter(object sender, EventArgs e)
        {
            EncryptLabel.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void EncryptPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!en) EncryptLabel.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void DecryptPanel_MouseEnter(object sender, EventArgs e)
        {
            DecryptLabel.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void DecryptPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!de) DecryptLabel.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void StegPanel_MouseEnter(object sender, EventArgs e)
        {
            StegLabel.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void StegPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!st) StegLabel.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void SettingPanel_MouseEnter(object sender, EventArgs e)
        {
            SettingLabel.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void SettingPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!op) SettingLabel.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void FeedbackPanel_MouseEnter(object sender, EventArgs e)
        {
            FeedbackLabel.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void FeedbackPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!fb) FeedbackLabel.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void AboutPanel_MouseEnter(object sender, EventArgs e)
        {
            AboutLabel.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void AboutPanel_MouseLeave(object sender, EventArgs e)
        {
            if (!ab) AboutLabel.ForeColor = Color.FromArgb(255, 255, 255);
        }
        //Slide 2       
        private void DESpanel_MouseEnter(object sender, EventArgs e)
        {
            DesText.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void DESpanel_MouseLeave(object sender, EventArgs e)
        {
            if (!des) DesText.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void AESpanel_MouseEnter(object sender, EventArgs e)
        {
            AesText.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void AESpanel_MouseLeave(object sender, EventArgs e)
        {
            if (!aes) AesText.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void RSApanel_MouseEnter(object sender, EventArgs e)
        {
            RsaText.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void RSApanel_MouseLeave(object sender, EventArgs e)
        {
            if (!rsa) RsaText.ForeColor = Color.FromArgb(255, 255, 255);
        }
        List<FileInfo> dfList = new List<System.IO.FileInfo>();
        private void DeDirSearch(string sDir)
        {
            try
            {
                foreach (string file in Directory.GetFiles(sDir))
                {
                    dfList.Add(new FileInfo(file));
                }
                foreach (string direc in Directory.GetDirectories(sDir))
                {
                    DeDirSearch(direc);
                }
            }
            catch (System.Exception excpt)
            {
                MessageBox.Show(excpt.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dfList.Clear();
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                DeDirSearch(folderBrowserDialog1.SelectedPath);
                EnURLlabel1.Visible = true;
                EnURLlabel1.Text = folderBrowserDialog1.SelectedPath.ToString();
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dfList.Clear();

            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select File(s)";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            using (myStream)
                            {

                                dfList.Add(new FileInfo(file));
                            }
                        }
                        label9.Visible = true;
                        label9.Text = file.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                    }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openKey = new OpenFileDialog();
            openKey.Filter = "files (*.txt;*.xml)|*.txt;*.xml";
            openKey.RestoreDirectory = true;
            openKey.Multiselect = false;
            openKey.Title = "Select Key File";
            if (openKey.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (String file in openKey.FileNames)
                    try
                    {
                        if ((myStream = openKey.OpenFile()) != null)
                        {
                            using (myStream)
                            {
                                DePassText.Text = File.ReadAllText(file.ToString(), Encoding.UTF8);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read key file from disk. Original error: " + ex.Message);
                    }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void GenKeyBtn_Click(object sender, EventArgs e)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            string privatekey = rsa.ToXmlString(true);
            string publickey = rsa.ToXmlString(false);
            File.WriteAllText(label27.Text + @"\PrivateKey.xml", privatekey);
            File.WriteAllText(label27.Text + @"\PublicKey.xml", publickey);
        }


        private void Blowpanel_MouseEnter(object sender, EventArgs e)
        {
            BlowText.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void Blowpanel_MouseLeave(object sender, EventArgs e)
        {
            if (!blow) BlowText.ForeColor = Color.FromArgb(255, 255, 255);
        }
        private void LOKIpanel_MouseEnter(object sender, EventArgs e)
        {
            LokiText.ForeColor = Color.FromArgb(173, 179, 165);
        }
        private void LOKIpanel_MouseLeave(object sender, EventArgs e)
        {
            if (!loki) LokiText.ForeColor = Color.FromArgb(255, 255, 255);
        }
        #endregion

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
