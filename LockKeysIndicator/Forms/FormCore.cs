using System;
using System.IO;
using System.Drawing;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using LockKeysIndicator.Properties;

namespace LockKeysIndicator
{
    public partial class FormCore : Form
    {
        private bool lastNumState;          // Last NumLock status
        private bool indicatingNumState;    // Is NumLock status being indicated?
        private bool lastCapsState;         // Last CapsLock status
        private bool indicatingCapsState;   // Is CapsLock status being indicated?
        private bool lastScrollState;       // Last ScrollLock status
        private bool indicatingScrollState; // Is ScrollLock status being indicated?

        private FormIndicator formIndicator;
        private FormAbout formAbout;
        private System.Timers.Timer timerKeyUpdate = new System.Timers.Timer();
        private Keys toggledKey = Keys.None; // Num? Caps? Scroll?
        
        private String startupLnkPath = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\LockKeysIndicator.lnk";

        /// <summary>
        /// Removes program from ALT + TAB view & puts to "Background processes" list on TaskMgr.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80; // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }

        // Pass clicks through visible Form area (if any)
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = SafeNativeMethods.GetWindowLong(this.Handle, (int)Win32.WindowStyles.GWL_EXSTYLE);
            SafeNativeMethods.SetWindowLong(this.Handle, (int)Win32.WindowStyles.GWL_EXSTYLE,
                style | (int)Win32.WindowStyles.WS_EX_LAYERED | (int)Win32.WindowStyles.WS_EX_TRANSPARENT);
        }

        public FormCore()
        {
            timerKeyUpdate.Elapsed += timerKeyUpdate_Elapsed;
            timerKeyUpdate.Interval = 5;
            
            InitializeComponent();

            formIndicator = new FormIndicator();
            UpdateIndicatorPos();
            timerUpdateIndicatorPos.Start();
            formIndicator.Show();
        }

        private void FormCore_Load(object sender, EventArgs e)
        {
            this.Hide();
            bool firstStart = String.IsNullOrEmpty(Settings.Default.Language);
#if DEBUG
            //firstStart = true;
#endif

            LoadLanguage();
            
            #region Set initial LockKeys' statuses

            // Set last Lock key statuses to current ones
            string keysToIndicate = Settings.Default.KeysToIndicate;
            indicatingNumState = keysToIndicate.Contains("NUM");
            tsmiIndicateChangeNum.Checked = indicatingNumState;
            indicatingCapsState = keysToIndicate.Contains("CAPS");
            tsmiIndicateChangeCaps.Checked = indicatingCapsState;
            indicatingScrollState = keysToIndicate.Contains("SCROLL");
            tsmiIndicateChangeScroll.Checked = indicatingScrollState;
            keysToIndicate = null;

            lastNumState = IsKeyLocked(Keys.NumLock);
            lastCapsState = IsKeyLocked(Keys.CapsLock);
            lastScrollState = IsKeyLocked(Keys.Scroll);

            UpdateNotifyIconStatus();

            #endregion

            tsmiAutoStart.Checked = File.Exists(startupLnkPath);
            tsmiDisableIndication.Checked = Settings.Default.IndicationDisabled;
            
            string effects = Settings.Default.Effects; // "TRANSPARENCY,FADE,BLENDING"
            tsmiEffectsTransparency.Checked = effects.Contains("TRANSPARENCY");
            tsmiEffectsFade.Checked = effects.Contains("FADE");
            tsmiEffectsBlending.Checked = effects.Contains("BLENDING");
            effects = null;

            timerKeyUpdate.Enabled = true;
            
            if(firstStart) { notifyIcon.ShowBalloonTip(5000); }
        }

        // Every 5ms
        private void timerKeyUpdate_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Keys tmpToggledKey = (lastNumState != IsKeyLocked(Keys.NumLock) ? Keys.NumLock :
                lastCapsState != IsKeyLocked(Keys.CapsLock) ? Keys.CapsLock :
                lastScrollState != IsKeyLocked(Keys.Scroll) ? Keys.Scroll : Keys.None);
            
