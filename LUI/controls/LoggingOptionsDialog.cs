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
using LUI.config;
using log4net.Repository.Hierarchy;
using LUI.tabs;

namespace LUI.controls
{
    public partial class LoggingOptionsDialog : LuiOptionsDialog
    {
        LabeledControl<ComboBox> LogLevel;

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

            LogLevel = new LabeledControl<ComboBox>(new ComboBox(), "Log Level");
            LogLevel.Control.DropDownStyle = ComboBoxStyle.DropDownList;

            Hierarchy h = (Hierarchy)LogManager.GetRepository();
            foreach (Level l in h.LevelMap.AllLevels) LogLevel.Control.Items.Add(l.DisplayName);

            LogLevel.Control.SelectedIndexChanged += (s, e) => OnOptionsChanged(s, e);

            Controls.Add(LogLevel);

            ConfigChanged += (s, e) => HandleConfigChanged(s, e);

            ResumeLayout(false);
        }

        public override void HandleApply(object sender, EventArgs e)
        {
            string LevelName = (string)LogLevel.Control.SelectedItem;
            Config.LogLevel = LevelName;
            //((Hierarchy)LogManager.GetRepository()).Root.Level = ((Hierarchy)LogManager.GetRepository()).LevelMap[LevelName];
            //((Hierarchy)LogManager.GetRepository()).RaiseConfigurationChanged(EventArgs.Empty);
        }

        public override void HandleConfigChanged(object sender, EventArgs e)
        {
            LogLevel.Control.SelectedItem = Config.LogLevel;
        }
    }
}
