namespace LockKeysIndicator
{
    partial class FormIndicator
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIndicator));
            this.timerHide = new System.Windows.Forms.Timer(this.components);
            this.timerFadeOut = new System.Windows.Forms.Timer(this.components);
            this.timerFadeIn = new System.Windows.Forms.Timer(this.components);
            this.picBlendKey = new BlendingPictureBox();
            this.timerBlendImg = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picBlendKey)).BeginInit();
            this.SuspendLayout();
            // 
            // timerHide
            // 
            this.timerHide.Interval = 650;
            this.timerHide.Tag = "650";
            this.timerHide.Tick += new System.EventHandler(this.timerHide_Tick);
            // 
            // timerFadeOut
            // 
            this.timerFadeOut.Interval = 10;
            this.timerFadeOut.Tick += new System.EventHandler(this.timerFadeOut_Tick);
            // 
            // timerFadeIn
            // 
            this.timerFadeIn.Interval = 10;
            this.timerFadeIn.Tick += new System.EventHandler(this.timerFadeIn_Tick);
            // 
            // picBlendKey
            // 
            this.picBlendKey.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBlendKey.BackColor = System.Drawing.Color.Transparent;
            this.picBlendKey.Blend = 0F;
            this.picBlendKey.Image1 = null;
            this.picBlendKey.Image2 = null;
            this.picBlendKey.Location = new System.Drawing.Point(10, 10);
            this.picBlendKey.Name = "picBlendKey";
            this.picBlendKey.Size = new System.Drawing.Size(164, 164);
            this.picBlendKey.TabIndex = 1;
            this.picBlendKey.TabStop = false;
            // 
            // timerBlendImg
            // 
            this.timerBlendImg.Interval = 10;
            this.timerBlendImg.Tick += new System.EventHandler(this.timerBlendImg_Tick);
            // 
            // FormIndicator
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Fuchsia;
            this.BlendedBackground = ((System.Drawing.Bitmap)(resources.GetObject("$this.BlendedBackground")));
            this.ClientSize = new System.Drawing.Size(184, 184);
            this.ControlBox = false;
            this.Controls.Add(this.picBlendKey);
            this.DrawControlBackgrounds = true;
            this.EnhancedRendering = true;
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormIndicator";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.SizeMode = LockKeysIndicator.AlphaForm.SizeModes.Stretch;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormIndicator";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            ((System.ComponentModel.ISupportInitialize)(this.picBlendKey)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerHide;
        private System.Windows.Forms.Timer timerFadeOut;
        private System.Windows.Forms.Timer timerFadeIn;
        private BlendingPictureBox picBlendKey;
        private System.Windows.Forms.Timer timerBlendImg;
    }
}