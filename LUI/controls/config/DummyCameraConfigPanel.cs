using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lasercom.camera;
using lasercom.objects;

namespace LUI.controls
{
    class DummyCameraConfigPanel : LuiObjectConfigPanel<CameraParameters>
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
            
        }

        public override void CopyTo(CameraParameters other)
        {
            
        }
    }
}
