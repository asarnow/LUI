using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.objects;
using log4net;
using LUI.config;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Threading;

namespace LUI.tabs
{
    public partial class LuiTab : UserControl
    {
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Commander Commander { get; set; }
        public LuiConfig Config { get; set; }
        protected BackgroundWorker worker;
        protected bool wait;

        protected BackgroundWorker ioWorker;
        protected Dispatcher Dispatcher;

        public LuiTab(LuiConfig config)
        {
            Config = config;
            Commander = new Commander();
            InitializeComponent();
            Init();
            CameraGain.ValueChanged += CameraGain_ValueChanged;
            Collect.Click += Collect_Click;
            Abort.Click += Abort_Click;
            Clear.Click += Clear_Click;
            OpenLaser.Click += OpenLaser_Click;
            CloseLaser.Click += CloseLaser_Click;
            OpenLamp.Click += OpenLamp_Click;
            CloseLamp.Click += CloseLamp_Click;
            Graph.MouseClick += Graph_Click;
        }

        public LuiTab() : this(null) { }

        private void Init()
        {
            SuspendLayout();

            Abort.Enabled = false;
            //Panel DummyWidth = new Panel();
            //DummyWidth.Height = 0;
            //DummyWidth.Width = CommonObjectPanel.Width;
            //CommonObjectPanel.Controls.Add(DummyWidth);
            ResumeLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CameraBox.ObjectChanged += HandleCameraChanged;
            BeamFlagBox.ObjectChanged += HandleBeamFlagsChanged;

            if (!IsInDesignMode())
            {
                HandleParametersChanged(this, EventArgs.Empty);
                LoadSettings();
            }
            
            Graph.XLeft = (float)Commander.Camera.Calibration[0];
            Graph.XRight = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
        }

        public virtual void HandleParametersChanged(object sender, EventArgs e)
        {
            var selectedCamera = CameraBox.SelectedObject;
            CameraBox.Objects.Items.Clear();
            foreach (var p in Config.GetParameters(typeof(CameraParameters)))
                CameraBox.Objects.Items.Add(p);
            // One of next two lines will trigger CameraChanged event.
            CameraBox.SelectedObject = selectedCamera;
            if (CameraBox.Objects.SelectedItem == null) CameraBox.Objects.SelectedIndex = 0;

            var selectedBeamFlags = BeamFlagBox.SelectedObject;
            BeamFlagBox.Objects.Items.Clear();
            foreach (var p in Config.GetParameters(typeof(BeamFlagsParameters)))
                BeamFlagBox.Objects.Items.Add(p);
            BeamFlagBox.SelectedObject = selectedBeamFlags;
            if (BeamFlagBox.Objects.SelectedItem == null) BeamFlagBox.Objects.SelectedIndex = 0;
        }

        public virtual void HandleCameraChanged(object sender, EventArgs e)
        {
            // Replace the Camera property in the Commander.
            Commander.Camera = (ICamera)Config.GetObject((CameraParameters)CameraBox.SelectedObject);
            if (Commander.Camera.HasIntensifier)
            {
                CameraGain.Enabled = true;
                CameraGain.Minimum = Commander.Camera.MinIntensifierGain;
                CameraGain.Maximum = Commander.Camera.MaxIntensifierGain;
                CameraGain.Value = Commander.Camera.IntensifierGain;
            }
            else
            {
                CameraGain.Enabled = false;
                CameraGain.Minimum = 0;
                CameraGain.Maximum = 0;
                CameraGain.Value = 0;
            }
            // Update the graph with new camera's calibrated X-axis.
            HandleCalibrationChanged(sender, new LuiObjectParametersEventArgs(CameraBox.SelectedObject));
        }

        public virtual void HandleCalibrationChanged(object sender, LuiObjectParametersEventArgs e)
        {
            // If a different camera is selected, do nothing (until that camera is selected by the user).
            if (!CameraBox.SelectedObject.Equals(e.Argument)) return;

            Graph.XLeft = (float)Commander.Camera.Calibration[0];
            Graph.XRight = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
            Graph.ClearAxes();
            Graph.Invalidate();
        }

        public virtual void HandleBeamFlagsChanged(object sender, EventArgs e)
        {
            if (Commander.BeamFlag != null) Commander.BeamFlag.CloseLaserAndFlash();
            Commander.BeamFlag = (AbstractBeamFlags)Config.GetObject(BeamFlagBox.SelectedObject);
        }

        public virtual void HandleContainingTabSelected(object sender, EventArgs e)
        {
            if (Commander.Camera != null && Commander.Camera.HasIntensifier)
                CameraGain.Value = Commander.Camera.IntensifierGain;
        }

        public void HandleExit(object sender, EventArgs e)
        {
            if (Config.Saved)
            {
                SaveSettings();
            }
        }

        protected virtual void LoadSettings()
        {
            var Settings = Config.TabSettings[this.GetType().Name];
            string value;
            if (Settings.TryGetValue("Camera", out value))
                CameraBox.SelectedObject = Config.GetFirstParameters(typeof(CameraParameters), value);
            if (Settings.TryGetValue("BeamFlag", out value))
                BeamFlagBox.SelectedObject = Config.GetFirstParameters(typeof(BeamFlagsParameters), value);
        }

        protected virtual void SaveSettings()
        {
            var Settings = Config.TabSettings[this.GetType().Name];
            Settings["Camera"] = CameraBox.SelectedObject != null ? CameraBox.SelectedObject.Name : null;
            Settings["BeamFlag"] = BeamFlagBox.SelectedObject != null ? BeamFlagBox.SelectedObject.Name : null;
        }

        protected virtual void Collect_Click(object sender, EventArgs e)
        {
            Collect.Enabled = NScan.Enabled = false;
            Abort.Enabled = true;
            int N = (int)NScan.Value;
            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(WorkProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(WorkComplete);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(N);
        }

        protected virtual void Abort_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            Graph.Clear();
            Graph.Invalidate();
        }

        private void OpenLaser_Click(object sender, EventArgs e)
        {
            Commander.BeamFlag.OpenLaser();
        }

        private void CloseLaser_Click(object sender, EventArgs e)
        {
            Commander.BeamFlag.CloseLaser();
        }

        private void OpenLamp_Click(object sender, EventArgs e)
        {
            Commander.BeamFlag.OpenFlash();
        }

        private void CloseLamp_Click(object sender, EventArgs e)
        {
            Commander.BeamFlag.CloseFlash();
        }

        private void CameraGain_ValueChanged(object sender, EventArgs e)
        {
            Commander.Camera.IntensifierGain = (int)CameraGain.Value;
        }

        protected virtual void Graph_Click(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected virtual void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public bool IsBusy
        {
            get
            {
                return worker != null ? worker.IsBusy : false;
            }
        }

        public ParentForm.TaskState TaskBusy()
        {
            return ((ParentForm)FindForm()).CurrentTask;
        }

        public static bool IsInDesignMode()
        {
            if (Application.ExecutablePath.IndexOf("devenv.exe", StringComparison.OrdinalIgnoreCase) > -1)
            {
                return true;
            }
            return false;
        }

        #region dialogs

        protected void BlockingBlankDialog()
        {
            DialogResult result = MessageBox.Show("Please insert blank",
                "Blank",
                MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                worker.CancelAsync();
            }
            wait = false;
        }

        protected void BlockingSampleDialog()
        {
            DialogResult result = MessageBox.Show("Please insert sample",
                    "Continue",
                    MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                worker.CancelAsync();
            }
            wait = false;
        }

        #endregion

    }
}
