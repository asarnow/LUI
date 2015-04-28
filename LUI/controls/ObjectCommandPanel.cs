using lasercom.objects;
using lasercom.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace LUI.controls
{
    [Designer(typeof(LUI.controls.designer.ObjectCommandPanelDesigner))]
    public class ObjectCommandPanel : UserControl
    {
        public event EventHandler ObjectChanged;

        public LuiObjectParameters Object { get; set; }

        private GroupBox Group;

        private FlowLayoutPanel _Flow;
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FlowLayoutPanel Flow
        {
            get
            {
                return _Flow;
            }
        }

        private LabeledControl<ComboBox> _Objects;
        public ComboBox Objects
        {
            get
            {
                return _Objects.Control;
            }
        }

        public LuiObjectParameters SelectedObject
        {
            get
            {
                return (LuiObjectParameters)Objects.SelectedItem;
            }
            set
            {
                Objects.SelectedItem = value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        [Category("Appearance")]
        public override string Text
        {
            get
            {
                return Group.Text;
            }
            set
            {
                Group.Text = value;
            }
        }

        public ObjectCommandPanel()
        {
            SuspendLayout();

            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            Group = new GroupBox();
            Group.AutoSize = true;
            Group.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Group.Dock = DockStyle.Fill;

            _Flow = new FlowLayoutPanel();
            Flow.FlowDirection = FlowDirection.TopDown;
            Flow.AutoSize = true;
            Flow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Flow.Dock = DockStyle.Fill;

            _Objects = new LabeledControl<ComboBox>(new ComboBox(), "Device:");
            Objects.DropDownStyle = ComboBoxStyle.DropDownList;
            Objects.DisplayMember = "Name";
            Objects.SelectedIndexChanged += (s, e) => ObjectChanged.Raise(s, e);
            Flow.Controls.Add(_Objects);

            Group.Controls.Add(Flow);

            Controls.Add(Group);

            ResumeLayout();
        }

    }
}