            if(tmpToggledKey != Keys.None) // A LockKey status has been changed
            {
                toggledKey = tmpToggledKey;

                // Toggle last status for the toggled LockKey
                if (toggledKey == Keys.NumLock) { lastNumState = !lastNumState; }
                else if (toggledKey == Keys.CapsLock) { lastCapsState = !lastCapsState; }
                else if (toggledKey == Keys.Scroll) { lastScrollState = !lastScrollState; }

                UpdateNotifyIconStatus();

                if(((toggledKey == Keys.NumLock && indicatingNumState) ||
                    (toggledKey == Keys.CapsLock && indicatingCapsState) ||
                    (toggledKey == Keys.Scroll && indicatingScrollState))
                    && !Settings.Default.IndicationDisabled)
                {
                    bool isKeyToggled = (toggledKey == Keys.NumLock ? lastNumState :
                        toggledKey == Keys.CapsLock ? lastCapsState :
                        toggledKey == Keys.Scroll ? lastScrollState : false);

                    formIndicator.UpdateKey(toggledKey, isKeyToggled);
                }
            }
        }

        // Every 20ms
        private void timerUpdateIndicatorPos_Tick(object sender, EventArgs e)
        {
            UpdateIndicatorPos();
        }
        
        private Point curPos = Cursor.Position;
        private Screen active = null;

        private void UpdateIndicatorPos()
        {
            try
            {
                int loc = Settings.Default.LocationOnScreen; // Location index ( -1 = Active, 0 = Primary, > 0 = MonitorX )

                if (loc < 0) // Get active screen (where cursor is on)
                {
                    curPos = Cursor.Position;
                    Screen[] screens = Screen.AllScreens;

                    for (int i = 0; i < screens.Length; i++)
                    {
                        active = screens[i];
                        Point min = active.Bounds.Location;
                        Point max = new Point(active.Bounds.Width + min.X - 1, active.Bounds.Height + min.Y - 1);

                        if (curPos.X >= min.X && curPos.Y >= min.Y && curPos.X <= max.X && curPos.Y <= max.Y) { break; }
                    }
                }
                else { if (active != null) { active = null; } }

                Screen display = (loc < 0 ? active : loc == 0 ? Screen.PrimaryScreen : loc > 0 ? Screen.AllScreens[loc - 1] : null);

                if (display != null)
                {
                    Size screenSize = new Size(display.Bounds.Width, display.Bounds.Height);
                    formIndicator.Location = new Point((screenSize.Width - formIndicator.Width) / 2 + display.Bounds.X,
                        ((screenSize.Height - formIndicator.Height) / 2) + (screenSize.Height / 5) + display.Bounds.Y);
                }
            }
            catch { Settings.Default.LocationOnScreen = -1; Settings.Default.Save(); }
            
        }

        #region Localization

        private void LoadLanguage()
        {
            CultureInfo uiLocale = CultureInfo.CurrentUICulture; // CurrentCulture
            string locale = uiLocale.Name;
            locale = locale.Contains("-") ? (locale.Substring(0, locale.IndexOf('-'))) : locale;
            //Console.WriteLine("Reported OS locale: {0}", uiLocale.Name);

            Shared.Language.Unload();
            tsmiLanguage.DropDownItems.Clear();

            ResourceSet resourceSet = Resources.ResourceManager.GetResourceSet(uiLocale, true, true);
            string langToLoad = null;

            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = entry.Key.ToString();
                //Console.WriteLine("RESOURCE >>> " + resourceKey);

                if (resourceKey.StartsWith("localization_"))
                {
                    //Debug.WriteLine("=> Resources > Localization > " + resourceKey);

                    string language = entry.Value.ToString();
                    bool loaded = Shared.Language.Load(language, false);

                    ToolStripMenuItem tsmi = new ToolStripMenuItem();

                    if (loaded)
                    {
                        string lang = Shared.Language.Tokens["language"];
                        string lang_en = Shared.Language.Tokens["language_en"];

                        // Get flag icon, add to language context menu etc.
                        tsmi.Text = $"{lang}" + ((lang != lang_en) ? $" ({lang_en})" : "");
                        Console.WriteLine("=> LoadLanguage(): Added language: '" + lang_en + "'");

                        try // Try adding language flag icon
                        {
                            tsmi.Image = Base64ToImage(Shared.Language.Tokens["flag"]);
                            tsmi.ImageScaling = ToolStripItemImageScaling.None;
                        }
                        catch { }

                        tsmi.Tag = resourceKey;
                        tsmi.Click += tsmiLanguageItem_Click;
                        tsmiLanguage.DropDownItems.Add(tsmi);

                    }
                    else { Shared.Language.Unload(); }

                    if (loaded && langToLoad == null && (resourceKey == "localization_" + Settings.Default.Language || resourceKey == "localization_" + locale)) // Try to find language matching user's UI locale / settings
                    {
                        langToLoad = language;
                        tsmi.Checked = true; // Check currently loaded language
                        LocalizeUI(); // Actually change UI language (loading just changes Tokens (strings) )
                        if (Settings.Default.Language == "")
                        {
                            Settings.Default.Language = resourceKey.Substring(resourceKey.IndexOf("_") + 1);
                            Settings.Default.Save();
                        }
                    }
                }
            }

            if (langToLoad != null) { if (!Shared.Language.Load(langToLoad, false)) { Shared.Language.Unload(); } }
            
            if (Shared.Language?.Tokens == null) // Couldn't find matching language, just use english
            {
                //Shared.Language.Load(Resources.localization_en, false);
                Settings.Default.Language = "en";
                Settings.Default.Save();
            }
        }

        private void LocalizeUI()
        {
            try
            {
                UpdateNotifyIconStatus();

                notifyIcon.BalloonTipTitle = Shared.Language.Tokens["program_name"];
                notifyIcon.BalloonTipText = Shared.Language.Tokens["first_start_msg"];

                tsmiQuit.Text = Shared.Language.Tokens["quit"];
                tsmiLanguage.Text = Shared.Language.Tokens["lang"];
                tsmiEffects.Text = Shared.Language.Tokens["effects"];
                tsmiEffectsTransparency.Text = Shared.Language.Tokens["effects_transparency"];
                tsmiEffectsFade.Text = Shared.Language.Tokens["effects_fade"];
                tsmiEffectsBlending.Text = Shared.Language.Tokens["effects_blending"];
                tsmiAutoStart.Text = Shared.Language.Tokens["autostart"];
                tsmiIndicatorLocation.Text = Shared.Language.Tokens["indicator_location"];
                tsmiIndicateChange.Text = Shared.Language.Tokens["indicate_change"];
                tsmiDisableIndication.Text = Shared.Language.Tokens["indicate_disable"];
                tsmiAbout.Text = Shared.Language.Tokens["about"];

                tsmiActiveScreen.Text = Shared.Language.Tokens["active_screen"];
                tsmiPrimaryScreen.Text = Shared.Language.Tokens["primary_screen"];
                tsmiAllScreens.Text = Shared.Language.Tokens["all_screens"];

                //RightToLeft rtl = Shared.Language.IsLayoutRTL ? RightToLeft.Yes : RightToLeft.No;
                rmbNotifyIcon.RightToLeft = Shared.Language.IsLayoutRTL ? RightToLeft.Yes : RightToLeft.No; // rtl
                formAbout = (FormAbout)Application.OpenForms["FormAbout"];
                if(formAbout != null) { formAbout.Close(); Console.WriteLine("=> FormAbout was closed due to a Language change"); }
                
            }
            catch { Debug.WriteLine("=> LocalizeUI(): Localization failed! Parts of the UI may not be translated"); }
        }

        private void UpdateNotifyIconStatus()
        {
            string part2 = $" {Assembly.GetExecutingAssembly().GetName().Version.ToString()}\nNum: {(lastNumState ? "ON" : "OFF")}\nCaps: {(lastCapsState ? "ON" : "OFF")}\nScroll: {(lastScrollState ? "ON" : "OFF")}";
            try { notifyIcon.Text = $"{Shared.Language.Tokens["program_name"]}{part2}"; }
            catch { notifyIcon.Text = $"Lock Keys Indicator{part2}"; }
        }

        private void tsmiLanguageItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedLang = (sender as ToolStripMenuItem);
            string lang = "";
            try { lang = clickedLang.Tag?.ToString(); lang = lang.Substring(lang.IndexOf("_") + 1); } catch { }
            
            if(!clickedLang.Checked && !String.IsNullOrEmpty(lang))
            {
                foreach (ToolStripMenuItem tsmi in tsmiLanguage.DropDownItems)
                {
                    if(tsmi.Checked) { tsmi.Checked = false; }
                }
                bool loaded = Shared.Language.Load(Resources.ResourceManager.GetString("localization_" + lang), false);
                clickedLang.Checked = loaded;
                if(loaded) { Settings.Default.Language = lang; Settings.Default.Save(); LocalizeUI(); }
            }
        }

        #endregion

        private Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            formAbout = (FormAbout)Application.OpenForms["FormAbout"];
            if (formAbout == null) { new FormAbout().Show(); }
            else { formAbout.Focus(); }
        }

        private void tsmiQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmiIndicateChangeItem_CheckedChanged(object sender, EventArgs e)
        {
            var item = (sender as ToolStripMenuItem);
            string text = item.Text;
            
            if (text.StartsWith("Num")) { indicatingNumState = item.Checked; ToggleIndicatingStatus(Keys.NumLock, item.Checked); }
            else if (text.StartsWith("Caps")) { indicatingCapsState = item.Checked; ToggleIndicatingStatus(Keys.CapsLock, item.Checked); }
            else { indicatingScrollState = item.Checked; ToggleIndicatingStatus(Keys.Scroll, item.Checked); }
        }

        private void ToggleIndicatingStatus(Keys lockKey, bool indicating)
        {
            string keysToIndicate = Settings.Default.KeysToIndicate;

            indicatingNumState = keysToIndicate.Contains("NUM");
            indicatingCapsState = keysToIndicate.Contains("CAPS");
            indicatingScrollState = keysToIndicate.Contains("SCROLL");
            bool changed = false;

            if(lockKey == Keys.NumLock && indicatingNumState != indicating) { indicatingNumState = indicating; changed = true; }
            else if (lockKey == Keys.CapsLock && indicatingCapsState != indicating) { indicatingCapsState = indicating; changed = true; }
            else if (lockKey == Keys.Scroll && indicatingScrollState != indicating) { indicatingScrollState = indicating; changed = true; }

            if (changed)
            {
                Settings.Default.KeysToIndicate = $"{(indicatingNumState ? "NUM" : "")},{(indicatingCapsState ? "CAPS" : "")},{(indicatingScrollState ? "SCROLL" : "")}";
                Settings.Default.Save();

                Console.WriteLine("=> ToggleIndicatingStatus(): KeyToIndicate is now '" + Settings.Default.KeysToIndicate + "'");
            }
        }

        // Toggles an Indicator effect status;
        // Effects: 1 = Transparency, 2 = Fade, 3 = Blending (Images)
        private void ToggleEffectStatus(int effect, bool status)
        {
            string effects = Settings.Default.Effects; // "TRANSPARENCY,FADE,BLENDING"

            bool transparency = effect == 1 ? status : effects.Contains("TRANSPARENCY");
            bool fade = effect == 2 ? status : effects.Contains("FADE");
            bool blending = effect == 3 ? status : effects.Contains("BLENDING");

            formIndicator.TransparencyEnabled = transparency;
            formIndicator.FadingEnabled = (tsmiEffectsFade.Enabled && fade);
            formIndicator.BlendingEnabled = blending;

            effects = null;

            Settings.Default.Effects = $"{(transparency ? "TRANSPARENCY" : "")},{(fade ? "FADE" : "")},{(blending ? "BLENDING" : "")}";
            Settings.Default.Save();
        }
        
        private void tsmiAutoStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!tsmiAutoStart.Checked && !File.Exists(startupLnkPath)) // Create shortcut
                {
                    Shortcut shortcut = new Shortcut
                    {
                        Address = startupLnkPath,
                        Description = "An auto-start shortcut for Lock Keys Indicator",
                        IconLocation = Assembly.GetEntryAssembly().Location,
                        Target = Assembly.GetEntryAssembly().Location,
                        WorkingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
                    };
                    shortcut.Create();
                }
                else if (tsmiAutoStart.Checked && File.Exists(startupLnkPath)) { File.Delete(startupLnkPath); } // Remove shortcut

                tsmiAutoStart.Checked = File.Exists(startupLnkPath); // Finally change Checked status (if all goes well)
            }
            catch { }
        }

        private void rmbNotifyIcon_VisibleChanged(object sender, EventArgs e)
        {
            if (rmbNotifyIcon.Visible)
            {
                Screen[] displays = Screen.AllScreens;
                Screen display = null;
                string itemText = "";

                for (int i = 0; i < displays.Length; i++)
                {
                    display = displays[i];
                    itemText = $"{Shared.Language.Tokens["monitor"]} {i + 1} ({display.Bounds.Width}x{display.Bounds.Height}{(display.Primary ? $", {Shared.Language.Tokens["primary"]}" : "")})";

                    ToolStripMenuItem tsmiItem = new ToolStripMenuItem
                    {
                        Tag = i + 1,
                        Text = itemText,
                        Checked = (i == (Settings.Default.LocationOnScreen - 1))
                    };
                    tsmiItem.Click += tsmiScreenLocation_Click;
                    tsmiAllScreens.DropDownItems.Add(tsmiItem);
                }

                tsmiActiveScreen.Checked = (Settings.Default.LocationOnScreen < 0);
                tsmiPrimaryScreen.Checked = (Settings.Default.LocationOnScreen == 0);
                if (Settings.Default.LocationOnScreen > 0) { tsmiAllScreens.CheckState = CheckState.Indeterminate;  }
            }
            else
            {
                tsmiAllScreens.DropDownItems.Clear();
                tsmiAllScreens.Checked = false;
                tsmiActiveScreen.Checked = false;
            }
        }

        private void tsmiScreenLocation_Click(object sender, EventArgs e)
        {
            var item = (sender as ToolStripMenuItem);
            try
            {
                int val = int.Parse(item.Tag.ToString());
                if (Settings.Default.LocationOnScreen != val)
                {
                    Settings.Default.LocationOnScreen = val;
                    Settings.Default.Save();
                }
                UpdateIndicatorPos();
            }
            catch { }
        }

        private void tsmiDisableIndication_CheckedChanged(object sender, EventArgs e)
        {
            bool isDisabled = (tsmiDisableIndication.Checked);

            tsmiIndicateChangeNum.Enabled = !isDisabled;
            tsmiIndicateChangeCaps.Enabled = !isDisabled;
            tsmiIndicateChangeScroll.Enabled = !isDisabled;

            if (Settings.Default.IndicationDisabled != isDisabled)
            {
                Settings.Default.IndicationDisabled = isDisabled;
                Settings.Default.Save();
            }
        }

        private void tsmiEffect_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
            int tag = int.Parse(tsmi.Tag.ToString());
            bool enabled = tsmi.Checked;

            if (tag == 1) { tsmiEffectsFade.Enabled = tsmiEffectsTransparency.Checked; }

            ToggleEffectStatus(tag, enabled);
        }
    }
}
