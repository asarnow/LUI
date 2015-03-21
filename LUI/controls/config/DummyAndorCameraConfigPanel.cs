using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lasercom.camera;

namespace LUI.controls
{
    class DummyAndorCameraConfigPanel : LuiObjectConfigPanel<CameraParameters>
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
            
        }

        public override void CopyTo(CameraParameters other)
        {
            
        }
    }
}
