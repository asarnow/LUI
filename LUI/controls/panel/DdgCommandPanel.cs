using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using lasercom;
using lasercom.ddg;
using LUI.config;

namespace LUI.controls
{
    public partial class DdgCommandPanel : UserControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Commander Commander { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LuiConfig Config { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DelayGeneratorParameters PrimaryDelayDdg
        {
            get
            {
                return PrimaryDelayDdgs.SelectedItem as DelayGeneratorParameters;
            }
            set
            {
                PrimaryDelayDdgs.SelectedItem = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryDelayDelay
        {
            get
            {
                return PrimaryDelayDelays.SelectedItem as string;
            }
            set
            {
                PrimaryDelayDelays.SelectedItem = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PrimaryDelayTrigger
        {
            get
            {
                return PrimaryDelayTriggers.SelectedItem as string;
            }
            set
            {
                PrimaryDelayTriggers.SelectedItem = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        private string PrimaryDelayText
        {
            get
            {
                return PrimaryDelayValueText.Text;
            }
            set
            {
                PrimaryDelayValueText.Text = value;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double PrimaryDelayValue
        {
            get
            {
                return double.Parse(PrimaryDelayText);
            }
            set
            {
                PrimaryDelayText = value.ToString("E3");
            }
        }

        private bool _AllowZero;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool AllowZero
        {
            get
            {
                return _AllowZero;
            }
            set
            {
                _AllowZero = value;
            }
        }

        public DdgCommandPanel()
        {
            InitializeComponent();

            PrimaryDelayDdgs.SelectedIndexChanged += PrimaryDelayDdgs_SelectedIndexChanged;
            PrimaryDelayDelays.SelectedIndexChanged += PrimaryDelayDelays_SelectedIndexChanged;

            //PrimaryDelayTriggers.SelectedIndexChanged += PrimaryDelayTriggers_SelectedIndexChanged;

            PrimaryDelayValueText.TextChanged += PrimaryDelayValue_TextChanged;
            PrimaryDelayValueText.KeyPress += PrimaryDelayValue_KeyPress;

            PrimaryDelayDdgs.DisplayMember = "Name";
            PrimaryDelayDdgs.ValueMember = "Self";
        }

        private void PrimaryDelayDdgs_SelectedIndexChanged(object sender, EventArgs e)
        {
            var DDG = (IDigitalDelayGenerator)Config.GetObject((DelayGeneratorParameters)PrimaryDelayDdg);
            Commander.DDG = DDG;
            // Re-populate the available delay and trigger choices.
            PrimaryDelayDelays.Items.Clear();
            foreach (string d in DDG.Delays) PrimaryDelayDelays.Items.Add(d);
            PrimaryDelayTriggers.Items.Clear();
            foreach (string d in DDG.Triggers) PrimaryDelayTriggers.Items.Add(d);
            PrimaryDelayText = "";
        }

        private void PrimaryDelayDelays_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrimaryDelayTrigger = Commander.DDG.GetDelayTrigger(PrimaryDelayDelay);
            PrimaryDelayValue = Commander.DDG.GetDelayValue(PrimaryDelayDelay);
        }

        void PrimaryDelayTriggers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == PrimaryDelayTriggers && IsPrimaryDelayValueValid())
            {
                ApplyPrimaryDelayValue();
            }
        }

        void PrimaryDelayValue_TextChanged(object sender, EventArgs e)
        {
            if (!IsPrimaryDelayValueValid())
            {
                PrimaryDelayValueText.ForeColor = Color.Red;
            }
            else
            {
                PrimaryDelayValueText.ForeColor = SystemColors.WindowText;
            }
        }

        /// <summary>
        /// Handles Enter and Escape keys for PrimaryDelayValue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PrimaryDelayValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tb = sender as TextBox;
            Keys key = (Keys)e.KeyChar;
            if (key == Keys.Enter)
            {
                if (IsPrimaryDelayValueValid())
                {
                    ApplyPrimaryDelayValue();
                }
                UpdatePrimaryDelayValue();
                e.Handled = true;
            }
            if (key == Keys.Escape)
            {
                UpdatePrimaryDelayValue();
                e.Handled = true;
            }
        }

        bool IsPrimaryDelayValueValid()
        {
            double value;
            if (PrimaryDelayDdg == null ||
                PrimaryDelayDelay == null ||
                PrimaryDelayTrigger == null ||
                PrimaryDelayText == "")
            {
                return false;
            }
            else if (!double.TryParse(PrimaryDelayText, out value))
            {
                return false;
            }
            else if (AllowZero ? value < 0 : value <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void UpdatePrimaryDelayValue()
        {
            if (Commander.DDG != null && PrimaryDelayDelay != null)
            {
                PrimaryDelayValue = Commander.DDG.GetDelayValue(PrimaryDelayDelay);
            }
            else
            {
                PrimaryDelayText = "";
            }
        }

        public void ApplyPrimaryDelayValue()
        {
            Commander.DDG.SetDelay(PrimaryDelayDelay, PrimaryDelayTrigger, PrimaryDelayValue);
        }

        public void HandleParametersChanged(object sender, EventArgs e)
        {
            PrimaryDelayDdgs.Items.Clear();
            var parameters = Config.GetParameters(typeof(DelayGeneratorParameters));
            foreach (var p in parameters)
            {
                PrimaryDelayDdgs.Items.Add(p);
            }
        }

        private void DDGTable_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
