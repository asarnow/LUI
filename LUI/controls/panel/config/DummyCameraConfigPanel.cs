using System;
using lasercom.camera;

namespace LUI.controls
{
    class DummyCameraConfigPanel : CameraConfigPanel
    {

        public override Type Target
        {
            get { return typeof(DummyCamera); }
        }

        public DummyCameraConfigPanel()
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
