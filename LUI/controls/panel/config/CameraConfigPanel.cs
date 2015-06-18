using System.Windows.Forms;
using lasercom.camera;

namespace LUI.controls
{
    public abstract class CameraConfigPanel : LuiObjectConfigPanel<CameraParameters>
    {
        LabeledControl<TextBox> CalFile;

        public CameraConfigPanel() : base()
        {
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
        }

        public override void CopyTo(CameraParameters other)
        {
            other.CalFile = CalFile.Control.Text;
        }
    }
}
