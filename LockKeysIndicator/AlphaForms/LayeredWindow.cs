using System;
using System.Drawing;
using System.Windows.Forms;

namespace LockKeysIndicator // AlphaForms by Anthony Mushrow
{
    class LayeredWindow : Form
    {
        private Rectangle m_rect;

        public Point LayeredPos
        {
            get => m_rect.Location;
            set { m_rect.Location = value; }
        }

        public Size LayeredSize
        {
            get => m_rect.Size;
        }

        public LayeredWindow()
        {
            //We need to set this before the window is created, otherwise we
            //have to reset thw window styles using SetWindowLong because
            //the window will no longer be drawn
            this.ShowInTaskbar = false;

            this.FormBorderStyle = FormBorderStyle.None;
        }

        public void UpdateWindow(Bitmap image, byte opacity)
        {
            UpdateWindow(image, opacity, -1, -1, this.LayeredPos);
        }

        public void UpdateWindow(Bitmap image, byte opacity, int width, int height, Point pos)
        {
            IntPtr hdcWindow = SafeNativeMethods.GetWindowDC(this.Handle);
            IntPtr hDC = SafeNativeMethods.CreateCompatibleDC(hdcWindow);
            IntPtr hBitmap = image.GetHbitmap(Color.FromArgb(0));
            IntPtr hOld = SafeNativeMethods.SelectObject(hDC, hBitmap);
            Size size = new Size(0, 0);
            Point zero = new Point(0, 0);

            if (width == -1 || height == -1)
            {
                //No width and height specified, use the size of the image
                size.Width = image.Width;
                size.Height = image.Height;
            }
            else
            {
                //Use whichever size is smallest, so that the image will
                //be clipped if necessary
                size.Width = Math.Min(image.Width, width);
                size.Height = Math.Min(image.Height, height);
            }
            m_rect.Size = size;
            m_rect.Location = pos;

            Win32.BLENDFUNCTION blend = new Win32.BLENDFUNCTION();
            blend.BlendOp = (byte)Win32.BlendOps.AC_SRC_OVER;
            blend.SourceConstantAlpha = opacity;
            blend.AlphaFormat = (byte)Win32.BlendOps.AC_SRC_ALPHA;
            blend.BlendFlags = (byte)Win32.BlendFlags.None;

            SafeNativeMethods.UpdateLayeredWindow(this.Handle, hdcWindow, ref pos, ref size, hDC, ref zero, 0, ref blend, Win32.BlendFlags.ULW_ALPHA);

            SafeNativeMethods.SelectObject(hDC, hOld);
            SafeNativeMethods.DeleteObject(hBitmap);
            SafeNativeMethods.DeleteDC(hDC);
            SafeNativeMethods.ReleaseDC(this.Handle, hdcWindow);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= (int)Win32.WindowStyles.WS_EX_LAYERED;
                return cp;
            }
        }
    }
}
