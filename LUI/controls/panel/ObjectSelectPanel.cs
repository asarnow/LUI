using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom.camera;
using lasercom.control;
using Extensions;

namespace LUI.controls
{
    public class ObjectSelectPanel : FlowLayoutPanel
    {
        public event EventHandler CameraChanged;
        public event EventHandler BeamFlagsChanged;

        LabeledControl<ComboBox> _Cameras;
        public ComboBox Cameras
        {
            get
            {
                return _Cameras.Control;
            }
        }

        public CameraParameters SelectedCamera
        {
            get
            {
                return (CameraParameters)Cameras.SelectedItem;
            }
            set
            {
                Cameras.SelectedItem = value;
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

        public BeamFlagsParameters SelectedBeamFlags
        {
            get
            {
                return (BeamFlagsParameters)BeamFlags.SelectedItem;
            }
            set
            {
                BeamFlags.SelectedItem = value;
            }
        }


        public ObjectSelectPanel()
        {
            this.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            _Cameras = new LabeledControl<ComboBox>(new ComboBox(), "Camera:");
            Cameras.DropDownStyle = ComboBoxStyle.DropDownList;
            Cameras.DisplayMember = "Name";
            Cameras.SelectedIndexChanged += (s,e) => CameraChanged.Raise(s,e);
            Controls.Add(_Cameras);

            _BeamFlags = new LabeledControl<ComboBox>(new ComboBox(), "Beam Flags:");
            BeamFlags.DropDownStyle = ComboBoxStyle.DropDownList;
            BeamFlags.DisplayMember = "Name";
            BeamFlags.SelectedIndexChanged += (s, e) => BeamFlagsChanged.Raise(s, e);
            Controls.Add(_BeamFlags);
        }
    }
}
