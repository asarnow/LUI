using System;
using lasercom.camera;

namespace LUI.controls
{
    class DummyAndorCameraConfigPanel : AndorCameraConfigPanel
    {

        public override Type Target
        {
            get { return typeof(DummyAndorCamera); }
        }

        public DummyAndorCameraConfigPanel()
            : base()
        {

        }

        public override void CopyFrom(CameraParameters other)
        {
            base.CopyFrom(other);
        }

        public override void CopyTo(CameraParameters other)
        {
            base.CopyTo(other);
        }
    }
}
