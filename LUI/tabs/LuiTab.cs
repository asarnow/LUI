using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Threading;
using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.io;
using lasercom.objects;
using LUI.config;
using LUI.controls;

namespace LUI.tabs
{
    public partial class LuiTab : UserControl
    {
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
            Commander.Camera = new DummyAndorCamera();
            Commander.BeamFlag = new DummyBeamFlags();
            InitializeComponent();
            Collect.Click += Collect_Click;
            Abort.Click += Abort_Click;
            Clear.Click += Clear_Click;
            Graph.MouseClick += Graph_Click;
        }

        public LuiTab() : this(null) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ObjectSelector.CameraChanged += HandleCameraChanged;
            ObjectSelector.BeamFlagsChanged += HandleBeamFlagsChanged;

            HandleParametersChanged(this, EventArgs.Empty);

            Graph.XLeft = (float)Commander.Camera.Calibration[0];
            Graph.XRight = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
        }

        public virtual void HandleParametersChanged(object sender, EventArgs e)
        {
            var selectedCamera = ObjectSelector.SelectedCamera;
            ObjectSelector.Cameras.Items.Clear();
            foreach (var p in Config.GetParameters(typeof(CameraParameters))) 
                ObjectSelector.Cameras.Items.Add(p);
            // One of next two lines will trigger CameraChanged event.
            ObjectSelector.SelectedCamera = selectedCamera;
            if (ObjectSelector.Cameras.SelectedItem == null) ObjectSelector.Cameras.SelectedIndex = 0;

            var selectedBeamFlags = ObjectSelector.BeamFlags.SelectedItem;
            ObjectSelector.BeamFlags.Items.Clear();
            foreach (var p in Config.GetParameters(typeof(BeamFlagsParameters)))
                ObjectSelector.BeamFlags.Items.Add(p);
            ObjectSelector.BeamFlags.SelectedItem = selectedBeamFlags;
            if (ObjectSelector.BeamFlags.SelectedItem == null) ObjectSelector.BeamFlags.SelectedIndex = 0;
        }

        public virtual void HandleCameraChanged(object sender, EventArgs e)
        {
            Commander.Camera = (ICamera)Config.GetObject((CameraParameters)ObjectSelector.SelectedCamera);

            // Update the graph with new camera's calibrated X-axis.
            HandleCalibrationChanged(sender, new LuiObjectParametersEventArgs(ObjectSelector.SelectedCamera));
        }

        public virtual void HandleCalibrationChanged(object sender, LuiObjectParametersEventArgs e)
        {
            // If a different camera is selected, do nothing (until that camera is selected by the user).
            if (!ObjectSelector.SelectedCamera.Equals(e.Argument)) return;

            Graph.XLeft = (float)Commander.Camera.Calibration[0];
            Graph.XRight = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
            Graph.ClearAxes();
            Graph.Invalidate();
        }

        public virtual void HandleBeamFlagsChanged(object sender, EventArgs e)
        {
            if (Commander.BeamFlag != null) Commander.BeamFlag.CloseLaserAndFlash();
            Commander.BeamFlag = (AbstractBeamFlags)Config.GetObject(ObjectSelector.SelectedBeamFlags);
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
                return worker.IsBusy;
            }
        }

        public ParentForm.State TaskBusy()
        {
            return ((ParentForm)FindForm()).TaskBusy;
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
