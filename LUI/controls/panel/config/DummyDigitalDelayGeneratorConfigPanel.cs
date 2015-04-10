using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lasercom.ddg;

namespace LUI.controls
{
    class DummyDigitalDelayGeneratorConfigPanel : LuiObjectConfigPanel<DelayGeneratorParameters>
    {

        public override Type Target
        {
            get { return typeof(DummyDigitalDelayGenerator); }
        }

        public DummyDigitalDelayGeneratorConfigPanel()
            : base()
        {

        }

        public override void CopyFrom(DelayGeneratorParameters other)
        {
   
        }

        public override void CopyTo(DelayGeneratorParameters other)
        {

        }
    }
}
