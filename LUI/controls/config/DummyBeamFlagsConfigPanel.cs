using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lasercom.control;

namespace LUI.controls
{
    class DummyBeamFlagsConfigPanel : LuiObjectConfigPanel
    {

        public override Type Target
        {
            get { return typeof(DummyBeamFlags); }
        }

        public DummyBeamFlagsConfigPanel()
            : base()
        {

        }

        public override void CopyTo(lasercom.objects.LuiObjectParameters p)
        {
            
        }

        public override void CopyFrom(lasercom.objects.LuiObjectParameters p)
        {
            
        }
    }
}
