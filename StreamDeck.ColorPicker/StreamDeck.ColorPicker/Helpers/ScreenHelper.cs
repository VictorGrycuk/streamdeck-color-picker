using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace StreamDeck.ColorPicker.Helpers
{
    public static class ScreenHelper
    {
        private enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);

        internal static Point GetMouseLocation()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);
            }

            var cursorLocation = new Point();
            GetCursorPos(ref cursorLocation);

            return cursorLocation;
        }

        internal static Color GetPixelColor(Point location)
            => Win32.GetPixelColor(location.X, location.Y);
    }

    sealed class Win32
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        static public Color GetPixelColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                            (int)(pixel & 0x0000FF00) >> 8,
                            (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
    }
}
