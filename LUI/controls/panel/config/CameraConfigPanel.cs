using System;
using System.Windows.Forms;
using lasercom.camera;

namespace LUI.controls
{
    public abstract class CameraConfigPanel : LuiObjectConfigPanel<CameraParameters>
    {
        LabeledControl<TextBox> CalFile;
        LabeledControl<NumericUpDown> VBin;
        LabeledControl<NumericUpDown> VStart;
        LabeledControl<NumericUpDown> VEnd;
        LabeledControl<ComboBox> ReadMode;
        LabeledControl<NumericUpDown> SaturationLevel;

        ToolTip BinTip;

        public CameraConfigPanel() : base()
        {
            this.SuspendLayout();
            CalFile = new LabeledControl<TextBox>(new TextBox(), "Calibration file:");
            CalFile.Control.TextChanged += OnOptionsChanged;
            CalFile.Control.TextChanged += (s, e) => CalFile.Control.AutoResize();
            CalFile.Control.MinimumSize = new System.Drawing.Size(40,0);
            CalFile.Control.Text = "";
            var Browse = new Button();
            Browse.Text = "...";
            Browse.AutoSize = true;
            Browse.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Browse.Click += Browse_Click;
            CalFile.Controls.Add(Browse);
            this.Controls.Add(CalFile);

            SaturationLevel = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Saturation level:");
            SaturationLevel.Control.Minimum = 0;
            SaturationLevel.Control.Maximum = (int)Math.Pow(2, 16);
            SaturationLevel.Control.ValueChanged += OnOptionsChanged;
            this.Controls.Add(SaturationLevel);

            var ImagePanel = new FlowLayoutPanel();
            ImagePanel.FlowDirection = FlowDirection.LeftToRight;
            ImagePanel.AutoSize = true;
            ImagePanel.WrapContents = false;
            VBin = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Vertical bin size:");
            VBin.Control.Minimum = -1;
            VBin.Control.Width = 54;
            VBin.Control.ValueChanged += VBin_ValueChanged;
            VBin.Control.ValueChanged += OnOptionsChanged;
            VStart = new LabeledControl<NumericUpDown>(new NumericUpDown(), "First row:");
            VStart.Control.Minimum = -1;
            VStart.Control.Width = VBin.Control.Width;
            VStart.Control.ValueChanged += VStart_ValueChanged;
            VStart.Control.ValueChanged += OnOptionsChanged;
            VEnd = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Last row:");
            VEnd.Control.Minimum = -1;
            VEnd.Control.Maximum = int.MaxValue;
            VEnd.Control.Width = VBin.Control.Width;
            VEnd.Control.ValueChanged += VEnd_ValueChanged;
            VEnd.Control.ValueChanged += OnOptionsChanged;
            ImagePanel.Controls.Add(VBin);
            ImagePanel.Controls.Add(VStart);
            ImagePanel.Controls.Add(VEnd);
            this.Controls.Add(ImagePanel);

            ReadMode = new LabeledControl<ComboBox>(new ComboBox(), "Read Mode:");
            ReadMode.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            ReadMode.Control.DisplayMember = "Item1";
            ReadMode.Control.ValueMember = "Item2";
            ReadMode.Control.Items.Add(new Tuple<string, int>("Full Vertical Binning", AndorCamera.ReadModeFVB));
            ReadMode.Control.Items.Add(new Tuple<string, int>("Image", AndorCamera.ReadModeImage));
            ReadMode.Control.SelectedIndex = 0;
            ReadMode.Control.SelectedIndexChanged += OnOptionsChanged;
            this.Controls.Add(ReadMode);

            BinTip = new ToolTip();
            BinTip.SetToolTip(VEnd.Control, "Row count should be a multiple of bin size.");
            BinTip.SetToolTip(VStart.Control, "Row count should be a multiple of bin size.");
            BinTip.SetToolTip(VBin.Control, "Row count should be a multiple of bin size.");

            this.ResumeLayout();
        }

        private void VEnd_ValueChanged(object sender, System.EventArgs e)
        {
            VStart.Control.Maximum = VEnd.Control.Value;
            VBin.Control.Maximum = VEnd.Control.Value - VStart.Control.Value + 1;
            ImageCheck();
        }

        private void VStart_ValueChanged(object sender, System.EventArgs e)
        {
            VEnd.Control.Minimum = VStart.Control.Value;
            VBin.Control.Maximum = VEnd.Control.Value - VStart.Control.Value + 1;
            ImageCheck();
        }

        private void VBin_ValueChanged(object sender, System.EventArgs e)
        {
            ImageCheck();
        }

        private void ImageCheck()
        {
            if (VBin.Control.Value == 0 || ((VEnd.Control.Value - VStart.Control.Value + 1) % VBin.Control.Value != 0))
                BinMultipleBad();
            else
                BinMultipleOk();
        }

        private void BinMultipleBad()
        {
            VStart.Control.ForeColor = System.Drawing.Color.Red;
            VEnd.Control.ForeColor = System.Drawing.Color.Red;
            VBin.Control.ForeColor = System.Drawing.Color.Red;
            BinTip.Active = true;
        }

        private void BinMultipleOk()
        {
            VStart.Control.ForeColor = System.Drawing.Color.Black;
            VEnd.Control.ForeColor = System.Drawing.Color.Black;
            VBin.Control.ForeColor = System.Drawing.Color.Black;
            BinTip.Active = false;
        }

        private void Browse_Click(object sender, System.EventArgs e)
        {
            var orig = CalFile.Control.Text;
            var user = GuiUtil.SimpleFileNameDialog("CAL Files|*.cal");
            CalFile.Control.Text = user != "" ? user : orig;
        }
    
        public override void CopyFrom(CameraParameters other)
        {
            CalFile.Control.Text = other.CalFile;
            VBin.Control.Value = other.VBin;
            VStart.Control.Value = other.VStart;
            VEnd.Control.Value = Math.Max(-1, other.VStart + other.VCount - 1);
            ReadMode.Control.SelectedIndex = other.ReadMode == AndorCamera.ReadModeFVB ? 0 : 1;
            SaturationLevel.Control.Value = other.SaturationLevel;

        }

        public override void CopyTo(CameraParameters other)
        {
            other.CalFile = CalFile.Control.Text;
            other.VBin = (int)VBin.Control.Value;
            other.VStart = (int)VStart.Control.Value;
            other.VCount = (int)Math.Max(-1, VEnd.Control.Value - VStart.Control.Value + 1);
            other.ReadMode = ReadMode.Control.SelectedItem != null ? ((Tuple<string,int>)ReadMode.Control.SelectedItem).Item2 : AndorCamera.ReadModeFVB;
            other.SaturationLevel = (int)SaturationLevel.Control.Value;
        }
    }
}
