using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUI.controls
{
    public class DisabledRichTextBox : System.Windows.Forms.RichTextBox
    {
        private const int WM_SETFOCUS = 0x07;
        private const int WM_ENABLE = 0x0A;
        private const int WM_SETCURSOR = 0x20;

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        [Category("Appearance")]
        public override bool AutoSize { get; set; }

        public DisabledRichTextBox()
            : base()
        {
            SetAutoSizeMode(System.Windows.Forms.AutoSizeMode.GrowAndShrink);
            TextChanged += HandleTextChanged;
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            // Originally included WM_ENABLE.
            // Removed to allow enabling/disabling to change text color.
            // WM_SETFOCUS and WM_SETCURSOR appear sufficient for use as label.
            if (!(m.Msg == WM_SETFOCUS || m.Msg == WM_SETCURSOR))
                base.WndProc(ref m);
        }

        private System.Drawing.Size GetAutoSize()
        {
            return System.Windows.Forms.TextRenderer.MeasureText(Text, Font);
        }

        public override System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)
        {
            return GetAutoSize();
        }

        private void ResizeForAutoSize()
        {
            if (this.AutoSize)
            {
                SetBoundsCore(this.Left, this.Top, this.Width, this.Height, System.Windows.Forms.BoundsSpecified.Size);
            }
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, System.Windows.Forms.BoundsSpecified specified)
        {

            //  Only when the size is affected...
            if (this.AutoSize && (specified & System.Windows.Forms.BoundsSpecified.Size) != 0)
            {
                System.Drawing.Size size = GetAutoSize();

                width = size.Width;
                height = size.Height;
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }

        private void HandleTextChanged(object sender, EventArgs e)
        {
            this.Size = GetAutoSize();
        }
    }
}
