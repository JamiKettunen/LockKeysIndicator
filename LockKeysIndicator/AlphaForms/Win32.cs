using System;
using System.Runtime.InteropServices;

static class Win32 // AlphaForms by Anthony Mushrow
{
    public enum Message : uint
    {
        WM_NCHITTEST = 132,
        HTTRANSPARENT = 0xFFFFFFFF,
        HTCLIENT = 1,
        HTCAPTION = 2,
        WM_NCMOUSEMOVE = 160,
        WM_NCLBUTTONDOWN = 161,
        WM_NCLBUTTONUP = 162,
        WM_NCLBUTTONDBLCLK = 163,
        WM_WINDOWPOSCHANGING = 70,
        WM_ENTERSIZEMOVE = 561,
        WM_EXITSIZEMOVE = 562,
        WM_SYSCOMMAND = 274,
        WM_PAINT = 15,
        HWND_TOP = 0,
        SC_MINIMIZE = 61472,
        SC_RESTORE = 61728,
        SC_MAXIMIZE = 61488,
        WM_SIZE = 5,
        WM_ACTIVATE = 6,
        WM_SETFOCUS = 7,
        WM_SETCURSOR = 32,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_MOUSEWHEEL = 0x020A,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C,
        WM_XBUTTONDBLCLK = 0x020D,
        WM_MOUSELEAVE = 0x02A3,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_NCACTIVATE = 0X0086,
        GWL_WNDPROC = 0xFFFFFFFC,
        GWL_EXSTYLE = 0xFFFFFFEC
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPOS
    {
        public IntPtr hwnd;
        public IntPtr hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public WindowPosFlags flags;
    };

    [Flags]
    public enum WindowPosFlags : uint
    {
        NONE = 0x0000,
        SWP_NOSIZE = 0x0001,
        SWP_NOMOVE = 0x0002,
        SWP_NOZORDER = 0x0004,
        SWP_NOREDRAW = 0x0008,
        SWP_NOACTIVATE = 0x0010,
        SWP_FRAMECHANGED = 0x0020,
        SWP_SHOWWINDOW = 0x0040,
        SWP_HIDEWINDOW = 0x0080,
        SWP_NOCOPYBITS = 0x0100,
        SWP_NOOWNERZORDER = 0x0200,
        SWP_NOSENDCHANGING = 0x0400,
        SWP_DEFERERASE = 0x2000,
        SWP_ASYNCWINDOWPOS = 0x4000,
        SWP_CUSTOMFLAG = 0x8000
    };

    public enum WindowStyles
    {
        WS_EX_LAYERED = 0x00080000,
        /*WS_EX_LAYERED = 0x80000,*/
        GWL_EXSTYLE = -20,
        WS_EX_TRANSPARENT = 0x20
}

    public enum GetWindow_Cmd : uint
    {
        GW_HWNDFIRST = 0,
        GW_HWNDLAST = 1,
        GW_HWNDNEXT = 2,
        GW_HWNDPREV = 3,
        GW_OWNER = 4,
        GW_CHILD = 5,
        GW_ENABLEDPOPUP = 6
    };

    public enum SystemCursor : ulong
    {
        IDC_APPSTARTING = 32650, //Standard arrow and small hourglass
        IDC_NORMAL = 32512, //Standard arrow
        IDC_CROSS = 32515, //Crosshair
        IDC_HAND = 32649, //Hand
        IDC_HELP = 32651, //Arrow and question mark
        IDC_IBEAM = 32513, //I-beam
        IDC_NO = 32648, //Slashed circle
        IDC_SIZEALL = 32646, //Four-pointed arrow pointing north, south, east, and west
        IDC_SIZENESW = 32643, //Double-pointed arrow pointing northeast and southwest
        IDC_SIZENS = 32645, //Double-pointed arrow pointing north and south
        IDC_SIZENWSE = 32642, //Double-pointed arrow pointing northwest and southeast
        IDC_SIZEWE = 32644, //Double-pointed arrow pointing west and east
        IDC_UP = 32516, //Vertical arrow
        IDC_WAIT = 32514  //Hourglass
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BLENDFUNCTION
    {
        public byte BlendOp;
        public byte BlendFlags;
        public byte SourceConstantAlpha;
        public byte AlphaFormat;
    }

    public enum BlendOps : byte
    {
        AC_SRC_OVER = 0x00,
        AC_SRC_ALPHA = 0x01,
        AC_SRC_NO_PREMULT_ALPHA = 0x01,
        AC_SRC_NO_ALPHA = 0x02,
        AC_DST_NO_PREMULT_ALPHA = 0x10,
        AC_DST_NO_ALPHA = 0x20
    }

    public enum BlendFlags : uint
    {
        None = 0x00,
        ULW_COLORKEY = 0x01,
        ULW_ALPHA = 0x02,
        ULW_OPAQUE = 0x04
    }

    public enum TernaryRasterOperations : uint
    {
        SRCCOPY = 0x00CC0020,
        SRCPAINT = 0x00EE0086,
        SRCAND = 0x008800C6,
        SRCINVERT = 0x00660046,
        SRCERASE = 0x00440328,
        NOTSRCCOPY = 0x00330008,
        NOTSRCERASE = 0x001100A6,
        MERGECOPY = 0x00C000CA,
        MERGEPAINT = 0x00BB0226,
        PATCOPY = 0x00F00021,
        PATPAINT = 0x00FB0A09,
        PATINVERT = 0x005A0049,
        DSTINVERT = 0x00550009,
        BLACKNESS = 0x00000042,
        WHITENESS = 0x00FF0062
    }
}
