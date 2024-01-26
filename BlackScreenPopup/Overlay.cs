using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace BlackScreenPopup
{
    public partial class Overlay : Form, IDisposable
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CAPTION = 0x2;
        private const int HT_BOTTOM = 0xF;
        private const int HT_LEFT = 0xA;
        private const int HT_RIGHT = 0xB;
        private const int HT_TOP = 0x8;
        private const int HT_TOPLEFT = 0xD;
        private const int HT_TOPRIGHT = 0xC;
        private const int HT_BOTTOMLEFT = 0x10;
        private const int HT_BOTTOMRIGHT = 0x11;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]

        public static extern bool ReleaseCapture();

        public Overlay()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1000, 50);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST)
            {
                int x = (int)m.LParam & 0xFFFF;
                int y = ((int)m.LParam >> 16) & 0xFFFF;
                Point cursorPos = PointToClient(new Point(x, y));

                const int cornerSize = 10;

                if (cursorPos.Y <= cornerSize && cursorPos.X <= cornerSize)
                    m.Result = (IntPtr)HT_TOPLEFT;
                else if (cursorPos.Y <= cornerSize && cursorPos.X >= ClientSize.Width - cornerSize)
                    m.Result = (IntPtr)HT_TOPRIGHT;
                else if (cursorPos.Y >= ClientSize.Height - cornerSize && cursorPos.X <= cornerSize)
                    m.Result = (IntPtr)HT_BOTTOMLEFT;
                else if (cursorPos.Y >= ClientSize.Height - cornerSize && cursorPos.X >= ClientSize.Width - cornerSize)
                    m.Result = (IntPtr)HT_BOTTOMRIGHT;
                else if (cursorPos.Y <= cornerSize)
                    m.Result = (IntPtr)HT_TOP;
                else if (cursorPos.Y >= ClientSize.Height - cornerSize)
                    m.Result = (IntPtr)HT_BOTTOM;
                else if (cursorPos.X <= cornerSize)
                    m.Result = (IntPtr)HT_LEFT;
                else if (cursorPos.X >= ClientSize.Width - cornerSize)
                    m.Result = (IntPtr)HT_RIGHT;
                else
                    m.Result = (IntPtr)HT_CAPTION;
            }   
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            closeButton.Location = new Point(ClientSize.Width - 15, 1);
            minimizeButton.Location = new Point(ClientSize.Width - 30, 1);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.P)
            {
                TopMost = !TopMost;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
