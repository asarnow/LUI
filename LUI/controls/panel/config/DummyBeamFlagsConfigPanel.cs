using System;
using lasercom.control;

namespace LUI.controls
{
    class DummyBeamFlagsConfigPanel : LuiObjectConfigPanel<BeamFlagsParameters>
    {

        public override Type Target
        {
            get { return typeof(DummyBeamFlags); }
        }

        public DummyBeamFlagsConfigPanel()
            : base()
        {

        }

        public override void CopyTo(BeamFlagsParameters other)
        {
            
        }

        public override void CopyFrom(BeamFlagsParameters other)
        {
            
        }
    }
}
