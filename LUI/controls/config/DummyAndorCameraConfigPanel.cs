using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lasercom.camera;

namespace LUI.controls
{
    class DummyAndorCameraConfigPanel : LuiObjectConfigPanel
    {

        public override Type Target
        {
            get { return typeof(DummyAndorCamera); }
        }

        public DummyAndorCameraConfigPanel()
            : base()
        {

        }

        public override void CopyFrom(lasercom.objects.LuiObjectParameters p)
        {
            
        }

        public override void CopyTo(lasercom.objects.LuiObjectParameters p)
        {
            
        }
    }
}
