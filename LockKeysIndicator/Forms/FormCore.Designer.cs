namespace LockKeysIndicator
{
    partial class FormCore
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCore));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.rmbNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiIndicateChange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisableIndication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiIndicateChangeNum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIndicateChangeCaps = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIndicateChangeScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiIndicatorLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiActiveScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPrimaryScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiScreenLocationSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAllScreens = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEffects = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEffectsTransparency = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEffectsFade = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEffectsBlending = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAutoStart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerUpdateIndicatorPos = new System.Windows.Forms.Timer(this.components);
            this.rmbNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.rmbNotifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Visible = true;
            // 
            // rmbNotifyIcon
            // 
            this.rmbNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout,
            this.toolStripSeparator2,
            this.tsmiIndicateChange,
            this.tsmiIndicatorLocation,
            this.tsmiEffects,
            this.tsmiAutoStart,
            this.tsmiLanguage,
            this.toolStripSeparator1,
            this.tsmiQuit});
            this.rmbNotifyIcon.Name = "rmbNotifyIcon";
            this.rmbNotifyIcon.ShowItemToolTips = false;
            this.rmbNotifyIcon.Size = new System.Drawing.Size(170, 170);
            this.rmbNotifyIcon.VisibleChanged += new System.EventHandler(this.rmbNotifyIcon_VisibleChanged);
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Image = global::LockKeysIndicator.Properties.Resources.about_16x;
            this.tsmiAbout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(169, 22);
            this.tsmiAbout.Text = "about";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(166, 6);
            // 
            // tsmiIndicateChange
            // 
            this.tsmiIndicateChange.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDisableIndication,
            this.toolStripSeparator3,
            this.tsmiIndicateChangeNum,
            this.tsmiIndicateChangeCaps,
            this.tsmiIndicateChangeScroll});
            this.tsmiIndicateChange.Image = global::LockKeysIndicator.Properties.Resources.key_16x;
            this.tsmiIndicateChange.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiIndicateChange.Name = "tsmiIndicateChange";
            this.tsmiIndicateChange.Size = new System.Drawing.Size(169, 22);
            this.tsmiIndicateChange.Text = "indicate_change";
            // 
            // tsmiDisableIndication
            // 
            this.tsmiDisableIndication.CheckOnClick = true;
            this.tsmiDisableIndication.Name = "tsmiDisableIndication";
            this.tsmiDisableIndication.Size = new System.Drawing.Size(158, 22);
            this.tsmiDisableIndication.Text = "indicate_disable";
            this.tsmiDisableIndication.CheckedChanged += new System.EventHandler(this.tsmiDisableIndication_CheckedChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(155, 6);
            // 
            // tsmiIndicateChangeNum
            // 
            this.tsmiIndicateChangeNum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsmiIndicateChangeNum.Checked = true;
            this.tsmiIndicateChangeNum.CheckOnClick = true;
            this.tsmiIndicateChangeNum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiIndicateChangeNum.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiIndicateChangeNum.Name = "tsmiIndicateChangeNum";
            this.tsmiIndicateChangeNum.Size = new System.Drawing.Size(158, 22);
            this.tsmiIndicateChangeNum.Text = "Num Lock";
            this.tsmiIndicateChangeNum.CheckedChanged += new System.EventHandler(this.tsmiIndicateChangeItem_CheckedChanged);
            // 
            // tsmiIndicateChangeCaps
            // 
            this.tsmiIndicateChangeCaps.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsmiIndicateChangeCaps.Checked = true;
            this.tsmiIndicateChangeCaps.CheckOnClick = true;
            this.tsmiIndicateChangeCaps.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiIndicateChangeCaps.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiIndicateChangeCaps.Name = "tsmiIndicateChangeCaps";
            this.tsmiIndicateChangeCaps.Size = new System.Drawing.Size(158, 22);
            this.tsmiIndicateChangeCaps.Text = "Caps Lock";
            this.tsmiIndicateChangeCaps.CheckedChanged += new System.EventHandler(this.tsmiIndicateChangeItem_CheckedChanged);
            // 
            // tsmiIndicateChangeScroll
            // 
            this.tsmiIndicateChangeScroll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsmiIndicateChangeScroll.Checked = true;
            this.tsmiIndicateChangeScroll.CheckOnClick = true;
            this.tsmiIndicateChangeScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiIndicateChangeScroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsmiIndicateChangeScroll.Name = "tsmiIndicateChangeScroll";
            this.tsmiIndicateChangeScroll.Size = new System.Drawing.Size(158, 22);
            this.tsmiIndicateChangeScroll.Text = "Scroll Lock";
            this.tsmiIndicateChangeScroll.CheckedChanged += new System.EventHandler(this.tsmiIndicateChangeItem_CheckedChanged);
            // 
            // tsmiIndicatorLocation
            // 
            this.tsmiIndicatorLocation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiActiveScreen,
            this.tsmiPrimaryScreen,
            this.tsmiScreenLocationSeperator,
            this.tsmiAllScreens});
            this.tsmiIndicatorLocation.Image = global::LockKeysIndicator.Properties.Resources.monitor_16x;
            this.tsmiIndicatorLocation.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiIndicatorLocation.Name = "tsmiIndicatorLocation";
            this.tsmiIndicatorLocation.Size = new System.Drawing.Size(169, 22);
            this.tsmiIndicatorLocation.Text = "indicator_location";
            // 
            // tsmiActiveScreen
            // 
            this.tsmiActiveScreen.Name = "tsmiActiveScreen";
            this.tsmiActiveScreen.Size = new System.Drawing.Size(154, 22);
            this.tsmiActiveScreen.Tag = "-1";
            this.tsmiActiveScreen.Text = "active_screen";
            this.tsmiActiveScreen.Click += new System.EventHandler(this.tsmiScreenLocation_Click);
            // 
            // tsmiPrimaryScreen
            // 
            this.tsmiPrimaryScreen.Name = "tsmiPrimaryScreen";
            this.tsmiPrimaryScreen.Size = new System.Drawing.Size(154, 22);
            this.tsmiPrimaryScreen.Tag = "0";
            this.tsmiPrimaryScreen.Text = "primary_screen";
            this.tsmiPrimaryScreen.Click += new System.EventHandler(this.tsmiScreenLocation_Click);
            // 
            // tsmiScreenLocationSeperator
            // 
            this.tsmiScreenLocationSeperator.Name = "tsmiScreenLocationSeperator";
            this.tsmiScreenLocationSeperator.Size = new System.Drawing.Size(151, 6);
            // 
            // tsmiAllScreens
            // 
            this.tsmiAllScreens.Name = "tsmiAllScreens";
            this.tsmiAllScreens.Size = new System.Drawing.Size(154, 22);
            this.tsmiAllScreens.Text = "all_screens";
            // 
            // tsmiEffects
            // 
            this.tsmiEffects.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiEffectsTransparency,
            this.tsmiEffectsFade,
            this.tsmiEffectsBlending});
            this.tsmiEffects.Image = global::LockKeysIndicator.Properties.Resources.effects_16x;
            this.tsmiEffects.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiEffects.Name = "tsmiEffects";
            this.tsmiEffects.Size = new System.Drawing.Size(169, 22);
            this.tsmiEffects.Text = "effects";
            // 
            // tsmiEffectsTransparency
            // 
            this.tsmiEffectsTransparency.CheckOnClick = true;
            this.tsmiEffectsTransparency.Name = "tsmiEffectsTransparency";
            this.tsmiEffectsTransparency.Size = new System.Drawing.Size(182, 22);
            this.tsmiEffectsTransparency.Tag = "1";
            this.tsmiEffectsTransparency.Text = "effects_transparency";
            this.tsmiEffectsTransparency.CheckedChanged += new System.EventHandler(this.tsmiEffect_CheckedChanged);
            // 
            // tsmiEffectsFade
            // 
            this.tsmiEffectsFade.CheckOnClick = true;
            this.tsmiEffectsFade.Name = "tsmiEffectsFade";
            this.tsmiEffectsFade.Size = new System.Drawing.Size(182, 22);
            this.tsmiEffectsFade.Tag = "2";
            this.tsmiEffectsFade.Text = "effects_fade";
            this.tsmiEffectsFade.CheckedChanged += new System.EventHandler(this.tsmiEffect_CheckedChanged);
            // 
            // tsmiEffectsBlending
            // 
            this.tsmiEffectsBlending.CheckOnClick = true;
            this.tsmiEffectsBlending.Name = "tsmiEffectsBlending";
            this.tsmiEffectsBlending.Size = new System.Drawing.Size(182, 22);
            this.tsmiEffectsBlending.Tag = "3";
            this.tsmiEffectsBlending.Text = "effects_blending";
            this.tsmiEffectsBlending.CheckedChanged += new System.EventHandler(this.tsmiEffect_CheckedChanged);
            // 
            // tsmiAutoStart
            // 
            this.tsmiAutoStart.Image = global::LockKeysIndicator.Properties.Resources.windows_16x;
            this.tsmiAutoStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiAutoStart.Name = "tsmiAutoStart";
            this.tsmiAutoStart.Size = new System.Drawing.Size(169, 22);
            this.tsmiAutoStart.Text = "autostart";
            this.tsmiAutoStart.Click += new System.EventHandler(this.tsmiAutoStart_Click);
            // 
            // tsmiLanguage
            // 
            this.tsmiLanguage.Image = global::LockKeysIndicator.Properties.Resources.translate_16x;
            this.tsmiLanguage.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiLanguage.Name = "tsmiLanguage";
            this.tsmiLanguage.Size = new System.Drawing.Size(169, 22);
            this.tsmiLanguage.Text = "lang";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(166, 6);
            // 
            // tsmiQuit
            // 
            this.tsmiQuit.Image = global::LockKeysIndicator.Properties.Resources.quit_16x;
            this.tsmiQuit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmiQuit.Name = "tsmiQuit";
            this.tsmiQuit.Size = new System.Drawing.Size(169, 22);
            this.tsmiQuit.Text = "quit";
            this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
            // 
            // timerUpdateIndicatorPos
            // 
            this.timerUpdateIndicatorPos.Interval = 20;
            this.timerUpdateIndicatorPos.Tick += new System.EventHandler(this.timerUpdateIndicatorPos_Tick);
            // 
            // FormCore
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(32, 32);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(32, 32);
            this.MinimizeBox = false;
            this.Name = "FormCore";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "LKI Background Process";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.FormCore_Load);
            this.rmbNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip rmbNotifyIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem tsmiLanguage;
        private System.Windows.Forms.ToolStripMenuItem tsmiIndicateChange;
        private System.Windows.Forms.ToolStripMenuItem tsmiIndicateChangeNum;
        private System.Windows.Forms.ToolStripMenuItem tsmiIndicateChangeCaps;
        private System.Windows.Forms.ToolStripMenuItem tsmiIndicateChangeScroll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiAutoStart;
        private System.Windows.Forms.ToolStripMenuItem tsmiIndicatorLocation;
        private System.Windows.Forms.ToolStripMenuItem tsmiActiveScreen;
        private System.Windows.Forms.ToolStripSeparator tsmiScreenLocationSeperator;
        private System.Windows.Forms.ToolStripMenuItem tsmiAllScreens;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrimaryScreen;
        private System.Windows.Forms.Timer timerUpdateIndicatorPos;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisableIndication;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffects;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffectsTransparency;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffectsFade;
        private System.Windows.Forms.ToolStripMenuItem tsmiEffectsBlending;
    }
}

