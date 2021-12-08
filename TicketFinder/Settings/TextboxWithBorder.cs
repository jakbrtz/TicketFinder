using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TicketFinder.Settings
{
    public class TextboxWithBorder : TextBox
    {
        private const int WM_PAINT = 0x000F;
        private ButtonBorderStyle m_BorderStyle = ButtonBorderStyle.Solid;
        private Color m_BorderColor = Color.DarkGray;

        public TextboxWithBorder() { }

        public new ButtonBorderStyle BorderStyle
        {
            get => m_BorderStyle;
            set
            {
                if (m_BorderStyle != value)
                {
                    m_BorderStyle = value;
                    Invalidate();
                }
            }
        }

        [DefaultValue(typeof(Color), "DarkGray")]
        public Color BorderColor
        {
            get => m_BorderColor;
            set
            {
                if (m_BorderColor != value)
                {
                    m_BorderColor = value;
                    Invalidate();
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case WM_PAINT:
                    if (IsHandleCreated)
                    {
                        using (var g = Graphics.FromHwndInternal(this.Handle))
                        {
                            ControlPaint.DrawBorder(g, ClientRectangle, m_BorderColor, m_BorderStyle);
                        }
                        m.Result = IntPtr.Zero;
                    }
                    break;
            }
        }
    }
}
