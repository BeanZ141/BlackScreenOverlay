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

        private CustomSlider transparencySlider;
        private CheckBox switchButton;
        private bool isBlurMode = false;

        public Overlay()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1000, 50);

            transparencySlider = new CustomSlider();
            transparencySlider.Visible = false;
            transparencySlider.Location = new Point(transparencyButton.Left - transparencySlider.Width - -55, transparencyButton.Top + 1);
            transparencySlider.ValueChanged += TransparencySlider_ValueChanged;
            transparencySlider.Size = new Size(80, 20);
            this.Controls.Add(transparencySlider);

            switchButton = new CustomSwitchButton();
            switchButton.Text = "";
            switchButton.Size = new Size(10, 10);
            switchButton.Location = new Point(transparencyButton.Left - switchButton.Width - 2, transparencyButton.Top);
            switchButton.CheckedChanged += SwitchButton_CheckedChanged;
            this.Controls.Add(switchButton);

            switchButton.Visible = false;

        }

        enum Accentstate
        {
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_DISABLED = 0
        }

        struct AccentPolicy
        {
            public Accentstate AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        public struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        public enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        public static class WindowExtension
        {
            [DllImport("user32.dll")]
            static internal extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        }


        private void SwitchButton_CheckedChanged(object sender, EventArgs e)
        {
            isBlurMode = switchButton.Checked;
            UpdateUIBasedOnMode();
        }

        private void UpdateUIBasedOnMode()
        {
            if (isBlurMode)
            {
                EnableBlur();
            }
            else
            {
                DisableBlur();
            }
        }

        private void EnableBlur()
        {
            var accent = new AccentPolicy
            {
                AccentState = Accentstate.ACCENT_ENABLE_BLURBEHIND
            };

            var accentStructsize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructsize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructsize,
                Data = accentPtr
            };

            WindowExtension.SetWindowCompositionAttribute(this.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }

        private void DisableBlur()
        {
            this.BackColor = Color.Black;

            var accent = new AccentPolicy
            {
                AccentState = Accentstate.ACCENT_DISABLED
            };

            var accentStructsize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructsize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructsize,
                Data = accentPtr
            };

            WindowExtension.SetWindowCompositionAttribute(this.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
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

        private void TransparencySlider_ValueChanged(object sender, EventArgs e)
        {
            double transparencyValue = (100 - transparencySlider.Value) / 100.0;
            const double minTransparency = 0.5;

            transparencyValue = Math.Max(minTransparency, transparencyValue);
            this.Opacity = transparencyValue;
        }

        private void transparencyButton_Click(object sender, EventArgs e)
        {
            transparencySlider.Visible = !transparencySlider.Visible;
            switchButton.Visible = !switchButton.Visible;
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
            transparencyButton.Location = new Point(ClientSize.Width - 45, 1);
            switchButton.Location = new Point(ClientSize.Width - 160, 7);
            transparencySlider.Location = new Point(transparencyButton.Left - transparencySlider.Width - 15, transparencyButton.Top + 1);
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
