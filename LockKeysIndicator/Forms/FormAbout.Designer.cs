namespace LockKeysIndicator
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.picProgramIcon = new System.Windows.Forms.PictureBox();
            this.lblProgramName = new System.Windows.Forms.Label();
            this.lblProgramVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picProgramIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // picProgramIcon
            // 
            this.picProgramIcon.Image = ((System.Drawing.Image)(resources.GetObject("picProgramIcon.Image")));
            this.picProgramIcon.Location = new System.Drawing.Point(16, 11);
            this.picProgramIcon.Name = "picProgramIcon";
            this.picProgramIcon.Size = new System.Drawing.Size(128, 128);
            this.picProgramIcon.TabIndex = 0;
            this.picProgramIcon.TabStop = false;
            // 
            // lblProgramName
            // 
            this.lblProgramName.AutoSize = true;
            this.lblProgramName.Font = new System.Drawing.Font("Segoe UI Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramName.Location = new System.Drawing.Point(172, 48);
            this.lblProgramName.Name = "lblProgramName";
            this.lblProgramName.Size = new System.Drawing.Size(168, 30);
            this.lblProgramName.TabIndex = 1;
            this.lblProgramName.Tag = "program_name";
            this.lblProgramName.Text = "program_name";
            this.lblProgramName.SizeChanged += new System.EventHandler(this.lblNameAndVersion_SizeChanged);
            // 
            // lblProgramVersion
            // 
            this.lblProgramVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgramVersion.Font = new System.Drawing.Font("Segoe UI Semilight", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramVersion.Location = new System.Drawing.Point(180, 78);
            this.lblProgramVersion.Name = "lblProgramVersion";
            this.lblProgramVersion.Size = new System.Drawing.Size(145, 25);
            this.lblProgramVersion.TabIndex = 1;
            this.lblProgramVersion.Tag = "version";
            this.lblProgramVersion.Text = "version {version}";
            this.lblProgramVersion.SizeChanged += new System.EventHandler(this.lblNameAndVersion_SizeChanged);
            // 
            // FormAbout
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(374, 153);
            this.Controls.Add(this.lblProgramName);
            this.Controls.Add(this.picProgramIcon);
            this.Controls.Add(this.lblProgramVersion);
            this.Font = new System.Drawing.Font("Segoe UI Semilight", 12.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "about_title";
            this.Text = "about_title";
            ((System.ComponentModel.ISupportInitialize)(this.picProgramIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picProgramIcon;
        private System.Windows.Forms.Label lblProgramName;
        private System.Windows.Forms.Label lblProgramVersion;
    }
}