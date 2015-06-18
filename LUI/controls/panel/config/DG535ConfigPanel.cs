using System;
using System.Windows.Forms;
using lasercom.ddg;
using lasercom.gpib;

namespace LUI.controls
{
    class DG535ConfigPanel : LuiObjectConfigPanel<DelayGeneratorParameters>
    {

        LabeledControl<ComboBox> GpibAddress;
        LabeledControl<ComboBox> GpibProvider;

        public override Type Target
        {
            get { return typeof(DG535); }
        }

        public DG535ConfigPanel()
            : base()
        {
            GpibAddress = new LabeledControl<ComboBox>(new ComboBox(), "GPIB Address:");
            GpibAddress.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            for (byte b = 0; b < 32; b++) GpibAddress.Control.Items.Add(b);
            GpibAddress.Control.SelectedIndexChanged += (s,e) => OnOptionsChanged(s,e);
            this.Controls.Add(GpibAddress);
        }

        public DG535ConfigPanel(LuiOptionsListDialog<IGpibProvider, GpibProviderParameters> GpibOptionsList)
            : this()
        {
            GpibProvider = new LabeledControl<ComboBox>(new ComboBox(), "GPIB Provider:");
            GpibProvider.Control.DisplayMember = "Name";
            GpibProvider.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            GpibOptionsList.OptionsChanged += (s, e) => UpdateProviders(GpibOptionsList);
            GpibOptionsList.ConfigMatched += (s, e) => UpdateProviders(GpibOptionsList);
            GpibProvider.Control.SelectedIndexChanged += OnOptionsChanged;
            this.Controls.Add(GpibProvider);
        }

        private void UpdateProviders(LuiOptionsListDialog<IGpibProvider, GpibProviderParameters> GpibOptionsList)
        {
            GpibProvider.Control.Items.Clear();
            foreach (var item in GpibOptionsList.TransientItems)
            {
                GpibProvider.Control.Items.Add(item);
            }
        }

        public override void CopyFrom(DelayGeneratorParameters other)
        {
            GpibAddress.Control.SelectedItem = other.GpibAddress;
            GpibProvider.Control.SelectedItem = other.GpibProvider;
        }

        public override void CopyTo(DelayGeneratorParameters other)
        {
            other.GpibAddress = (byte)GpibAddress.Control.SelectedItem;
            other.GpibProvider = (GpibProviderParameters)GpibProvider.Control.SelectedItem;
        }

    }
}
