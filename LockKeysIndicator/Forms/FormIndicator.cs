using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using LockKeysIndicator.Properties;

namespace LockKeysIndicator
{
    public partial class FormIndicator : AlphaForm
    {
        private List<Image> keyStates = null;

        protected internal bool TransparencyEnabled = true;
        protected internal bool FadingEnabled = true;
        protected internal bool BlendingEnabled = true;

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

        // Pass clicks through visible Form area
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var style = SafeNativeMethods.GetWindowLong(this.Handle, (int)Win32.WindowStyles.GWL_EXSTYLE);
            SafeNativeMethods.SetWindowLong(this.Handle, (int)Win32.WindowStyles.GWL_EXSTYLE,
                style | (int)Win32.WindowStyles.WS_EX_LAYERED | (int)Win32.WindowStyles.WS_EX_TRANSPARENT);
        }

        // Initializer
        public FormIndicator(Keys key = Keys.None, bool keyToggled = false)
        {
            // Load tilemap with key states
            keyStates = Tilemap.Load(Resources.icons_164x);
            if(keyStates == null) { Console.WriteLine("FormIndicator(): KeyState tilemap loading error has occured!"); }

            InitializeComponent();
            
            if (key != Keys.None) { UpdateKey(key, keyToggled); }
        }

        public void UpdateKey(Keys key, bool keyToggled)
        {
            if (InvokeRequired) // When called from FormCore etc.
            {
                Invoke(new MethodInvoker(delegate { UpdateKey(key, keyToggled); }));
                return; // Stop executing the non-invoked function call
            }

            /*this.TransparencyEnabled = Settings.Default.Effects.Contains("TRANSPARENCY");
            this.FadingEnabled = Settings.Default.Effects.Contains("FADE");
            this.BlendingEnabled = Settings.Default.Effects.Contains("BLENDING");*/

            if (timerHide.Enabled) { timerHide.Stop(); }
            FadeIn();

            if(keyStates != null) // Tilemap loaded successfully!
            {
                int keyIndex = (key == Keys.NumLock ? 0 : key == Keys.CapsLock ? 2 : key == Keys.Scroll ? 4 : -2);

                if (BlendingEnabled)
                {
                    // First time pressing a Lock key after starting the program
                    if (picBlendKey.Image1 == null || picBlendKey.Image2 == null)
                    { picBlendKey.Image = null; picBlendKey.Image2 = (keyToggled ? keyStates[keyIndex + 1] : keyStates[keyIndex]); }

                    BlendTo(keyStates[keyIndex + (keyToggled ? 0 : 1)]);
                }
                else
                {
                    if (picBlendKey.Image1 != null || picBlendKey.Image2 != null || picBlendKey.Blend != 0)
                    {
                        timerBlendImg.Stop();
                        picBlendKey.Blend = 0;
                        picBlendKey.Image1 = null;
                        picBlendKey.Image2 = null;
                    }

                    picBlendKey.Image = keyStates[keyIndex + (keyToggled ? 0 : 1)];
                }
            }
        }

        private void BlendTo(Image blendImg)
        {
            timerBlendImg.Stop();
            picBlendKey.Image1 = picBlendKey.Image2;
            picBlendKey.Blend = 0;
            picBlendKey.Image2 = blendImg;
            timerBlendImg.Start();
        }

        private void timerBlendImg_Tick(object sender, EventArgs e)
        {
            if (picBlendKey.Image1 != null && picBlendKey.Image2 != null)
            {
                if (picBlendKey.Blend < 1.0F) { picBlendKey.Blend += 0.1F; }
                else { timerBlendImg.Stop(); }
            }
        }

        #region Fade effects

        protected internal bool Fading = false;
        double fadeEnd = 0.0;
        double fadeStep = 0.1;

        protected internal void FadeIn(double end = 0.95, double step = 0.1, int interval = 10)
        {
            if (InvokeRequired) // When called from FormCore etc.
            {
                Invoke(new MethodInvoker(delegate { FadeIn(end, step, interval); }));
                return; // Stop executing the non-invoked function call
            }

            StopFading();
            timerFadeIn.Interval = interval;
            fadeEnd = end;

            Fading = true;
            timerFadeIn.Start();
        }

        private void timerFadeIn_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < fadeEnd && FadingEnabled) { this.Opacity += fadeStep; UpdateLayeredBackground(); }
            else { timerFadeIn.Stop(); this.Opacity = (TransparencyEnabled ? fadeEnd : 1); UpdateLayeredBackground(); Fading = false; timerHide.Start(); }
        }

        protected internal void FadeOut(double end = 0.0, double step = 0.1, int interval = 10)
        {
            if (InvokeRequired) // When called from FormCore etc.
            {
                Invoke(new MethodInvoker(delegate { FadeOut(end, step, interval); }));
                return; // Stop executing the non-invoked function call
            }

            StopFading();
            timerFadeOut.Interval = interval;
            fadeEnd = end;
            
            Fading = true;
            timerFadeOut.Start();
        }

        private void timerFadeOut_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > fadeEnd && FadingEnabled) { this.Opacity -= fadeStep; UpdateLayeredBackground(); }
            else { timerFadeOut.Stop(); this.Opacity = fadeEnd; UpdateLayeredBackground(); picBlendKey.Image2 = null; Fading = false; }
        }

        protected internal void StopFading()
        {
            if (InvokeRequired) // When called from FormCore etc.
            {
                Invoke(new MethodInvoker(delegate { StopFading(); }));
                return; // Stop executing the non-invoked function call
            }

            if (Fading) { timerFadeIn.Stop(); timerFadeOut.Stop(); Fading = false; }
        }

        private void timerHide_Tick(object sender, EventArgs e)
        {
            timerHide.Stop();

            if (!Fading) { FadeOut(); }
        }

        #endregion
    }
}
