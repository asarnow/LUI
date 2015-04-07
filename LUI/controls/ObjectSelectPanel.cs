using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUI.controls
{
    public class ObjectSelectPanel : FlowLayoutPanel
    {
        public event EventHandler CameraChanged;
        public event EventHandler BeamFlagsChanged;

        LabeledControl<ComboBox> _Camera;
        public ComboBox Camera
        {
            get
            {
                return _Camera.Control;
            }
        }

        LabeledControl<ComboBox> _BeamFlags;
        public ComboBox BeamFlags
        {
            get
            {
                return _BeamFlags.Control;
            }
        }


        public ObjectSelectPanel()
        {
            this.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            _Camera = new LabeledControl<ComboBox>(new ComboBox(), "Camera:");
            Camera.DropDownStyle = ComboBoxStyle.DropDownList;
            Camera.DisplayMember = "Name";
            Camera.SelectedIndexChanged += CameraChanged;
            Controls.Add(_Camera);

            _BeamFlags = new LabeledControl<ComboBox>(new ComboBox(), "Beam Flags:");
            BeamFlags.DropDownStyle = ComboBoxStyle.DropDownList;
            BeamFlags.DisplayMember = "Name";
            BeamFlags.SelectedIndexChanged += BeamFlagsChanged;
            Controls.Add(_BeamFlags);
        }
    }
}
