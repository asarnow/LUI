using System;
using lasercom.control;

namespace LUI.controls
{
    class DummyPumpConfigPanel : LuiObjectConfigPanel<PumpParameters>
    {
        public override Type Target
        {
            get { return typeof(DummyPump); }
        }

        public DummyPumpConfigPanel()
            : base()
        {

        }

        public override void CopyTo(PumpParameters other)
        {

        }

        public override void CopyFrom(PumpParameters other)
        {

        }
    }
}
