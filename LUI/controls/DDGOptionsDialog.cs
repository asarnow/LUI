using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom.extensions;
using lasercom.ddg;

namespace LUI.controls
{
    public partial class DDGOptionsDialog : LUIOptionsDialog
    {
        LabeledControl<ComboBox> DelayGeneratorTypes;

        public DDGOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

         public DDGOptionsDialog(Size Size, bool Visibility)
        {
            InitializeComponent();
            this.Size = Size;
            this.Visible = Visibility;
            Init();
        }

        public DDGOptionsDialog(Size Size) : this(Size, true) {}

        private void Init()
        {
            SuspendLayout();

            Panel ConfigPanel = new Panel();
            ConfigPanel.Dock = DockStyle.Fill;
            Controls.Add(ConfigPanel);

            Panel ListPanel = new Panel();
            ListPanel.Dock = DockStyle.Left;
            Controls.Add(ListPanel);

            #region DDG list and DDG configuration panel
            ListView DelayGenerators = new ListView();
            DelayGenerators.SelectedIndexChanged += SelectedDDGChanged;
            DelayGenerators.Dock = DockStyle.Top;
            
            Panel DelayGeneratorsControlPanel = new Panel();
            DelayGeneratorsControlPanel.Dock = DockStyle.Top;

            #region Buttons
            Button Add = new Button();
            Add.Dock = DockStyle.Left;
            Add.Text = "Add";
            Add.Click += Add_Click;
            
            Button Remove = new Button();
            Remove.Dock = DockStyle.Left;
            Remove.Text = "Remove";
            Remove.Click += Remove_Click;

            DelayGeneratorsControlPanel.Controls.Add(Remove);
            DelayGeneratorsControlPanel.Controls.Add(Add);
            #endregion

            ListPanel.Controls.Add(DelayGeneratorsControlPanel);
            ListPanel.Controls.Add(DelayGenerators);
            #endregion

            #region DDG configuration subpanel
            Panel ConfigSubPanel = new Panel();
            ConfigSubPanel.Dock = DockStyle.Fill;
            ConfigPanel.Controls.Add(ConfigSubPanel);

            DelayGeneratorTypes = new LabeledControl<ComboBox>(new ComboBox(), "DDG Type:");
            DelayGeneratorTypes.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            DelayGeneratorTypes.Control.DisplayMember = "Name";
            List<Type> DDGTypes = typeof(IDigitalDelayGenerator).GetSubclasses(true);
            DDGTypes.ForEach(x => {
                DelayGeneratorTypes.Control.Items.Add(x);
            });
            DelayGeneratorTypes.TabIndexChanged += DelayGeneratorTypes_TabIndexChanged;
            DelayGeneratorTypes.Dock = DockStyle.Top;
            ConfigPanel.Controls.Add(DelayGeneratorTypes);
            #endregion

            ResumeLayout(false);
        }

        void DelayGeneratorTypes_TabIndexChanged(object sender, EventArgs e)
        {
            Type t = (Type)DelayGeneratorTypes.Control.SelectedItem;
            
        }

        void Remove_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void Add_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SelectedDDGChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OnApply()
        {
            throw new NotImplementedException();
        }
    }
}
