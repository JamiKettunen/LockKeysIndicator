using System;
using System.Windows.Forms;

namespace LockKeysIndicator
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();

            this.Localize();
        }

        private void lblNameAndVersion_SizeChanged(object sender, EventArgs e)
        {
            this.Width = 155 + lblProgramName.Width + 80;
        }

        public void Localize()
        {
            try
            {
                Shared.Language.Localize(this);
                RightToLeft rtl = Shared.Language.IsLayoutRTL ? RightToLeft.Yes : RightToLeft.No;
                if (this.RightToLeft != rtl) { this.RightToLeft = rtl; }
            }
            catch { Console.WriteLine("Couldn't localize FormAbout!"); }
        }
    }
}
