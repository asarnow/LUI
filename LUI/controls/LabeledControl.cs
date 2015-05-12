using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUI.controls
{
    public partial class LabeledControl<T> : FlowLayoutPanel where T:Control 
    {
        public T Control { get; set; }
        public DisabledRichTextBox Label { get; set; }

        public LabeledControl(T Control) : this (Control, "") {}

        public LabeledControl(T Control, string Text)
        {
            AutoSize = true;
            this.Control = Control;
            Label = new DisabledRichTextBox();
            Label.ScrollBars = RichTextBoxScrollBars.None;
            Label.Size = TextRenderer.MeasureText(Text, Label.Font);
            Label.Text = Text;
            Label.BorderStyle = BorderStyle.None;
            Label.BackColor = BackColor;
            Label.Anchor = AnchorStyles.Left;

            Controls.Add(Label);
            Controls.Add(Control);
        }

        public LabeledControl() { }
    }
}
