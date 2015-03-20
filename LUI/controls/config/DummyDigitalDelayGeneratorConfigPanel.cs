using lasercom.ddg;
using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUI.controls
{
    class DummyDigitalDelayGeneratorConfigPanel : LuiObjectConfigPanel
    {

        public override Type Target
        {
            get { return typeof(DummyDigitalDelayGenerator); }
        }

        public DummyDigitalDelayGeneratorConfigPanel()
            : base()
        {

        }

        public override void CopyFrom(LuiObjectParameters p)
        {
   
        }

        public override void CopyTo(LuiObjectParameters p)
        {

        }
    }
}
