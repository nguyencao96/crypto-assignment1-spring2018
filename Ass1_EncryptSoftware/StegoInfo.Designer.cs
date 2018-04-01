namespace Ass1_EncryptSoftware
{
    partial class StegoInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnInfoButton = new System.Windows.Forms.Button();
            this.EnInfoLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EnInfoImage2 = new System.Windows.Forms.PictureBox();
            this.EnInfoImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.EnInfoImage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnInfoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // EnInfoButton
            // 
            this.EnInfoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(179)))), ((int)(((byte)(165)))));
            this.EnInfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EnInfoButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnInfoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(76)))), ((int)(((byte)(90)))));
            this.EnInfoButton.Location = new System.Drawing.Point(275, 354);
            this.EnInfoButton.Name = "EnInfoButton";
            this.EnInfoButton.Size = new System.Drawing.Size(92, 31);
            this.EnInfoButton.TabIndex = 45;
            this.EnInfoButton.Text = "OK";
            this.EnInfoButton.UseVisualStyleBackColor = false;
            this.EnInfoButton.Click += new System.EventHandler(this.EnInfoButton_Click);
            // 
            // EnInfoLabel
            // 
            this.EnInfoLabel.AutoSize = true;
            this.EnInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.EnInfoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnInfoLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(76)))), ((int)(((byte)(90)))));
            this.EnInfoLabel.Location = new System.Drawing.Point(19, 287);
            this.EnInfoLabel.Name = "EnInfoLabel";
            this.EnInfoLabel.Size = new System.Drawing.Size(506, 34);
            this.EnInfoLabel.TabIndex = 44;
            this.EnInfoLabel.Text = "Steganography is the practice of concealing a file, message, image, \r\nor video wi" +
    "thin another file, message, image, or video.";
            this.EnInfoLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StegoInfo_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(76)))), ((int)(((byte)(90)))));
            this.label3.Location = new System.Drawing.Point(23, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 31);
            this.label3.TabIndex = 42;
            this.label3.Text = "Steganography";
            this.label3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StegoInfo_MouseDown);
            // 
            // EnInfoImage2
            // 
            this.EnInfoImage2.Image = global::Ass1_EncryptSoftware.Properties.Resources.stego2;
            this.EnInfoImage2.Location = new System.Drawing.Point(335, 49);
            this.EnInfoImage2.Name = "EnInfoImage2";
            this.EnInfoImage2.Size = new System.Drawing.Size(310, 217);
            this.EnInfoImage2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.EnInfoImage2.TabIndex = 46;
            this.EnInfoImage2.TabStop = false;
            this.EnInfoImage2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StegoInfo_MouseDown);
            // 
            // EnInfoImage
            // 
            this.EnInfoImage.Image = global::Ass1_EncryptSoftware.Properties.Resources.stego;
            this.EnInfoImage.Location = new System.Drawing.Point(22, 79);
            this.EnInfoImage.Name = "EnInfoImage";
            this.EnInfoImage.Size = new System.Drawing.Size(307, 160);
            this.EnInfoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.EnInfoImage.TabIndex = 43;
            this.EnInfoImage.TabStop = false;
            this.EnInfoImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StegoInfo_MouseDown);
            // 
            // StegoInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 427);
            this.Controls.Add(this.EnInfoImage2);
            this.Controls.Add(this.EnInfoButton);
            this.Controls.Add(this.EnInfoLabel);
            this.Controls.Add(this.EnInfoImage);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StegoInfo";
            this.Text = "StegoInfo";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.StegoInfo_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.EnInfoImage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnInfoImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox EnInfoImage2;
        private System.Windows.Forms.Button EnInfoButton;
        private System.Windows.Forms.Label EnInfoLabel;
        private System.Windows.Forms.PictureBox EnInfoImage;
        private System.Windows.Forms.Label label3;
    }
}