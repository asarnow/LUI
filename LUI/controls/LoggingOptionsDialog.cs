using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Core;
using log4net;

namespace LUI.controls
{
    public partial class LoggingOptionsDialog : LUIOptionsDialog
    {
        public LoggingOptionsDialog(Size Size, bool Visibility)
        {
            InitializeComponent();
            this.Size = Size;
            this.Visible = Visibility;
            Init();
        }

        public LoggingOptionsDialog(Size Size) : this(Size, true) {}

        public LoggingOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            SuspendLayout();

            LabeledControl<ComboBox> LogLevel = new LabeledControl<ComboBox>(new ComboBox(), "Log Level");
            LogLevel.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            
            string[] loglevels = { "All", "Debug", "Info", "Warn", "Error", "Fatal", "Off" };
            LogLevel.Control.Items.AddRange(loglevels);
            LogLevel.Control.SelectedIndex = 1; // Default level.

            //LogLevel.SelectedIndexChanged += (sender, args) =>
            {
                // Update the log level of the root logger and raise configuration changed event.
                //((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).Root.Level = (Level)LogLevel.SelectedItem;
                //((log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
            };

            Controls.Add(LogLevel);

            ResumeLayout(false);
        }

        public override void OnApply()
        {
            throw new NotImplementedException();
        }
    }
}
